using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;

namespace stellar_dotnetcore_sdk.requests
{
    /// <summary>
    /// Builds requests connected to trades.
    /// </summary>
    public class TradesRequestBuilder : RequestBuilder<TradesRequestBuilder>
    {
        public TradesRequestBuilder(Uri serverUri)
            : base(serverUri, "trades")
        {
        }

        public TradesRequestBuilder BaseAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("base_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                _uriBuilder.SetQueryParam("base_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("base_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }
            return this;
        }

        public TradesRequestBuilder OfferId(string offerId)
        {
            _uriBuilder.SetQueryParam("offer_id", offerId);
            return this;
        }

        public TradesRequestBuilder CounterAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("counter_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                _uriBuilder.SetQueryParam("counter_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("counter_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
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
