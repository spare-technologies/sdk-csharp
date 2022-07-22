using Newtonsoft.Json;
using Spare.NET.Sdk.Enum.Payment;

namespace Spare.NET.Sdk.Models.Payment.Account
{
    public class SpAccount
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("email")] public string Email { get; set; }
        
        [JsonProperty("gender")] public SpGender? Gender { get; set; }
        [JsonProperty("fullname")] public string Fullname { get; set; }

        [JsonProperty("picture")] public string Picture { get; set; }
        
        [JsonProperty("phone")] public string Phone { get; set; }

    }
}