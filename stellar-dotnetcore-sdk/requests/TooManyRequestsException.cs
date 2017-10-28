using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.requests
{
    public class TooManyRequestsException : Exception
    {
        public TooManyRequestsException(int retryAfter)
            : base("The rate limit for the requesting IP address is over its alloted limit.")
        {
            RetryAfter = retryAfter;
        }

        public int RetryAfter { get; set; }
    }
}
