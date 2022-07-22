using Newtonsoft.Json;

namespace Spare.NET.Sdk.Models.Payment.Domestic
{
    public class SpDomesticPaymentRequest: SpDomesticPayment
    {
        [JsonProperty("customerInformation")]
        public SpPaymentDebtorInformation CustomerInformation { get; set; }
    }
}