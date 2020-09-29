using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents claimable_balance_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class ClaimableBalanceClaimantCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 51;

        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; private set; }

        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        [JsonProperty(PropertyName = "predicate")]
        public Predicate Predicate { get; private set; }

        public ClaimableBalanceClaimantCreatedEffectResponse()
        {

        }

        public ClaimableBalanceClaimantCreatedEffectResponse(string asset, string balanceID, string amount, Predicate predicate)
        {
            Asset = asset;
            BalanceID = balanceID;
            Amount = amount;
            Predicate = predicate;
        }
    }
}
