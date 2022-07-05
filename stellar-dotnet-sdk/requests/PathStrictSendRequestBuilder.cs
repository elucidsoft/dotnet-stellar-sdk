using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk.requests
{
    public class
        PathStrictSendRequestBuilder : RequestBuilderExecutePageable<PathStrictSendRequestBuilder, PathResponse>
    {
        public PathStrictSendRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "paths/strict-send", httpClient)
        {
        }

        public PathStrictSendRequestBuilder DestinationAccount(string account)
        {
            UriBuilder.SetQueryParam("destination_account", account);
            return this;
        }

        public PathStrictSendRequestBuilder SourceAmount(string amount)
        {
            UriBuilder.SetQueryParam("source_amount", amount);
            return this;
        }

        public PathStrictSendRequestBuilder SourceAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("source_asset_type", asset.Type);

            if (asset is AssetTypeCreditAlphaNum)
            {
                AssetTypeCreditAlphaNum creditAlphaNumAsset = (AssetTypeCreditAlphaNum)asset;
                UriBuilder.SetQueryParam("source_asset_code", creditAlphaNumAsset.Code);
                UriBuilder.SetQueryParam("source_asset_issuer", creditAlphaNumAsset.Issuer);
            }

            return this;
        }

        public PathStrictSendRequestBuilder DestinationAssets(IEnumerable<Asset> sources)
        {
            var encodedAssets = sources.Select(a => a.ToQueryParameterEncodedString());
            UriBuilder.SetQueryParam("destination_assets", String.Join(",", encodedAssets));
            return this;
        }
    }
}