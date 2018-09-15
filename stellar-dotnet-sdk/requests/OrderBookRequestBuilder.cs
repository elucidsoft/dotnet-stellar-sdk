using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    public class OrderBookRequestBuilder : RequestBuilder<OrderBookRequestBuilder>
    {
        public OrderBookRequestBuilder(Uri serverURI, HttpClient httpClient)
            : base(serverURI, "order_book", httpClient)
        {
        }

        public OrderBookRequestBuilder BuyingAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("buying_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                UriBuilder.SetQueryParam("buying_asset_code", creditAlphaNumAsset.Code);
                UriBuilder.SetQueryParam("buying_asset_issuer", creditAlphaNumAsset.Issuer);
            }

            return this;
        }

        public OrderBookRequestBuilder SellingAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("selling_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                UriBuilder.SetQueryParam("selling_asset_code", creditAlphaNumAsset.Code);
                UriBuilder.SetQueryParam("selling_asset_issuer", creditAlphaNumAsset.Issuer);
            }

            return this;
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

        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<OrderBookResponse> Execute()
        {
            return await Execute<OrderBookResponse>(BuildUri());
        }
    }
}