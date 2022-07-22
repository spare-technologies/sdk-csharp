using System;

namespace Spare.NET.Sdk.Exceptions
{
    [Serializable]
    public class SpNullReferenceException : Exception
    {
        public SpNullReferenceException()
        {
        }

        public SpNullReferenceException(string message) : base(message)
        {
        }
    }
}