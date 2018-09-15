using Newtonsoft.Json;
using stellar_dotnet_sdk.requests;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Represents offer response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/offer.html
    /// <seealso cref="OffersRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class OfferResponse : Response, IPagingToken
    {
        public OfferResponse(long id, string pagingToken, string seller, Asset selling, Asset buying, string amount, string price, OfferResponseLinks links)
        {
            Id = id;
            PagingToken = pagingToken;
            Seller = seller;
            Selling = selling;
            Buying = buying;
            Amount = amount;
            Price = price;
            Links = links;
        }

        [JsonProperty(PropertyName = "id")] public long Id { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }

        [JsonProperty(PropertyName = "seller")]
        public string Seller { get; private set; }

        [JsonProperty(PropertyName = "selling")]
        public Asset Selling { get; private set; }

        [JsonProperty(PropertyName = "buying")]
        public Asset Buying { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        [JsonProperty(PropertyName = "price")] public string Price { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public OfferResponseLinks Links { get; private set; }
    }
}