using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.operations
{
    /// <summary>
    /// Represents AllowTrust operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class AllowTrustOperationResponse : OperationResponse
    {
        public AllowTrustOperationResponse(KeyPair trustor, KeyPair trustee, string assetType, string assetCode, string assetIssuer, bool authorize)
        {
            Trustor = trustor;
            Trustee = trustee;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            Authorize = authorize;
        }

        [JsonProperty(PropertyName = "trustor")]
        public KeyPair Trustor { get; }

        [JsonProperty(PropertyName = "trustee")]
        public KeyPair Trustee { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; }

        [JsonProperty(PropertyName = "authorize")]
        public bool Authorize { get; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}