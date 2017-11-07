using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    /// <summary>
    /// Represents trades response.
    /// See: https://www.stellar.org/developers/horizon/reference/endpoints/trades-for-orderbook.html
    /// <seealso cref="TradesRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class TradeResponse : Response
    {
        public TradeResponse(string id, string pagingToken, string createdAt, KeyPair seller, string soldAmount, string soldAssetType, string soldAssetCode, string soldAssetIssuer, KeyPair buyer, string boughtAmount, string boughtAssetType, string boughtAssetCode, string boughtAssetIssuer)
        {
            Id = id;
            PagingToken = pagingToken;
            CreatedAt = createdAt;
            Seller = seller;
            SoldAmount = soldAmount;
            SoldAssetType = soldAssetType;
            SoldAssetCode = soldAssetCode;
            SoldAssetIssuer = soldAssetIssuer;
            Buyer = buyer;
            BoughtAmount = boughtAmount;
            BoughtAssetType = boughtAssetType;
            BoughtAssetCode = boughtAssetCode;
            BoughtAssetIssuer = boughtAssetIssuer;
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; }

        [JsonProperty(PropertyName = "seller")]
        public KeyPair Seller { get; }

        [JsonProperty(PropertyName = "sold_amount")]
        public string SoldAmount { get; }

        [JsonProperty(PropertyName = "sold_asset_type")]
        public string SoldAssetType { get; }

        [JsonProperty(PropertyName = "sold_asset_code")]
        public string SoldAssetCode { get; }

        [JsonProperty(PropertyName = "sold_asset_issuer")]
        public string SoldAssetIssuer { get; }

        [JsonProperty(PropertyName = "buyer")]
        public KeyPair Buyer { get; }

        [JsonProperty(PropertyName = "bought_amount")]
        public string BoughtAmount { get; }

        [JsonProperty(PropertyName = "bought_asset_type")]
        public string BoughtAssetType { get; }

        [JsonProperty(PropertyName = "bought_asset_code")]
        public string BoughtAssetCode { get; }

        [JsonProperty(PropertyName = "bought_asset_issuer")]
        public string BoughtAssetIssuer { get; }


        [JsonProperty(PropertyName = "_links")]
        public TradeResponseLinks Links { get; }
    }

    public class TradeResponseLinks
    {
        public TradeResponseLinks(Link self, Link seller, Link buyer)
        {
            Self = self;
            Seller = seller;
            Buyer = buyer;
        }

        [JsonProperty(PropertyName = "self")]
        public Link Self { get; }

        [JsonProperty(PropertyName = "seller")]
        public Link Seller { get; }

        [JsonProperty(PropertyName = "buyer")]
        public Link Buyer { get; }
    }
}