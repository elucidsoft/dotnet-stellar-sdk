using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class RevokeSponsorshipOperationResponse : OperationResponse
    {
        public override int TypeId => 18;

        [JsonProperty(PropertyName = "account_id")]
        public string AccountID { get; private set; }

        [JsonProperty(PropertyName = "claimable_balance_id")]
        public string ClaimableBalanceID { get; private set; }

        [JsonProperty(PropertyName = "data_account_id")]
        public string DataAccountID { get; private set; }

        [JsonProperty(PropertyName = "data_name")]
        public string DataName { get; private set; }

        [JsonProperty(PropertyName = "offer_id")]
        public string OfferID { get; private set; }

        [JsonProperty(PropertyName = "trustline_account_id")]
        public string TrustlineAccountID { get; private set; }

        [JsonProperty(PropertyName = "trustline_asset")]
        public string TrustlineAsset { get; private set; }

        [JsonProperty(PropertyName = "signer_account_id")]
        public string SignerAccountID { get; private set; }

        [JsonProperty(PropertyName = "signer_key")]
        public string SignerKey { get; private set; }

        public RevokeSponsorshipOperationResponse()
        {

        }
    }
}
