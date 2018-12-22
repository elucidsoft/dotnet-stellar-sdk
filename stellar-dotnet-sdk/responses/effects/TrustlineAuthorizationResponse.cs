using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <inheritdoc />
    public class TrustlineAuthorizationResponse : EffectResponse
    {
        /// <inheritdoc />
        public TrustlineAuthorizationResponse(KeyPair trustor, string assetType, string assetCode)
        {
            Trustor = trustor;
            AssetType = assetType;
            AssetCode = assetCode;
        }

        [JsonProperty(PropertyName = "trustor")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair Trustor { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }
    }
}