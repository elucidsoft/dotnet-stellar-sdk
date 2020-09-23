using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_sponsorship_removed effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineSponsorshipRemovedEffectResponse : EffectResponse
    {
        public override int TypeId => 65;

        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        public TrustlineSponsorshipRemovedEffectResponse()
        {

        }

        public TrustlineSponsorshipRemovedEffectResponse(string asset, string formerSponsor)
        {
            Asset = asset;
            FormerSponsor = formerSponsor;
        }
    }
}
