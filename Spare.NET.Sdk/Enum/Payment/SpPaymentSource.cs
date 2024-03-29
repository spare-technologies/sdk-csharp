using System.Runtime.Serialization;

namespace Spare.NET.Sdk.Enum.Payment
{
    public enum SpPaymentSource
    {
        [EnumMember(Value = "MOBILE")] Mobile = 0,

        [EnumMember(Value = "WEB")] Web = 1,

        [EnumMember(Value = "API")] Api = 2
    }
}