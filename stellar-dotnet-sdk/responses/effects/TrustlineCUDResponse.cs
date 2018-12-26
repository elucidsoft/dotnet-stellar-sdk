using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    public abstract class TrustlineCUDResponse : EffectResponse
    {
        public TrustlineCUDResponse()
        {

        }

        protected TrustlineCUDResponse(string limit, string assetType, string assetCode, string assetIssuer)
        {
            Limit = limit;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
        }

        [JsonProperty(PropertyName = "limit")]
        public string Limit { get; private set; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}