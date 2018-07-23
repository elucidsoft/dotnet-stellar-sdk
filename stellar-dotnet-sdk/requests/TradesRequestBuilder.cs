using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk.requests
{
    /// <summary>
    /// Builds requests connected to trades.
    /// </summary>
    public class TradesRequestBuilder : RequestBuilder<TradesRequestBuilder>
    {
        public TradesRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "trades", httpClient)
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

        public Task<Page<TradeResponse>> Execute()
        {
            return Execute<Page<TradeResponse>>(BuildUri());
        }
    }
}
