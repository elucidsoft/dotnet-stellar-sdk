using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents claimable_balance_sponsorship_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class ClaimableBalanceSponsorshipUpdatedEffectResponse : EffectResponse
    {
        public override int TypeId => 67;

        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        [JsonProperty(PropertyName = "new_sponsor")]
        public string NewSponsor { get; private set; }

        public ClaimableBalanceSponsorshipUpdatedEffectResponse()
        {

        }

        public ClaimableBalanceSponsorshipUpdatedEffectResponse(string balanceID, string formerSponsor, string newSponsor)
        {
            BalanceID = balanceID;
            FormerSponsor = formerSponsor;
            NewSponsor = newSponsor;
        }
    }
}
