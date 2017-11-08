using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_sdk.requests
{
    /// <summary>
    /// Builds requests connected to trades.
    /// </summary>
    public class TradesRequestBuilder : RequestBuilder<TradesRequestBuilder>
    {
        public TradesRequestBuilder(Uri serverUri)
            : base(serverUri, "order_book/trades")
        {
        }

        public TradesRequestBuilder BuyingAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("buying_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                _uriBuilder.SetQueryParam("buying_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("buying_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
            return this;
        }

        public TradesRequestBuilder SellingAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("selling_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                _uriBuilder.SetQueryParam("selling_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("selling_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
            return this;
        }

        public static async Task<TradeResponse> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<TradeResponse>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }

        }

        public Task<TradeResponse> Execute()
        {
            return Execute(BuildUri());
        }
    }
}
