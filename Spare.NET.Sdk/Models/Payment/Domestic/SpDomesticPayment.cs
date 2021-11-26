using Newtonsoft.Json;

namespace Spare.NET.Sdk.Models.Payment.Domestic
{
    public class SpDomesticPayment
    {
        [JsonProperty("amount")] public decimal? Amount { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}