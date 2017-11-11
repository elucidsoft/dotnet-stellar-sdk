using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.operations
{
    /// <summary>
    ///     Represents SetOptions operation response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    ///     <seealso cref="requests.OperationsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SetOptionsOperationResponse : OperationResponse
    {
        public SetOptionsOperationResponse(int lowThreshold, int medThreshold, int highThreshold, KeyPair inflationDestination,
            string homeDomain, KeyPair signerKey, int signerWeight, int masterKeyWeight, string[] clearFlags, string[] setFlags)
        {
            LowThreshold = lowThreshold;
            MedThreshold = medThreshold;
            HighThreshold = highThreshold;
            InflationDestination = inflationDestination;
            HomeDomain = homeDomain;
            SignerKey = signerKey;
            SignerWeight = signerWeight;
            MasterKeyWeight = masterKeyWeight;
            ClearFlags = clearFlags;
            SetFlags = setFlags;
        }

        [JsonProperty(PropertyName = "low_threshold")]
        public int LowThreshold { get; }

        [JsonProperty(PropertyName = "med_threshold")]
        public int MedThreshold { get; }

        [JsonProperty(PropertyName = "high_threshold")]
        public int HighThreshold { get; }

        [JsonProperty(PropertyName = "inflation_dest")]
        public KeyPair InflationDestination { get; }

        [JsonProperty(PropertyName = "home_domain")]
        public string HomeDomain { get; }

        [JsonProperty(PropertyName = "signer_key")]
        public KeyPair SignerKey { get; }

        [JsonProperty(PropertyName = "signer_weight")]
        public int SignerWeight { get; }

        [JsonProperty(PropertyName = "master_key_weight")]
        public int MasterKeyWeight { get; }

        [JsonProperty(PropertyName = "clear_flags_s")]
        public string[] ClearFlags { get; }

        [JsonProperty(PropertyName = "set_flags_s")]
        public string[] SetFlags { get; }
    }
}