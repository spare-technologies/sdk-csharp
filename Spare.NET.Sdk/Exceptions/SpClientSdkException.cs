using System;

namespace Spare.NET.Sdk.Exceptions
{
    [Serializable]
    public class SpClientSdkException : Exception
    {
        public SpClientSdkException()
        {
        }

        public SpClientSdkException(string message) : base(message)
        {
        }
    }
}