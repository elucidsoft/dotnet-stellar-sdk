using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    public class OffersRequestBuilder : RequestBuilder<OffersRequestBuilder>
    {
        public OffersRequestBuilder(Uri serverURI) : base(serverURI, "offers")
        { }

        /**
         * Builds request to <code>GET /accounts/{account}/offers</code>
         * @see <a href="https://www.stellar.org/developers/horizon/reference/offers-for-account.html">Offers for Account</a>
         * @param account Account for which to get offers
         */
        public OffersRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "offers");
            return this;
        }


        ///<Summary>
        /// Requests specific <code>uri</code> and returns {@link Page} of {@link EffectResponse}.
        /// This method is helpful for getting the next set of results.
        /// </Summary>
        public static async Task<Page<OfferResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<OfferResponse>>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<OfferResponse>> Execute()
        {
            return await Execute(BuildUri());
        }


        public override RequestBuilder<OffersRequestBuilder> Cursor(String token)
        {
            base.Cursor(token);
            return this;
        }


        public override RequestBuilder<OffersRequestBuilder> Limit(int number)
        {
            base.Limit(number);
            return this;
        }


        public override RequestBuilder<OffersRequestBuilder> Order(OrderDirection direction)
        {
            base.Order(direction);
            return this;
        }
    }
}

