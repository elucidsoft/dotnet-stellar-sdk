using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <inheritdoc />
    /// <summary>
    ///     Sends an amount in a specific asset to a destination account.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    ///     <seealso cref="T:stellar_dotnetcore_sdk.requests.OperationsRequestBuilder" />
    ///     <seealso cref="T:stellar_dotnetcore_sdk.Server" />
    /// </summary>
    public class PaymentOperationResponse : OperationResponse
    {
        public PaymentOperationResponse()
        {

        }

        /// <summary>
        /// Sends an amount in a specific asset to a destination account.
        /// </summary>
        /// <param name="amount">Amount of the aforementioned asset to send.</param>
        /// <param name="assetType">The asset type (USD, BTC, etc.)</param>
        /// <param name="assetCode">The asset code (Alpha4, Alpha12, etc.)</param>
        /// <param name="assetIssuer">The account that created the asset</param>
        /// <param name="from">Account address that is sending the payment.</param>
        /// <param name="to">Account address that receives the payment.</param>
        public PaymentOperationResponse(string amount, string assetType, string assetCode, string assetIssuer, string from, string to)
        {
            Amount = amount;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            From = from;
            To = to;
        }

        public override int TypeId => 1;

        /// <summary>
        /// Amount of the aforementioned asset to send.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        /// <summary>
        /// The asset type (USD, BTC, etc.)
        /// </summary>
        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        /// <summary>
        /// The asset code (Alpha4, Alpha12, etc.)
        /// </summary>
        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        /// <summary>
        /// Account address that is sending the payment.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public string From { get; private set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public string To { get; private set; }

        /// <summary>
        /// Account address that receives the payment.
        /// </summary>
        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}
