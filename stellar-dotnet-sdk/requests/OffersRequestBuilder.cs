using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk.requests
{
    /// <inheritdoc />
    public class OffersRequestBuilder : RequestBuilderExecutePageable<OffersRequestBuilder, OfferResponse>
    {
        public OffersRequestBuilder(Uri serverURI, HttpClient httpClient) :
            base(serverURI, "offers", httpClient)
        {
        }

        /// <summary>
        /// Builds request to GET /accounts/{account}/offers
        /// See: https://www.stellar.org/developers/horizon/reference/offers-for-account.html
        /// </summary>
        /// <param name="account">Account for which to get offers</param>
        /// <returns></returns>
        public OffersRequestBuilder ForAccount(string account)
        {
            account = account ?? throw new ArgumentNullException(nameof(account), "account cannot be null");
            this.SetSegments("accounts", account, "offers");
            return this;
        }

        /// <summary>
        /// Filter offers by seller, selling asset, or buying asset.
        /// https://www.stellar.org/developers/horizon/reference/endpoints/offers.html
        /// </summary>
        /// <param name="options">The filter options</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public OffersRequestBuilder Offers(OffersRequestOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.Seller != null)
            {
                UriBuilder.SetQueryParam("seller", options.Seller);
            }

            if (options.Selling != null)
            {
                AddAssetFilterQueryParam("selling", options.Selling);
            }

            if (options.Buying != null)
            {
                AddAssetFilterQueryParam("buying", options.Buying);
            }

            return this;
        }

        /// <summary>
        /// Filter offers by seller, selling asset, or buying asset.
        /// https://www.stellar.org/developers/horizon/reference/endpoints/offers.html
        /// </summary>
        /// <param name="optionsAction">The filter options</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public OffersRequestBuilder Offers(Action<OffersRequestOptions> optionsAction)
        {
            if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

            var options = new OffersRequestOptions();
            optionsAction.Invoke(options);
            return Offers(options);
        }

        /// <summary>
        /// Filter offers by seller account.
        /// </summary>
        /// <param name="seller">The seller account</param>
        /// <returns></returns>
        public OffersRequestBuilder WithSeller(string seller)
        {
            return Offers(options => options.Seller = seller);
        }

        /// <summary>
        /// Filter offers by selling asset.
        /// </summary>
        /// <param name="selling">The selling being sold</param>
        /// <returns></returns>
        public OffersRequestBuilder WithSellingAsset(Asset selling)
        {
            return Offers(options => options.Selling = selling);
        }

        /// <summary>
        /// Filter offers by buying asset.
        /// </summary>
        /// <param name="buying">The asset being bought</param>
        /// <returns></returns>
        public OffersRequestBuilder WithBuyingAsset(Asset buying)
        {
            return Offers(options => options.Buying = buying);
        }

        private void AddAssetFilterQueryParam(string side, Asset asset)
        {
            UriBuilder.SetQueryParam($"{side}_asset_type", asset.GetType());
            switch (asset)
            {
                case AssetTypeNative _:
                    return;
                case AssetTypeCreditAlphaNum credit:
                    UriBuilder.SetQueryParam($"{side}_asset_code", credit.Code);
                    UriBuilder.SetQueryParam($"{side}_asset_issuer", credit.Issuer);
                    return;
                default:
                    throw new ArgumentException($"Unknown Asset type {asset.GetType()}");
            }
        }

        public class OffersRequestOptions
        {
            /// <summary>
            /// Account ID of the offer creator.
            /// </summary>
            public string Seller { get; set; }

            /// <summary>
            /// The asset being sold.
            /// </summary>
            public Asset Selling { get; set; }

            /// <summary>
            /// The asset being bought.
            /// </summary>
            public Asset Buying { get; set; }
        }
    }
}