using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_sponsorship_removed effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerSponsorshipRemovedEffectResponse : EffectResponse
    {
        public override int TypeId => 74;

        [JsonProperty(PropertyName = "signer")]
        public string Signer { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        public SignerSponsorshipRemovedEffectResponse()
        {

        }

        public SignerSponsorshipRemovedEffectResponse(string signer, string formerSponsor)
        {
            Signer = signer;
            FormerSponsor = formerSponsor;
        }
    }
}
