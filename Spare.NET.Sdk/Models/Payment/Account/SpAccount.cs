using Newtonsoft.Json;

namespace Spare.NET.Sdk.Models.Payment.Account
{
    public class SpAccount
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        [JsonProperty("fullname")] public string Fullname { get; set; }
        [JsonProperty("phone")] public string Phone { get; set; }

    }
}