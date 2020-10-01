using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class ClaimClaimableBalanceOperationResponse : OperationResponse
    {
        public override int TypeId => 15;

        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }

        [JsonProperty(PropertyName = "claimant")]
        public string Claimant { get; private set; }

        public ClaimClaimableBalanceOperationResponse()
        {

        }


        public ClaimClaimableBalanceOperationResponse(string balanceID, string claimant)
        {
            BalanceID = balanceID;
            Claimant = claimant;
        }
    }
}
