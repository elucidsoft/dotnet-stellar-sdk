using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class AccountResponseLinks
    {
        public AccountResponseLinks(Link effects, Link offers, Link operations, Link self, Link transactions)
        {
            Effects = effects;
            Offers = offers;
            Operations = operations;
            Self = self;
            Transactions = transactions;
        }

        [JsonProperty(PropertyName = "effects")]
        public Link Effects { get; private set; }

        [JsonProperty(PropertyName = "offers")]
        public Link Offers { get; private set; }

        [JsonProperty(PropertyName = "operations")]
        public Link Operations { get; private set; }

        [JsonProperty(PropertyName = "self")] public Link Self { get; private set; }

        [JsonProperty(PropertyName = "transactions")]
        public Link Transactions { get; private set; }
    }
}