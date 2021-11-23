using Newtonsoft.Json;

namespace Spare.NET.Sdk.Models.Payment.Account
{
    public sealed class SpUserPaymentBankAccount
    {
        [JsonProperty("scheme")] 
        public string Scheme { get; set; }

        [JsonProperty("identification")] 
        public string Identification { get; set; }
    }
}