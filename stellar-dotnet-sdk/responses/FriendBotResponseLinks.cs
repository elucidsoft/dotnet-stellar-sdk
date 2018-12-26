using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class FriendBotResponseLinks
    {
        [JsonProperty(PropertyName = "transaction")]
        public Link<TransactionResponse> Transaction { get; private set; }
    }
}