using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <inheritdoc />
    public class TrustlineAuthorizationResponse : EffectResponse
    {
        public TrustlineAuthorizationResponse()
        {

        }

        /// <inheritdoc />
        public TrustlineAuthorizationResponse(KeyPair trustor, string assetType, string assetCode)
        {
            Trustor = trustor;
            AssetType = assetType;
            AssetCode = assetCode;
        }

        [JsonProperty(PropertyName = "trustor")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair Trustor { get; private set; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }
    }
}