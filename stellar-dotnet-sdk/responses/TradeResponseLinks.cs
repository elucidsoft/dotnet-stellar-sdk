using Newtonsoft.Json;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk.responses
{
    public class TradeResponseLinks
    {
        public TradeResponseLinks(Link<AssetResponse> baseLink, Link<AssetResponse> counterLink, Link<OperationResponse> operationLink)
        {
            Base = baseLink;
            Counter = counterLink;
            Operation = operationLink;
        }

        [JsonProperty(PropertyName = "base")]
        public Link<AssetResponse> Base;

        [JsonProperty(PropertyName = "counter")]
        public Link<AssetResponse> Counter;

        [JsonProperty(PropertyName = "operation")]
        public Link<OperationResponse> Operation;
    }
}