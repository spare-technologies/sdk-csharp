using System;
using System.Runtime.Serialization;

namespace Spare.NET.Sdk.Exceptions
{
    [Serializable]
    public sealed class SpNullReferenceException : Exception
    {
        public SpNullReferenceException()
        {
        }

        public SpNullReferenceException(string message) : base(message)
        {
        }

        public SpNullReferenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SpNullReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}