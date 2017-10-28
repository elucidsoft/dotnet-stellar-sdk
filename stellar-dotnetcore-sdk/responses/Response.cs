using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Linq;

namespace stellar_dotnetcore_sdk.responses
{
    public abstract class Response
    {
        public void SetHeaders(HttpResponseHeaders headers)
        {
            RateLimitLimit = int.Parse(headers.GetValues("X-Ratelimit-Limit").First());
            RateLimitRemaining = int.Parse(headers.GetValues("X-Ratelimit-Remaining").First());
            RateLimitReset = int.Parse(headers.GetValues("X-Ratelimit-Reset").First());
        }

        protected int RateLimitLimit { get; private set; }

        protected int RateLimitRemaining { get; private set; }

        protected int RateLimitReset { get; private set; }
    }
}
