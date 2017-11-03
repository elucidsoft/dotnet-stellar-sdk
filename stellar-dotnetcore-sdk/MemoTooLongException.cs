using System;
using System.Runtime.Serialization;

namespace stellar_dotnetcore_sdk
{
    [Serializable]
    public class MemoTooLongException : Exception
    {
        public MemoTooLongException()
        {
        }

        public MemoTooLongException(string message) : base(message)
        {
        }

        public MemoTooLongException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MemoTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}