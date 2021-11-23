using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payment.Client;
using Payment.Models.Payment.Domestic;
using Security;
using Security.DigitalSignature;

namespace PaymentClientTest
{
    [TestClass]
    [TestCategory("automatedTest")]
    public class SpPaymentClientTest
    {
        /// <summary>
        /// Ecc private key
        /// </summary>
        private static string PrivateKey = @"";

        /// <summary>
        /// Ecc public key
        /// </summary>
        private static string PublicKey = @"";

        /// <summary>
        /// Server public key
        /// </summary>
        private static string ServerPublicKey = @"";

        private SpPaymentClient _paymentClient;

        /// <summary>
        /// Initialize test
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            _paymentClient = new SpPaymentClient(new SpPaymentClientOptions
            {
                ApiKey = "",
                AppId = "",
                BaseUrl = new Uri("")
            });
        }

        /// <summary>
        /// Create domestic payment test
        /// </summary>
        [TestMethod]
        public async Task CreateDomesticPaymentTest()
        {
            var payment = new SpDomesticPayment
            {
                Amount = 10m,
                Description = "Payment test"
            };

            try
            {
                var paymentResponse =
                    await _paymentClient.CreateDomesticPayment(payment,
                        SpEccSignatureManager.Sign(payment, PrivateKey));

                Assert.IsNotNull(paymentResponse);
                Assert.IsTrue(string.IsNullOrWhiteSpace(paymentResponse.PaymentResponse.Error));
                Assert.AreEqual(payment.Amount, paymentResponse.PaymentResponse.Data.Amount);
                Assert.IsFalse(string.IsNullOrEmpty(paymentResponse.PaymentResponse.Data.Link));
                Assert.IsTrue(SpEccSignatureManager.Verify(paymentResponse.PaymentResponse, paymentResponse.Signature,
                    ServerPublicKey));
                Trace.WriteLine(paymentResponse.PaymentResponse.ToJsonString());
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
        public async Task GetDomesticPaymentTest()
        {
            //Requested payment id
            const string paymentId = "";
            try
            {
                var domesticPayment = await _paymentClient.GetDomesticPayment(paymentId);
                Assert.IsNotNull(domesticPayment);
                Assert.IsNull(domesticPayment.Error);
                Assert.IsNotNull(domesticPayment.Data);
                Assert.IsFalse(string.IsNullOrWhiteSpace(domesticPayment.Data.Reference));
                Trace.WriteLine(domesticPayment.ToJsonString());
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
        public async Task ListPaymentsTest()
        {
            try
            {
                var listDomesticPayments = await _paymentClient.ListDomesticPayments();
                Assert.IsNotNull(listDomesticPayments);
                Assert.IsNull(listDomesticPayments.Error);
                Assert.IsNotNull(listDomesticPayments.Data);
                if (listDomesticPayments.Data.Any())
                {
                    foreach (var paymentResponse in listDomesticPayments.Data)
                    {
                        Assert.IsNotNull(paymentResponse);
                        Assert.IsFalse(string.IsNullOrWhiteSpace(paymentResponse.Reference));
                    }
                }

                Trace.WriteLine(listDomesticPayments.ToJsonString());
            }
            catch (Exception e)
            {
                Assert.Fail(e.ToJsonString());
            }
        }
    }
}