using Newtonsoft.Json;
using stellar_dotnet_sdk.responses.effects;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk.responses
{
    public class AccountResponseLinks
    {
        public AccountResponseLinks(Link<Page<EffectResponse>> effects, Link<Page<OfferResponse>> offers,
            Link<Page<OperationResponse>> operations, Link<AccountResponse> self,
            Link<Page<TransactionResponse>> transactions)
        {
            Effects = effects;
            Offers = offers;
            Operations = operations;
            Self = self;
            Transactions = transactions;
        }

        [JsonProperty(PropertyName = "effects")]
        public Link<Page<EffectResponse>> Effects { get; private set; }

        [JsonProperty(PropertyName = "offers")]
        public Link<Page<OfferResponse>> Offers { get; private set; }

        [JsonProperty(PropertyName = "operations")]
        public Link<Page<OperationResponse>> Operations { get; private set; }

        [JsonProperty(PropertyName = "self")]
        public Link<AccountResponse> Self { get; private set; }

        [JsonProperty(PropertyName = "transactions")]
        public Link<Page<TransactionResponse>> Transactions { get; private set; }
    }
}