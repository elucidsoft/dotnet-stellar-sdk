using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class PathResponseLinks
    {
        [JsonProperty(PropertyName = "self")]
        public Link<PathResponse> Self { get; }

        public PathResponseLinks(Link<PathResponse> self)
        {
            Self = self;
        }
    }
}