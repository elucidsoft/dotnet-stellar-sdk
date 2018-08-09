using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class OfferResponseLinks
    {
        public OfferResponseLinks(Link self, Link offerMager)
        {
            Self = self;
            OfferMager = offerMager;
        }

        [JsonProperty(PropertyName = "self")] public Link Self { get; private set; }

        [JsonProperty(PropertyName = "offer_maker")]
        public Link OfferMager { get; private set; }
    }
}