using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spare.NET.Sdk.Models.Payment.Domestic;
using Spare.NET.Security.Crypto;
using Spare.NET.Security.DigitalSignature;

namespace Spare.NET.Sdk.Test
{
    [TestClass]
    [TestCategory("automatedTest")]
    [TestCategory("automatedUnitTest")]
    public class SpSecurityTest
    {
        /// <summary>
        /// Generate EC key pair test
        /// </summary>
        [TestMethod]
        public void A_Should_Generate_Key_Pair()
        {
            var keyPair = SpCrypto.GenerateKeyPair();
            Assert.IsNotNull(keyPair, "Keypair should not be null");
            Assert.IsFalse(string.IsNullOrWhiteSpace(keyPair.PrivateKey), "Keypair should have private key");
            Assert.IsFalse(string.IsNullOrWhiteSpace(keyPair.PublicKey), "Keypair should have public key");
        }

        /// <summary>
        /// Sign and verify object test
        /// </summary>
        [TestMethod]
        public void B_Should_Sign_And_Verify_Object()
        {
            var payment = new Faker<SpDomesticPaymentRequest>()
                .Rules((faker, domesticPayment) =>
                {
                    domesticPayment.Amount = faker.Finance.Amount(2m, 100000000m, 5);
                    domesticPayment.Description = faker.Commerce.ProductDescription();
                    domesticPayment.OrderId = faker.Hashids.Encode();
                }).Generate();

            var customerInfo = new Faker<SpPaymentDebtorInformation>().Rules((faker, information) =>
            {
                information.Email = faker.Person.Email;
                information.Fullname = faker.Person.FullName;
                information.Phone = faker.Phone.PhoneNumberFormat().Replace("-", "");
                information.CustomerReferenceId = faker.Person.Random.Guid().ToString();
            }).Generate();

            payment.CustomerInformation = customerInfo;

            var keyPair = SpCrypto.GenerateKeyPair();

            var signature = SpEccSignatureManager.Sign(payment, keyPair.PrivateKey);

            Assert.IsFalse(string.IsNullOrWhiteSpace(signature), "Signature should not be null");

            Assert.IsTrue(SpEccSignatureManager.Verify(payment, signature, keyPair.PublicKey),
                "Signature should be valid");
        }
    }
}