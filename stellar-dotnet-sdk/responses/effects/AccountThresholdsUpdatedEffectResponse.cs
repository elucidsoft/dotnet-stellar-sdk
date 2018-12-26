using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_thresholds_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountThresholdsUpdatedEffectResponse : EffectResponse
    {
        public AccountThresholdsUpdatedEffectResponse()
        {

        }

        /// <inheritdoc />
        public AccountThresholdsUpdatedEffectResponse(int lowThreshold, int medThreshold, int highThreshold)
        {
            LowThreshold = lowThreshold;
            MedThreshold = medThreshold;
            HighThreshold = highThreshold;
        }

        public override int TypeId => 4;

        [JsonProperty(PropertyName = "low_threshold")]
        public int LowThreshold { get; private set; }

        [JsonProperty(PropertyName = "med_threshold")]
        public int MedThreshold { get; private set; }

        [JsonProperty(PropertyName = "high_threshold")]
        public int HighThreshold { get; private set; }
    }
}