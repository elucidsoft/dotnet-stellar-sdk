using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_sponsorship_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerSponsorshipUpdatedEffectResponse : EffectResponse
    {
        public override int TypeId => 73;

        [JsonProperty(PropertyName = "signer")]
        public string Signer { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        [JsonProperty(PropertyName = "new_sponsor")]
        public string NewSponsor { get; private set; }

        public SignerSponsorshipUpdatedEffectResponse()
        {

        }

        public SignerSponsorshipUpdatedEffectResponse(string signer, string formerSponsor, string newSponsor)
        {
            Signer = signer;
            FormerSponsor = formerSponsor;
            NewSponsor = newSponsor;
        }
    }
}
