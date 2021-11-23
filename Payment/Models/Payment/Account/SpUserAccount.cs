using Newtonsoft.Json;

namespace Payment.Models.Payment.Account
{
    public sealed class SpUserAccount
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }
    }
}