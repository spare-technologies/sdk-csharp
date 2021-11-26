using Newtonsoft.Json;

namespace Spare.NET.Security.Crypto
{
    public class SpEcKeyPair
    {
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }

        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }
    }
}