using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    public class OrderBookRequestBuilder : RequestBuilder<OrderBookRequestBuilder>
    {
        public OrderBookRequestBuilder(Uri serverURI) : base(serverURI, "order_book")
        {
           
        }

        public OrderBookRequestBuilder BuyingAsset(Asset asset)
        { 
            _uriBuilder.SetQueryParam("buying_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                _uriBuilder.SetQueryParam("buying_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("buying_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
            return this;
        }

        public OrderBookRequestBuilder SellingAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("selling_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                _uriBuilder.SetQueryParam("selling_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("selling_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
            return this;
        }

        ///<Summary>
        /// Requests specific <code>uri</code> and returns {@link Page} of {@link EffectResponse}.
        /// This method is helpful for getting the next set of results.
        /// </Summary>
        public static async Task<Page<OrderBookResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<OrderBookResponse>>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }
        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<OrderBookResponse>> Execute()
        {
            return await Execute(BuildUri());
        }


        public override OrderBookRequestBuilder Cursor(string token)
        {
            throw new NotImplementedException();
        }


        public override OrderBookRequestBuilder Limit(int number)
        {
            throw new NotImplementedException();
        }


        public override OrderBookRequestBuilder Order(OrderDirection direction)
        {
            throw new NotImplementedException();
        }
    }
}
