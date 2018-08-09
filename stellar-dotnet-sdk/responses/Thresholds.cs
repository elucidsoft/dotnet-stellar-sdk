using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///     Represents account thresholds.
    /// </summary>
    public class Thresholds
    {
        public Thresholds(int lowThreshold, int medThreshold, int highThreshold)
        {
            LowThreshold = lowThreshold;
            MedThreshold = medThreshold;
            HighThreshold = highThreshold;
        }

        [JsonProperty(PropertyName = "low_threshold")]
        public int LowThreshold { get; private set; }

        [JsonProperty(PropertyName = "med_threshold")]
        public int MedThreshold { get; private set; }

        [JsonProperty(PropertyName = "high_threshold")]
        public int HighThreshold { get; private set; }
    }
}