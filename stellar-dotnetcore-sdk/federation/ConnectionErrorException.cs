using System;

namespace stellar_dotnetcore_sdk.federation
{
    public class ConnectionErrorException : Exception
    {
        public ConnectionErrorException()
        {
        }

        public ConnectionErrorException(string message)
            : base(message)
        {
        }
    }
}