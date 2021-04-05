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
        /// <summary>
        /// Updates the “authorized” flag of an existing trust line this is called by the issuer of the asset.
        /// </summary>
        /// <param name="assetType">Asset type (native / alphanum4 / alphanum12)</param>
        /// <param name="assetCode">Asset code.</param>
        /// <param name="assetIssuer">Asset issuer.</param>
        /// <param name="amount">Amount</param>
        /// <param name="from">From account</param>
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
        /// From account
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string From { get; private set; }

        /// <summary>
        /// The asset to allow trust.
        /// </summary>
        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}
