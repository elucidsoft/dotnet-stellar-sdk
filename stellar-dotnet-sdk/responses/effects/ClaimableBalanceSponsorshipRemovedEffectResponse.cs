using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents claimable_balance_sponsorship_removed effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class ClaimableBalanceSponsorshipRemovedEffectResponse : EffectResponse
    {
        public override int TypeId => 68;

        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        public ClaimableBalanceSponsorshipRemovedEffectResponse()
        {

        }

        public ClaimableBalanceSponsorshipRemovedEffectResponse(string balanceID, string formerSponsor)
        {
            BalanceID = balanceID;
            FormerSponsor = formerSponsor;
        }
    }
}
