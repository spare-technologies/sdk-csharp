using System.Runtime.Serialization;

namespace Spare.NET.Sdk.Enum.Payment
{
    public enum SpPaymentStatus
    {
        [EnumMember(Value = "AWAITING_AUTHORIZATION")]
        AwaitingAuthorization,
        
        [EnumMember(Value = "PENDING")] Pending,

        [EnumMember(Value = "COMPLETED")] Completed,

        [EnumMember(Value = "REJECTED")] Rejected
    }
}