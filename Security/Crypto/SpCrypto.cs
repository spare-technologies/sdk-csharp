using System;
using System.IO;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Security.Crypto
{
    public static class SpCrypto
    {
        public static AsymmetricCipherKeyPair GenerateKeyPair()
        {
            var curve = ECNamedCurveTable.GetByName("prime256v1");
            var domainParams = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H, curve.GetSeed());

            var secureRandom = new SecureRandom();
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
            var outputPem = pkcs8Out.ToString();

            Console.WriteLine(outputPem);

            return keyPair;
        }
    }
}