using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.operations
{
    public class ChangeTrustOperationResponse : OperationResponse
    {
        public ChangeTrustOperationResponse(string assetCode, string assetIssuer, string assetType, string limit, KeyPair trustee, KeyPair trustor)
        {
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            AssetType = assetType;
            Limit = limit;
            Trustee = trustee;
            Trustor = trustor;
        }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "limit")]
        public string Limit { get; }

        [JsonProperty(PropertyName = "trustee")]
        public KeyPair Trustee { get; }

        [JsonProperty(PropertyName = "trustor")]
        public KeyPair Trustor { get; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}