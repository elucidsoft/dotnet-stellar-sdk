using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class PathResponseLinks
    {
        [JsonProperty(PropertyName = "self")] public Link Self { get; }

        public PathResponseLinks(Link self)
        {
            Self = self;
        }
    }
}