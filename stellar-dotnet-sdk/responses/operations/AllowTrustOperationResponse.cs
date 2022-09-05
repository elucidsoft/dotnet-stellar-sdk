using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <inheritdoc />
    /// <summary>
    /// Represents AllowTrust operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="T:stellar_dotnetcore_sdk.requests.OperationsRequestBuilder" />
    /// <seealso cref="T:stellar_dotnetcore_sdk.Server" />
    /// </summary>
    public class AllowTrustOperationResponse : OperationResponse
    {
        /// <summary>
        /// Updates the “authorized” flag of an existing trust line this is called by the issuer of the asset.
        /// </summary>
        /// <param name="trustor">Trustor account.</param>
        /// <param name="trustee">Trustee account.</param>
        /// <param name="assetType">Asset type (native / alphanum4 / alphanum12)</param>
        /// <param name="assetCode">Asset code.</param>
        /// <param name="assetIssuer">Asset issuer.</param>
        /// <param name="authorize">true when allowing trust, false when revoking trust</param>
        public AllowTrustOperationResponse(string trustor, string trustee, string assetType, string assetCode, string assetIssuer, bool authorize)
        {
            Trustor = trustor;
            Trustee = trustee;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            Authorize = authorize;
        }

        public AllowTrustOperationResponse()
        {

        }

        public override int TypeId => 7;

        /// <summary>
        /// Trustor account.
        /// </summary>
        [JsonProperty(PropertyName = "trustor")]
        public string Trustor { get; private set; }

        /// <summary>
        /// Trustee account.
        /// </summary>
        [JsonProperty(PropertyName = "trustee")]
        public string Trustee { get; private set; }

        /// <summary>
        /// Trustee account.
        /// </summary>
        [JsonProperty(PropertyName = "trustee_muxed")]
        public string TrusteeMuxed { get; private set; }

        /// <summary>
        /// Trustee account.
        /// </summary>
        [JsonProperty(PropertyName = "trustee_muxed_id")]
        public ulong? TrusteeMuxedID { get; private set; }

        /// <summary>
        /// Asset type (native / alphanum4 / alphanum12)
        /// </summary>
        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        /// <summary>
        /// Asset code.
        /// </summary>
        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        /// <summary>
        /// Asset issuer.
        /// </summary>
        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        /// <summary>
        /// true when allowing trust, false when revoking trust
        /// </summary>
        [JsonProperty(PropertyName = "authorize")]
        public bool Authorize { get; private set; }

        /// <summary>
        /// The asset to allow trust.
        /// </summary>
        public AssetTypeCreditAlphaNum Asset => stellar_dotnet_sdk.Asset.CreateNonNativeAsset(AssetCode, AssetIssuer);
    }
}
