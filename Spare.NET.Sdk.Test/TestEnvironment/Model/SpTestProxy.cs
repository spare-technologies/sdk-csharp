using Newtonsoft.Json;

namespace Spare.NET.Sdk.Test.TestEnvironment.Model
{
    public class SpTestProxy
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}