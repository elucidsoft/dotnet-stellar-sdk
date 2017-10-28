using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.requests
{
    public class ClientProtocolException : Exception
    {
        public ClientProtocolException(string message)
            : base(message)
        {

        }
    }
}
