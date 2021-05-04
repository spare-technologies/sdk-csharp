using Newtonsoft.Json;

namespace Payment.Models.Payment.Account
{
    public class SpUserPaymentBankAccount
    {
        [JsonProperty("scheme")] 
        public string Scheme { get; set; }

        [JsonProperty("identification")] 
        public string Identification { get; set; }
    }
}