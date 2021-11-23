using Newtonsoft.Json;
using Spare.NET.Sdk.Models.Response;

namespace Spare.NET.Sdk.Models.Payment.Domestic
{
    public class SpCreateDomesticPaymentResponse
    {
        [JsonProperty("paymentResponse")]
        public SpSpareSdkResponse<SpDomesticPaymentResponse, object> PaymentResponse { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}