using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_sponsorship_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineSponsorshipUpdatedEffectResponse : EffectResponse
    {
        public override int TypeId => 64;

        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        [JsonProperty(PropertyName = "new_sponsor")]
        public string NewSponsor { get; private set; }

        public TrustlineSponsorshipUpdatedEffectResponse()
        {

        }

        public TrustlineSponsorshipUpdatedEffectResponse(string asset, string formerSponsor, string newSponsor)
        {
            Asset = asset;
            FormerSponsor = formerSponsor;
            NewSponsor = newSponsor;
        }
    }
}
