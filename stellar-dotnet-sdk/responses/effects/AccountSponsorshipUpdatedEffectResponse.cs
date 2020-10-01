using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_sponsorship_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountSponsorshipUpdatedEffectResponse : EffectResponse
    {
        public override int TypeId => 61;


        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        [JsonProperty(PropertyName = "new_sponsor")]
        public string NewSponsor { get; private set; }

        public AccountSponsorshipUpdatedEffectResponse()
        {

        }

        public AccountSponsorshipUpdatedEffectResponse(string formerSponsor, string newSponsor)
        {
            FormerSponsor = formerSponsor;
            NewSponsor = newSponsor;
        }
    }
}
