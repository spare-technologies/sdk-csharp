using System.Runtime.Serialization;

namespace Spare.NET.Sdk.Enum.Payment
{
    public enum SpGender
    {
        [EnumMember(Value = "MALE")]
        Male = 1,
        [EnumMember(Value = "FEMALE")]
        Female = 2,
    }
}