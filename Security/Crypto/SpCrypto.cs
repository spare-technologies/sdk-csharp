using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Security.Crypto
{
    public static class SpCrypto
    {
        /// <summary>
        /// Generate prime256v1 key pair
        /// </summary>
        /// <returns></returns>
        public static SpEcKeyPair GenerateKeyPair()
        {
            var curve = ECNamedCurveTable.GetByName("prime256v1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            var secureRandom = new SecureRandom();
            secureRandom.SetSeed(Guid.NewGuid().ToByteArray());
            var keyParams = new ECKeyGenerationParameters(domainParams, secureRandom);

            var generator = new ECKeyPairGenerator("ECDSA");

            generator.Init(keyParams);
            var keyPair = generator.GenerateKeyPair();

            var privateKey = keyPair.Private as ECPrivateKeyParameters;
            var publicKey = keyPair.Public as ECPublicKeyParameters;

            var pkcs8 = new Pkcs8Generator(privateKey);

            using var pkcs8Out = new StringWriter();
            var pemWriter = new PemWriter(pkcs8Out);
            pemWriter.WriteObject(pkcs8.Generate());
            pkcs8Out.Close();

            var keyPairModel = new SpEcKeyPair
            {
                PrivateKey = pkcs8Out.ToString()
            };

            var key = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKey).GetDerEncoded();

            var keyStr = Convert.ToBase64String(key,
                Base64FormattingOptions.InsertLineBreaks);

            var builder = new StringBuilder($"-----BEGIN PUBLIC KEY-----\n");
            builder.Append($"{keyStr}\n");
            builder.Append("-----END PUBLIC KEY-----");

            keyPairModel.PublicKey = builder.ToString();

            return keyPairModel;
        }
    }
}