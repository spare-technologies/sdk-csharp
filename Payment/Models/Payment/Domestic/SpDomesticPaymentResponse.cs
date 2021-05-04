using System;
using Newtonsoft.Json;
using Payment.Enum.Payment;
using Payment.Models.Payment.Account;

namespace Payment.Models.Payment.Domestic
{
    public class SpDomesticPaymentResponse: SpDomesticPayment
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("reference")]
        public string Reference { get; set; }
        
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("issuer")]
        public SpUserAccount Issuer { get; set; }
        
        [JsonProperty("issuedFrom")]
        public SpPaymentSource? IssuedFrom { get; set; }

        [JsonProperty("debtor")]
        public SpPaymentUserAccount Debtor { get; set; }
        
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }
    }
}