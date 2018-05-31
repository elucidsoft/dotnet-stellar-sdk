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
        /// <summary>
        /// Sends an amount in a specific asset to a destination account.
        /// </summary>
        /// <param name="amount">Amount of the aforementioned asset to send.</param>
        /// <param name="assetType">The asset type (USD, BTC, etc.)</param>
        /// <param name="assetCode">The asset code (Alpha4, Alpha12, etc.)</param>
        /// <param name="assetIssuer">The account that created the asset</param>
        /// <param name="from">Account address that is sending the payment.</param>
        /// <param name="to">Account address that receives the payment.</param>
        public PaymentOperationResponse(string amount, string assetType, string assetCode, string assetIssuer, KeyPair from, KeyPair to)
        {
            Amount = amount;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            From = from;
            To = to;
        }

        /// <summary>
        /// Amount of the aforementioned asset to send.
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; }

        /// <summary>
        /// The asset type (USD, BTC, etc.)
        /// </summary>
        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        /// <summary>
        /// The asset code (Alpha4, Alpha12, etc.)
        /// </summary>
        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; }

        /// <summary>
        /// Account address that is sending the payment.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public KeyPair From { get; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public KeyPair To { get; }

        /// <summary>
        /// Account address that receives the payment.
        /// </summary>
        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}