using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trade effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TradeEffectResponse : EffectResponse
    {
        public TradeEffectResponse()
        {

        }

        /// <inheritdoc />
        public TradeEffectResponse(KeyPair seller, long offerId, string soldAmount, string soldAssetType, string soldAssetCode,
            string soldAssetIssuer, string boughtAmount, string boughtAssetType, string boughtAssetCode, string boughtAssetIssuer)
        {
            Seller = seller;
            OfferId = offerId;
            SoldAmount = soldAmount;
            SoldAssetType = soldAssetType;
            SoldAssetCode = soldAssetCode;
            SoldAssetIssuer = soldAssetIssuer;
            BoughtAmount = boughtAmount;
            BoughtAssetType = boughtAssetType;
            BoughtAssetCode = boughtAssetCode;
            BoughtAssetIssuer = boughtAssetIssuer;
        }

        public override int TypeId => 33;

        [JsonProperty(PropertyName = "seller")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair Seller { get; private set; }

        [JsonProperty(PropertyName = "offer_id")]
        public long OfferId { get; private set; }

        [JsonProperty(PropertyName = "sold_amount")]
        public string SoldAmount { get; private set; }

        [JsonProperty(PropertyName = "sold_asset_type")]
        public string SoldAssetType { get; private set; }

        [JsonProperty(PropertyName = "sold_asset_code")]
        public string SoldAssetCode { get; private set; }

        [JsonProperty(PropertyName = "sold_asset_issuer")]
        public string SoldAssetIssuer { get; private set; }

        [JsonProperty(PropertyName = "bought_amount")]
        public string BoughtAmount { get; private set; }

        [JsonProperty(PropertyName = "bought_asset_type")]
        public string BoughtAssetType { get; private set; }

        [JsonProperty(PropertyName = "bought_asset_code")]
        public string BoughtAssetCode { get; private set; }

        [JsonProperty(PropertyName = "bought_asset_issuer")]
        public string BoughtAssetIssuer { get; private set; }

        public Asset BoughtAsset => Asset.CreateNonNativeAsset(BoughtAssetType, BoughtAssetIssuer, BoughtAssetCode);

        public Asset SoldAsset => Asset.CreateNonNativeAsset(SoldAssetType, SoldAssetIssuer, SoldAssetCode);
    }
}