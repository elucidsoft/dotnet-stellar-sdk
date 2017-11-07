using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.operations
{
    /// <summary>
    ///     Represents Payment operation response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    ///     <seealso cref="OperationRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class PaymentOperationResponse : OperationResponse
    {
        public PaymentOperationResponse(string amount, string assetType, string assetCode, string assetIssuer, KeyPair from, KeyPair to)
        {
            Amount = amount;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            From = from;
            To = to;
        }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; }

        [JsonProperty(PropertyName = "from")]
        public KeyPair From { get; }

        [JsonProperty(PropertyName = "to")]
        public KeyPair To { get; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}