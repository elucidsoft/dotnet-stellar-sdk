using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class TradeResponseLinks
    {
        public TradeResponseLinks(Link baseLink, Link counterLink, Link operationLink)
        {
            Base = baseLink;
            Counter = counterLink;
            Operation = operationLink;
        }

        [JsonProperty(PropertyName = "base")] public Link Base;

        [JsonProperty(PropertyName = "counter")]
        public Link Counter;

        [JsonProperty(PropertyName = "operation")]
        public Link Operation;
    }
}