using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents CreatePassiveOffer operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class CreatePassiveOfferOperationResponse : OperationResponse
    {
        public CreatePassiveOfferOperationResponse(int offerId, string amount, string price, string buyingAssetType, string buyingAssetCode, string buyingAssetIssuer, string sellingAssetType, string sellingAssetCode, string sellingAssetIssuer)
        {
            OfferId = offerId;
            Amount = amount;
            Price1 = price;
            BuyingAssetType = buyingAssetType;
            BuyingAssetCode = buyingAssetCode;
            BuyingAssetIssuer = buyingAssetIssuer;
            SellingAssetType = sellingAssetType;
            SellingAssetCode = sellingAssetCode;
            SellingAssetIssuer = sellingAssetIssuer;
        }

        [JsonProperty(PropertyName = "offer_id")]
        public int OfferId { get; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; }

        // Price is not implemented yet in horizon
        [JsonProperty(PropertyName = "price")] public string Price1 { get; }

        [JsonProperty(PropertyName = "buying_asset_type")]
        public string BuyingAssetType { get; }

        [JsonProperty(PropertyName = "buying_asset_code")]
        public string BuyingAssetCode { get; }

        [JsonProperty(PropertyName = "buying_asset_issuer")]
        public string BuyingAssetIssuer { get; }

        [JsonProperty(PropertyName = "selling_asset_type")]
        public string SellingAssetType { get; }

        [JsonProperty(PropertyName = "selling_asset_code")]
        public string SellingAssetCode { get; }

        [JsonProperty(PropertyName = "selling_asset_issuer")]
        public string SellingAssetIssuer { get; }

        public Asset BuyingAsset => Asset.CreateNonNativeAsset(BuyingAssetType, BuyingAssetIssuer, BuyingAssetCode);

        public Asset SellingAsset => Asset.CreateNonNativeAsset(SellingAssetType, SellingAssetIssuer, SellingAssetCode);
    }
}