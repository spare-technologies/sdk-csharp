using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Spare.NET.Sdk.Client;
using Spare.NET.Sdk.Enum.Payment;
using Spare.NET.Sdk.Exceptions;
using Spare.NET.Sdk.Models.Payment.Domestic;
using Spare.NET.Sdk.Test.TestEnvironment.Model;
using Spare.NET.Security;
using Spare.NET.Security.DigitalSignature;

namespace Spare.NET.Sdk.Test
{
    [TestClass]
    [TestCategory("automatedTest")]
    [TestCategory("automatedE2ETest")]
    public class SpPaymentClientTest
    {
        private readonly string _paymentIdKey = $"{nameof(SpDomesticPayment)}_paymentId";

        private SpPaymentClient _paymentClient;

        private SpTestEnvironment _testEnvironment;

        /// <summary>
        /// Initialize test
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            LoadTestEnvironment();

            var options = new SpPaymentClientOptions
            {
                ApiKey = _testEnvironment.ApiKey,
                AppId = _testEnvironment.AppId,
                BaseUrl = new Uri(_testEnvironment.BaseUrl)
            };

            if (_testEnvironment.Proxy != null)
            {
                options.Proxy = new WebProxy
                {
                    Address = new Uri($"{_testEnvironment.Proxy.Host}:{_testEnvironment.Proxy.Port}"),
                    Credentials = new NetworkCredential
                    {
                        UserName = _testEnvironment.Proxy.Username,
                        Password = _testEnvironment.Proxy.Password
                    }
                };
            }

            _paymentClient = new SpPaymentClient(options);
        }

        /// <summary>
        /// Create domestic payment test
        /// </summary>
        [TestMethod]
        public async Task A_CreateDomesticPaymentTest()
        {
            var payment = new SpDomesticPaymentRequest
            {
                Amount = 10m,
                Description = "Spare.NET.Sdk test"
            };

            try
            {
                var paymentResponse =
                    await _paymentClient.CreateDomesticPayment(payment,
                        SpEccSignatureManager.Sign(payment, _testEnvironment.EcKeypair.Private));

                Trace.WriteLineIf(_testEnvironment.DebugMode, paymentResponse.ToJsonString());

                Assert.IsNotNull(paymentResponse, "Payment response should not be null");

                Assert.AreEqual(payment.Amount, paymentResponse.Payment.Amount,
                    "Payment response amount should be equal to payment request amount");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.Link),
                    "Payment response link should not be null");

                Assert.IsTrue(SpEccSignatureManager.Verify(paymentResponse.Payment, paymentResponse.Signature,
                    _testEnvironment.ServerPublicKey), "Payment response signature should be valid");

                Environment.SetEnvironmentVariable(_paymentIdKey, paymentResponse.Payment.Id.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToJsonString());
            }
        }

        /// <summary>
        /// Create domestic payment with customer information test
        /// </summary>
        [TestMethod]
        public async Task B_CreatePaymentWithCustomerInformation()
        {
            var payment = new Faker<SpDomesticPaymentRequest>()
                .Rules((faker, domesticPayment) =>
                {
                    domesticPayment.Amount = Math.Abs(faker.Finance.Amount());
                    domesticPayment.Description = faker.Commerce.ProductDescription();
                    domesticPayment.OrderId = faker.Hashids.Encode(125);
                }).Generate();

            var customerInfo = new Faker<SpPaymentDebtorInformation>().Rules((faker, information) =>
            {
                information.Email = faker.Person.Email;
                information.Fullname = faker.Person.FullName;
                information.Phone = faker.Phone.PhoneNumberFormat().Replace("-", "");
                information.CustomerReferenceId = faker.Person.Random.Guid().ToString();
            }).Generate();

            payment.CustomerInformation = customerInfo;

            var paymentResponse =
                await _paymentClient.CreateDomesticPayment(payment,
                    SpEccSignatureManager.Sign(payment, _testEnvironment.EcKeypair.Private));

            Trace.WriteLineIf(_testEnvironment.DebugMode, paymentResponse.ToJsonString());

            try
            {
                Assert.IsNotNull(paymentResponse, "Payment response should not be null");

                Assert.IsTrue(SpEccSignatureManager.Verify(paymentResponse.Payment, paymentResponse.Signature,
                    _testEnvironment.ServerPublicKey), "Payment response signature should be valid");

                Assert.IsNotNull(paymentResponse.Payment.Id, "Payment response id should not be null");

                Assert.IsNotNull(paymentResponse.Payment.Amount, "Payment response amount should not be null");

                Assert.AreEqual(payment.Amount, paymentResponse.Payment.Amount,
                    "Payment response amount should be equal to payment request amount");

                Assert.IsTrue(paymentResponse.Payment.IssuedFrom == SpPaymentSource.Api,
                    "Payment should be issued from API");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.Currency),
                    "Payment currency should not be null");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.Reference),
                    "Payment reference should not be null");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.CreatedAt),
                    "Payment created at should not be null");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.Description),
                    "Payment response description should not be null");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.OrderId),
                    "Payment response order id should not be null");

                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.Payment.Link),
                    "Payment response link should not be null");

                Assert.IsNotNull(paymentResponse.Payment.Debtor, "Payment response debtor should not be null");

                Assert.IsNotNull(paymentResponse.Payment.Debtor.Account,
                    "Payment response debtor account should not be null");

                Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Payment.Debtor.Account.Id),
                    "Payment response debtor id should not be null");

                Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Payment.Debtor.Account.Fullname),
                    "Payment response debtor fullname should not be null");

                Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Payment.Debtor.Account.Email),
                    "Payment response debtor email should not be null");

                Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Payment.Debtor.Account.Phone),
                    "Payment response debtor phone should not be null");

                Assert.IsFalse(
                    string.IsNullOrWhiteSpace(paymentResponse.Payment.Debtor.Account.CustomerReferenceId),
                    "Payment response debtor customer reference should not be null");


                Environment.SetEnvironmentVariable(_paymentIdKey, paymentResponse.Payment.Id.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToJsonString());
            }
        }

        /// <summary>
        /// Get domestic payment test
        /// </summary>
        [TestMethod]
        public async Task C_GetDomesticPaymentTest()
        {
            var paymentId = Environment.GetEnvironmentVariable(_paymentIdKey);

            Assert.IsFalse(string.IsNullOrWhiteSpace(paymentId));

            try
            {
                var paymentResponse = await _paymentClient.GetDomesticPayment(paymentId);

                Trace.WriteLineIf(_testEnvironment.DebugMode, paymentResponse.ToJsonString());

                Assert.IsNotNull(paymentResponse, "Payment response should not be null");

                Assert.IsNull(paymentResponse.Error, "Payment error should be null");

                Assert.IsNotNull(paymentResponse.Data, "Payment data should not be null");

                Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Data.Reference),
                    "Payment reference should not be null");
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToJsonString());
            }
        }

        /// <summary>
        /// List domestic payments test
        /// </summary>
        [TestMethod]
        public async Task D_ListPaymentsTest()
        {
            try
            {
                var listDomesticPayments = await _paymentClient.ListDomesticPayments();

                Trace.WriteLineIf(_testEnvironment.DebugMode, listDomesticPayments.ToJsonString());

                Assert.IsNotNull(listDomesticPayments, "Domestic payments list response should not be null");

                Assert.IsNull(listDomesticPayments.Error, "Domestic payments list response error should be null");

                Assert.IsNotNull(listDomesticPayments.Data, "Domestic payments list response data should not be null");

                if (listDomesticPayments.Data.Any())
                {
                    foreach (var paymentResponse in listDomesticPayments.Data)
                    {
                        Assert.IsNotNull(paymentResponse, "Payment should not be null");

                        Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Reference),
                            "Payment should have a reference");
                    }
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToJsonString());
            }
        }

        /// <summary>
        /// Wrong sdk configuration test
        /// </summary>
        [TestMethod]
        public void E_WrongSdkConfigurationTest()
        {
            Assert.ThrowsException<NullReferenceException>(() => { _paymentClient = new SpPaymentClient(null); },
                "Should validate client configuration");
        }

        /// <summary>
        /// Missing payment request signature test
        /// </summary>
        [TestMethod]
        public async Task F_MissingPaymentSignatureTest()
        {
            var payment = new SpDomesticPaymentRequest
            {
                Amount = 10m,
                Description = "Spare.NET.Sdk test"
            };

            await Assert.ThrowsExceptionAsync<SpClientSdkException>(() => _paymentClient.CreateDomesticPayment(payment, ""),
                "Should throws exception");
        }

        /// <summary>
        /// Load configuration
        /// </summary>
        private void LoadTestEnvironment()
        {
            using var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(),
                "TestEnvironment/testEnvironment.json"));
            _testEnvironment = JsonConvert.DeserializeObject<SpTestEnvironment>(sr.ReadToEnd());

            Assert.IsNotNull(_testEnvironment);
            Assert.IsNotNull(_testEnvironment.EcKeypair);
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.EcKeypair.Private));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.ServerPublicKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.ApiKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.AppId));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.BaseUrl));

            if (_testEnvironment.Proxy == null) return;
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.Proxy.Host));
            Assert.IsFalse(string.IsNullOrWhiteSpace(_testEnvironment.Proxy.Port));
        }
    }
}