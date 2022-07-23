using System;
using System.Runtime.Serialization;

namespace Spare.NET.Sdk.Exceptions
{
    [Serializable]
    public sealed class SpClientSdkException : Exception
    {
        public SpClientSdkException()
        {
        }

        public SpClientSdkException(string message) : base(message)
        {
        }

        public SpClientSdkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SpClientSdkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}