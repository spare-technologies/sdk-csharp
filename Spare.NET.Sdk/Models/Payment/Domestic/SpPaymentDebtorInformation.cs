using Newtonsoft.Json;

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

        [JsonProperty("customerReferenceId")]
        public string CustomerReferenceId { get; set; }
    }
}