using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <inheritdoc />
    /// <summary>
    /// Represents Clawback operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="T:stellar_dotnetcore_sdk.requests.ClawbackOperationResponse" />
    /// <seealso cref="T:stellar_dotnetcore_sdk.Server" />
    /// </summary>
    public class SetTrustlineFlagsOperationResponse : OperationResponse
    {
        /// <summary>
        /// Updates the “authorized” flag of an existing trust line this is called by the issuer of the asset.
        /// </summary>
        /// <param name="assetType">Asset type (native / alphanum4 / alphanum12)</param>
        /// <param name="assetCode">Asset code.</param>
        /// <param name="assetIssuer">Asset issuer.</param>
        /// <param name="amount">Amount</param>
        /// <param name="from">From account</param>
        public SetTrustlineFlagsOperationResponse(string assetType, string assetCode, string assetIssuer, string trustor, uint? setFlags, uint? clearFlags)
        {
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            SetFlags = setFlags;
            ClearFlags = clearFlags;
        }

        public SetTrustlineFlagsOperationResponse()
        {

        }

        public override int TypeId => 21;

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
        /// Trustor account.
        /// </summary>
        [JsonProperty(PropertyName = "trustor")]
        public string Trustor { get; private set; }

        /// <summary>
        /// Set Flags.
        /// </summary>
        [JsonProperty(PropertyName = "set_flags")]
        public uint? SetFlags { get; private set; }

        /// <summary>
        /// Set Flags.
        /// </summary>
        [JsonProperty(PropertyName = "clear_flags")]
        public uint? ClearFlags { get; private set; }

        /// <summary>
        /// The asset to allow trust.
        /// </summary>
        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}
