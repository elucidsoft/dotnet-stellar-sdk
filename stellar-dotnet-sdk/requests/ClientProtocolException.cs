using System;

namespace stellar_dotnet_sdk.requests
{
    public class ClientProtocolException : Exception
    {
        public ClientProtocolException(string message)
            : base(message)
        {
        }
    }
}