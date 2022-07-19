using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spare.NET.Sdk.Models.Payment.Domestic;
using Spare.NET.Security.Crypto;
using Spare.NET.Security.DigitalSignature;

namespace Spare.NET.Sdk.Test
{
    [TestClass]
    public class SpSecurityTest
    {
        /// <summary>
        /// Generate EC key pair test
        /// </summary>
        [TestMethod]
        public void A_Should_Generate_Key_Pair()
        {
            var keyPair = SpCrypto.GenerateKeyPair();
            Assert.IsNotNull(keyPair);
            Assert.IsFalse(string.IsNullOrWhiteSpace(keyPair.PrivateKey));
            Assert.IsFalse(string.IsNullOrWhiteSpace(keyPair.PublicKey));
        }

        /// <summary>
        /// Sign and verify object test
        /// </summary>
        [TestMethod]
        public void B_Should_Sign_And_Verify_Object()
        {
            var payment = new SpDomesticPayment
            {
                Amount = 10_000,
                Description = "Unit test",
                OrderId = Guid.NewGuid().ToString(),
                CustomerInformation = new SpPaymentDebtorInformation
                {
                    Email = "test@example.com",
                    Fullname = "John Doe",
                    Phone = "+2160000000",
                    CustomerReferenceId = Guid.NewGuid().ToString()
                }
            };

            var keyPair = SpCrypto.GenerateKeyPair();

            var signature = SpEccSignatureManager.Sign(payment, keyPair.PrivateKey);
            
            Assert.IsFalse(string.IsNullOrWhiteSpace(signature));
            
            Assert.IsTrue(SpEccSignatureManager.Verify(payment,signature,keyPair.PublicKey));
        }
    }
}