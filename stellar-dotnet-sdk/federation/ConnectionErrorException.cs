using System;

namespace stellar_dotnet_sdk.federation
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