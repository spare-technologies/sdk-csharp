using System;
using Newtonsoft.Json;
using Spare.NET.Sdk.Enum.Payment;
using Spare.NET.Sdk.Models.Payment.Account;

namespace Spare.NET.Sdk.Models.Payment.Domestic
{
    public sealed class SpDomesticPaymentResponse : SpDomesticPayment
    {
        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("reference")] public string Reference { get; set; }

        [JsonProperty("currency")] public string Currency { get; set; }

        [JsonProperty("issuer")] public SpUserAccount Issuer { get; set; }

        [JsonProperty("issuedFrom")] public SpPaymentSource? IssuedFrom { get; set; }

        [JsonProperty("debtor")] public SpPaymentUserAccount Debtor { get; set; }

        [JsonProperty("link")] public string Link { get; set; }

        [JsonProperty("createdAt")] public string CreatedAt { get; set; }
    }
}