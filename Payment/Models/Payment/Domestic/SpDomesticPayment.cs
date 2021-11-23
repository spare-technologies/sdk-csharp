using Newtonsoft.Json;

namespace Payment.Models.Payment.Domestic
{
    public class SpDomesticPayment
    {
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}