using Newtonsoft.Json;
using Spare.NET.Sdk.Enum.Payment;

namespace Spare.NET.Sdk.Models.Payment.Domestic
{
    public class SpPaymentDebtorInformation
    {
        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("gender")]
        public SpGender? Gender { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }
    }
}