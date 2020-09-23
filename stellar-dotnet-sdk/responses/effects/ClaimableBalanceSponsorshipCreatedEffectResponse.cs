using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents claimable_balance_sponsorship_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class ClaimableBalanceSponsorshipCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 66;

        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }

        [JsonProperty(PropertyName = "sponsor")]
        public string Sponsor { get; private set; }

        public ClaimableBalanceSponsorshipCreatedEffectResponse()
        {

        }

        public ClaimableBalanceSponsorshipCreatedEffectResponse(string balanceID, string sponsor)
        {
            BalanceID = balanceID;
            Sponsor = sponsor;
        }
    }
}
