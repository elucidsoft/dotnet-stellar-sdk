using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Response
    {
        protected int RateLimitLimit { get; private set; }

        protected int RateLimitRemaining { get; private set; }

        protected int RateLimitReset { get; private set; }

        public void SetHeaders(HttpResponseHeaders headers)
        {
            RateLimitLimit = int.Parse(headers.GetValues("X-Ratelimit-Limit").First());
            RateLimitRemaining = int.Parse(headers.GetValues("X-Ratelimit-Remaining").First());
            RateLimitReset = int.Parse(headers.GetValues("X-Ratelimit-Reset").First());
        }
    }
}