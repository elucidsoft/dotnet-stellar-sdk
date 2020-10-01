using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents data_sponsorship_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class DataSponsorshipUpdatedEffectResponse : EffectResponse
    {
        public override int TypeId => 67;

        [JsonProperty(PropertyName = "new_sponsor")]
        public string NewSponsor { get; private set; }

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        [JsonProperty(PropertyName = "data_name")]
        public string DataName { get; private set; }

        public DataSponsorshipUpdatedEffectResponse()
        {

        }

        public DataSponsorshipUpdatedEffectResponse(string newSponsor, string formerSponsor, string dataName)
        {
            NewSponsor = newSponsor;
            FormerSponsor = formerSponsor;
            DataName = dataName;
        }
    }
}
