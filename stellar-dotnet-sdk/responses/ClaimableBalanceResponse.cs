using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class ClaimableBalanceResponse : Response
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }
        
        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; private set; }
        
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }
        
        [JsonProperty(PropertyName = "sponsor")]
        public string Sponsor { get; private set; }
        
        [JsonProperty(PropertyName = "last_modified_ledger")]
        public long LastModifiedLedger { get; private set; }
        
        [JsonProperty(PropertyName = "claimants")]
        public Claimant[] Claimants { get; private set; }
    }
}