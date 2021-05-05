using Newtonsoft.Json;

namespace Payment.Models.Payment.Account
{
    public sealed class SpPaymentUserAccount
    {
        [JsonProperty("account")]
        public SpUserAccount Account { get; set; }

        [JsonProperty("bankAccount")]
        public SpUserPaymentBankAccount BankAccount { get; set; }
    }
}