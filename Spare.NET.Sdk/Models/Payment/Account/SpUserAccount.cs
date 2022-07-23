using Newtonsoft.Json;

namespace Spare.NET.Sdk.Models.Payment.Account
{
    public class SpUserAccount: SpAccount
    {
        [JsonProperty("customerReferenceId")] public string CustomerReferenceId { get; set; }
        
        [JsonProperty("customerPaymentLink")] public string CustomerPaymentLink { get; set; }
    }
}