using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class FriendBotResponseLinks
    {
        [JsonProperty(PropertyName = "transaction")]
        public Link Transaction { get; private set; }
    }
}