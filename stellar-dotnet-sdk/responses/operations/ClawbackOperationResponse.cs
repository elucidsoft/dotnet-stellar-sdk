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
    public class ClawbackOperationResponse : OperationResponse
    {
        /// <param name="assetType">Asset type (native / alphanum4 / alphanum12)</param>
        /// <param name="assetCode">Asset code.</param>
        /// <param name="assetIssuer">Asset issuer.</param>
        /// <param name="amount">Asset amount clawed back</param>
        /// <param name="from">Account from which the asset is clawed back</param>
        public ClawbackOperationResponse(string assetType, string assetCode, string assetIssuer, string amount, string from)
        {
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            Amount = amount;
            From = from;
        }

        public ClawbackOperationResponse()
        {

        }

        public override int TypeId => 19;

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
        /// Amount
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        /// <summary>
        /// Account from which the asset is clawed back
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string From { get; private set; }

        /// <summary>
        /// Muxed Account from which the asset is clawed back
        /// </summary>
        [JsonProperty(PropertyName = "from_muxed")]
        public string FromMuxed { get; private set; }

        /// <summary>
        /// Muxed Account ID from which the asset is clawed back
        /// </summary>
        [JsonProperty(PropertyName = "from_muxed_id")]
        public ulong? FromMuxedID { get; private set; }

        /// <summary>
        /// Asset representation (Using the values of the other fields)
        /// </summary>
        public AssetTypeCreditAlphaNum Asset => stellar_dotnet_sdk.Asset.CreateNonNativeAsset(AssetCode, AssetIssuer);
    }
}
