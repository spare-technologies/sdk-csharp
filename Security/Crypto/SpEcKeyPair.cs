using Newtonsoft.Json;

namespace Security.Crypto
{
    public class SpEcKeyPair
    {
        [JsonProperty("publicKey")]
        public string PublicKey { get; set; }

        [JsonProperty("privateKey")]
        public string PrivateKey { get; set; }
    }
}