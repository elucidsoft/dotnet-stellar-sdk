using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    /// <summary>
    /// Represents offer response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/offer.html
    /// <seealso cref="OffersRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class OfferResponse : Response
    {
        public OfferResponse(long id, string pagingToken, KeyPair seller, Asset selling, Asset buying, string amount, string price, OfferResponseLinks links)
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

        [JsonProperty(PropertyName = "id")]
        public long Id { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }

        [JsonProperty(PropertyName = "seller")]
        public KeyPair Seller { get; private set; }

        [JsonProperty(PropertyName = "selling")]
        public Asset Selling { get; private set; }

        [JsonProperty(PropertyName = "buying")]
        public Asset Buying { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        [JsonProperty(PropertyName = "price")]
        public string Price { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public OfferResponseLinks Links { get; private set; }
    }

    public class OfferResponseLinks
    {
        public OfferResponseLinks(Link self, Link offerMager)
        {
            Self = self;
            OfferMager = offerMager;
        }

        [JsonProperty(PropertyName = "self")]
        public Link Self { get; private set; }

        [JsonProperty(PropertyName = "offer_maker")]
        public Link OfferMager { get; private set; }
    }
}