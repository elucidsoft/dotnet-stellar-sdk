using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    public class TradesAggregationRequestBuilder : RequestBuilder<TradesAggregationRequestBuilder>
    {
        public TradesAggregationRequestBuilder(Uri serverUri, Asset baseAsset, Asset counterAsset, long startTime, long endTime, long resolution) : base(serverUri, "trade_aggregations")
        {
            BaseAsset(baseAsset);
            CounterAsset(counterAsset);
            _uriBuilder.SetQueryParam("start_time", startTime.ToString());
            _uriBuilder.SetQueryParam("end_time", endTime.ToString());
            _uriBuilder.SetQueryParam("resolution", resolution.ToString());
        }

        public void BaseAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("base_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum)
            {
                AssetTypeCreditAlphaNum creditAlphaNumAsset = (AssetTypeCreditAlphaNum)asset;
                _uriBuilder.SetQueryParam("base_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("base_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
        }

        public void CounterAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("counter_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum)
            {
                AssetTypeCreditAlphaNum creditAlphaNumAsset = (AssetTypeCreditAlphaNum)asset;
                _uriBuilder.SetQueryParam("counter_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("counter_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<TradeAggregationResponse> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<TradeAggregationResponse>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<TradeAggregationResponse> Execute()
        {
            return Execute(BuildUri());
        }
    }
}
