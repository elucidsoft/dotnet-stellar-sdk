using Newtonsoft.Json;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk.responses.effects
{

    public class TrustlineAuthorizationResponse : EffectResponse
    {
        public TrustlineAuthorizationResponse(KeyPair trustor, string assetType, string assetCode)
        {
            Trustor = trustor;
            AssetType = assetType;
            AssetCode = assetCode;
        }

        [JsonProperty(PropertyName = "trustor")]
        public KeyPair Trustor { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }
    }
}