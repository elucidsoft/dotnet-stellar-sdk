using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    /// <inheritdoc />
    public class OffersRequestBuilder : RequestBuilderExecuteable<OffersRequestBuilder, OfferResponse>
    {
        public OffersRequestBuilder(Uri serverURI, HttpClient httpClient) :
            base(serverURI, "offers", httpClient)
        { }

        /// <summary>
        /// Builds request to GET /accounts/{account}/offers
        /// See: https://www.stellar.org/developers/horizon/reference/offers-for-account.html
        /// </summary>
        /// <param name="account">Account for which to get offers</param>
        /// <returns></returns>
        public OffersRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "offers");
            return this;
        }
    }
}

