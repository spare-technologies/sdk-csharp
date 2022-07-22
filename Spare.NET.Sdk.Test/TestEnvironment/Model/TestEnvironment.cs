using Newtonsoft.Json;

namespace Spare.NET.Sdk.Test.TestEnvironment.Model
{
    public class SpTestEnvironment
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("appId")]
        public string AppId { get; set; }

        [JsonProperty("ecKeypair")]
        public SpTestKeypair EcKeypair { get; set; }

        [JsonProperty("serverPublicKey")]
        public string ServerPublicKey { get; set; }

        [JsonProperty("proxy")]
        public SpTestProxy Proxy { get; set; }

        [JsonProperty("debugMode")]
        public bool DebugMode { get; set; }
    }
}