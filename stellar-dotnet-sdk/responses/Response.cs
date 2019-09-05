using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    [JsonObject(MemberSerialization.OptIn)]
        public abstract class Response
    {
        private const string XRateLimitLimit = "X-Ratelimit-Limit";
        private const string XRateLimitRemaining = "X-Ratelimit-Remaining";
        private const string XRateLimitReset = "X-Ratelimit-Reset";

        protected int RateLimitLimit { get; private set; }

        protected int RateLimitRemaining { get; private set; }

        protected int RateLimitReset { get; private set; }

        public void SetHeaders(HttpResponseHeaders headers)
        {
            if(headers.Contains(XRateLimitLimit))
                RateLimitLimit = int.Parse(headers.GetValues(XRateLimitLimit).First());

            if (headers.Contains(XRateLimitRemaining))
                RateLimitRemaining = int.Parse(headers.GetValues(XRateLimitRemaining).First());

            if (headers.Contains(XRateLimitReset))
                RateLimitReset = int.Parse(headers.GetValues(XRateLimitReset).First());
        }
    }
}
