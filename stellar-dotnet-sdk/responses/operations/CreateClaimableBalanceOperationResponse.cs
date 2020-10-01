using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class CreateClaimableBalanceOperationResponse : OperationResponse
    {
        public override int TypeId => 14;

        [JsonProperty(PropertyName = "sponsor")]
        public string Sponsor { get; private set; }

        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        [JsonProperty(PropertyName = "claimants")]
        public Claimant[] Claimants { get; private set; }

        public CreateClaimableBalanceOperationResponse()
        {

        }


        public CreateClaimableBalanceOperationResponse(string sponsor, string asset, string amount, Claimant[] claimants)
        {
            Sponsor = sponsor;
            Asset = asset;
            Amount = amount;
            Claimants = claimants;
        }
    }
}
