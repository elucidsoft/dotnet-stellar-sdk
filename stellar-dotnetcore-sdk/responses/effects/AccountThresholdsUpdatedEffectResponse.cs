using Newtonsoft.Json;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_thresholds_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountThresholdsUpdatedEffectResponse : EffectResponse
    {
        public AccountThresholdsUpdatedEffectResponse(int lowThreshold, int medThreshold, int highThreshold)
        {
            LowThreshold = lowThreshold;
            MedThreshold = medThreshold;
            HighThreshold = highThreshold;
        }

        [JsonProperty(PropertyName = "low_threshold")]
        public int LowThreshold { get; }

        [JsonProperty(PropertyName = "med_threshold")]
        public int MedThreshold { get; }

        [JsonProperty(PropertyName = "high_threshold")]
        public int HighThreshold { get; }
    }
}