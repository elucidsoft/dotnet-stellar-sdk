using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents data_sponsorship_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class DataSponsorshipCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 66;

        [JsonProperty(PropertyName = "sponsor")]
        public string Sponsor { get; private set; }

        [JsonProperty(PropertyName = "data_name")]
        public string DataName { get; private set; }

        public DataSponsorshipCreatedEffectResponse()
        {

        }

        public DataSponsorshipCreatedEffectResponse(string sponsor, string dataName)
        {
            Sponsor = sponsor;
            DataName = dataName;
        }
    }
}
