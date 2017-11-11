using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.operations
{
    /// <summary>
    /// Represents PathPayment operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class PathPaymentOperationResponse : OperationResponse
    {
        public PathPaymentOperationResponse(string amount, string sourceAmount, KeyPair from, KeyPair to, string assetType, string assetCode, string assetIssuer, string sendAssetType, string sendAssetCode, string sendAssetIssuer)
        {
            Amount = amount;
            SourceAmount = sourceAmount;
            From = from;
            To = to;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            SendAssetType = sendAssetType;
            SendAssetCode = sendAssetCode;
            SendAssetIssuer = sendAssetIssuer;
        }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; }

        [JsonProperty(PropertyName = "source_amount")]
        public string SourceAmount { get; }

        [JsonProperty(PropertyName = "from")]
        public KeyPair From { get; }

        [JsonProperty(PropertyName = "to")]
        public KeyPair To { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; }

        [JsonProperty(PropertyName = "send_asset_type")]
        public string SendAssetType { get; }

        [JsonProperty(PropertyName = "send_asset_code")]
        public string SendAssetCode { get; }

        [JsonProperty(PropertyName = "send_asset_issuer")]
        public string SendAssetIssuer { get; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);

        public Asset SendAsset => Asset.CreateNonNativeAsset(SendAssetType, SendAssetIssuer, SendAssetCode);
    }
}