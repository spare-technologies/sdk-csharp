using Newtonsoft.Json;
using Payment.Models.Response;

namespace Payment.Models.Payment.Domestic
{
    public class SpCreateDomesticPaymentResponse
    {
        [JsonProperty("paymentResponse")]
        public SpSpareSdkResponse<SpDomesticPaymentResponse, object> PaymentResponse { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}