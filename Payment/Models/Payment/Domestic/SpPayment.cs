using System;
using Newtonsoft.Json;

namespace Payment.Models.Payment.Domestic
{
    public class SpDomesticPayment
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("successUrl")]
        public Uri SuccessUrl { get; set; }
        
        [JsonProperty("failUrl")]
        public Uri FailUrl { get; set; }
    }
}