using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Security.DigitalSignature
{
    public static class EccSignatureManager
    {
        /// <summary>
        /// Sign payload
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Sign(object data, string privateKey)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            };

            var bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));

            var sig = SignerUtilities.GetSigner("SHA-256withECDSA");
            var keyPair =
                (ECPrivateKeyParameters) new PemReader(new StringReader(privateKey)).ReadObject();

            sig.Init(true, keyPair);
            sig.BlockUpdate(bytes, 0, bytes.Length);
            return Convert.ToBase64String(sig.GenerateSignature());
        }

        /// <summary>
        /// Verify payload signature
        /// </summary>
        /// <param name="data"></param>
        /// <param name="signature"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool Verify(object data, string signature, string publicKey)
        {
            var sig = SignerUtilities.GetSigner("SHA-256withECDSA");

            var keyPair =
                (ECPublicKeyParameters) new PemReader(new StringReader(publicKey)).ReadObject();

            sig.Init(false, keyPair);
            var input = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
            });
            var bytes = Encoding.ASCII.GetBytes(input);

            var decodedSignature = Convert.FromBase64String(signature);

            sig.BlockUpdate(bytes, 0, bytes.Length);
            return sig.VerifySignature(decodedSignature);
        }

        /// <summary>
        /// Verify payload signature
        /// </summary>
        /// <param name="message"></param>
        /// <param name="signature"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool Verify(string message, string signature, string publicKey)
        {
            var sig = SignerUtilities.GetSigner("SHA-256withECDSA");

            var keyPair =
                (ECPublicKeyParameters) new PemReader(new StringReader(publicKey)).ReadObject();

            sig.Init(false, keyPair);

            var bytes = Encoding.ASCII.GetBytes(message);

            var decodedSignature = Convert.FromBase64String(signature);

            sig.BlockUpdate(bytes, 0, bytes.Length);
            return sig.VerifySignature(decodedSignature);
        }
    }
}