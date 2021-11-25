using Newtonsoft.Json;

namespace Spare.NET.Sdk.Models.Payment.Domestic
{
    public class SpCreateDomesticPaymentResponse
    {
        [JsonProperty("payment")]
        public SpDomesticPaymentResponse Payment { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}