using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations;

/// <summary>
/// Represents CreatePassiveOffer operation response.
/// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
/// <seealso cref="requests.OperationsRequestBuilder"/>
/// <seealso cref="Server"/>
/// </summary>
public class CreatePassiveOfferOperationResponse : OperationResponse
{
    public CreatePassiveOfferOperationResponse()
    {

    }

    public CreatePassiveOfferOperationResponse(int offerId, string amount, string price, string buyingAssetType, string buyingAssetCode, string buyingAssetIssuer, string sellingAssetType, string sellingAssetCode, string sellingAssetIssuer)
    {
        OfferId = offerId;
        Amount = amount;
        Price = price;
        BuyingAssetType = buyingAssetType;
        BuyingAssetCode = buyingAssetCode;
        BuyingAssetIssuer = buyingAssetIssuer;
        SellingAssetType = sellingAssetType;
        SellingAssetCode = sellingAssetCode;
        SellingAssetIssuer = sellingAssetIssuer;
    }

    public override int TypeId => 4;

    [JsonProperty(PropertyName = "offer_id")]
    public int OfferId { get; private set; }

    [JsonProperty(PropertyName = "amount")]
    public string Amount { get; private set; }

    /// <summary>
    ///     The ask price as a ratio.
    /// </summary>
    [JsonProperty(PropertyName = "price_r")]
    public Price PriceRatio { get; private set; }

    /// <summary>
    ///     The ask price.
    /// </summary>
    [JsonProperty(PropertyName = "price")]
    public string Price { get; private set; }

    [JsonProperty(PropertyName = "buying_asset_type")]
    public string BuyingAssetType { get; private set; }

    [JsonProperty(PropertyName = "buying_asset_code")]
    public string BuyingAssetCode { get; private set; }

    [JsonProperty(PropertyName = "buying_asset_issuer")]
    public string BuyingAssetIssuer { get; private set; }

    [JsonProperty(PropertyName = "selling_asset_type")]
    public string SellingAssetType { get; private set; }

    [JsonProperty(PropertyName = "selling_asset_code")]
    public string SellingAssetCode { get; private set; }

    [JsonProperty(PropertyName = "selling_asset_issuer")]
    public string SellingAssetIssuer { get; private set; }

    public Asset BuyingAsset => Asset.Create(BuyingAssetType, BuyingAssetCode, BuyingAssetIssuer);

    public Asset SellingAsset => Asset.Create(SellingAssetType, SellingAssetCode, SellingAssetIssuer);
}