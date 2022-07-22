using Newtonsoft.Json;

namespace Spare.NET.Sdk.Test.TestEnvironment.Model
{
    public class SpTestKeypair
    {
        [JsonProperty("private")]
        public string Private { get; set; }

        [JsonProperty("public")]
        public string Public { get; set; }
    }
}