using System.Runtime.Serialization;

namespace Spare.NET.Sdk.Enum.Payment
{
    public enum SpGender
    {
        [EnumMember(Value = "MALE")]
        MALE = 1,
        [EnumMember(Value = "FEMALE")]
        FEMALE = 2
    }
}