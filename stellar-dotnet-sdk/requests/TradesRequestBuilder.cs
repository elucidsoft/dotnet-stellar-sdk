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
    public class TradesRequestBuilder : RequestBuilderExecutePageable<TradesRequestBuilder, TradeResponse>
    {
        public TradesRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "trades", httpClient)
        {
        }

        public TradesRequestBuilder BaseAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("base_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                UriBuilder.SetQueryParam("base_asset_code", creditAlphaNumAsset.Code);
                UriBuilder.SetQueryParam("base_asset_issuer", creditAlphaNumAsset.Issuer);
            }

            return this;
        }

        public TradesRequestBuilder OfferId(string offerId)
        {
            UriBuilder.SetQueryParam("offer_id", offerId);
            return this;
        }

        public TradesRequestBuilder CounterAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("counter_asset_type", asset.GetType());
            if (asset is AssetTypeCreditAlphaNum creditAlphaNumAsset)
            {
                UriBuilder.SetQueryParam("counter_asset_code", creditAlphaNumAsset.Code);
                UriBuilder.SetQueryParam("counter_asset_issuer", creditAlphaNumAsset.Issuer);
            }

            return this;
        }


        ///<Summary>
        /// Builds request to <code>GET /accounts/{account}/trades</code>
        /// <a href="https://www.stellar.org/developers/horizon/reference/endpoints/trades-for-account.html">Trades for Account</a>
        /// </Summary>
        /// <param name="account">Account for which to get trades</param> 
        public TradesRequestBuilder ForAccount(KeyPair account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account.AccountId, "trades");
            return this;
        }
    }
}