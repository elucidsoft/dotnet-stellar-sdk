using System;
using System.Runtime.Serialization;

namespace stellar_dotnet_sdk
{
    public class FormatException : Exception
    {
        public FormatException()
        {
        }

        public FormatException(string message) : base(message)
        {
        }

        public FormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}