using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk.requests
{
    public class
        PathStrictReceiveRequestBuilder : RequestBuilderExecutePageable<PathStrictReceiveRequestBuilder, PathResponse>
    {
        public PathStrictReceiveRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "paths/strict-receive", httpClient)
        {
        }

        public PathStrictReceiveRequestBuilder SourceAccount(string account)
        {
            UriBuilder.SetQueryParam("source_account", account);
            return this;
        }

        public PathStrictReceiveRequestBuilder DestinationAccount(string account)
        {
            UriBuilder.SetQueryParam("destination_account", account);
            return this;
        }

        public PathStrictReceiveRequestBuilder DestinationAmount(string amount)
        {
            UriBuilder.SetQueryParam("destination_amount", amount);
            return this;
        }

        public PathStrictReceiveRequestBuilder DestinationAsset(Asset asset)
        {
            UriBuilder.SetQueryParam("destination_asset_type", asset.GetType());

            if (asset is AssetTypeCreditAlphaNum)
            {
                AssetTypeCreditAlphaNum creditAlphaNumAsset = (AssetTypeCreditAlphaNum) asset;
                UriBuilder.SetQueryParam("destination_asset_code", creditAlphaNumAsset.Code);
                UriBuilder.SetQueryParam("destination_asset_issuer", creditAlphaNumAsset.Issuer);
            }

            return this;
        }

        public PathStrictReceiveRequestBuilder SourceAssets(IEnumerable<Asset> sources)
        {
            var encodedAssets = sources.Select(a => a.ToQueryParameterEncodedString());
            UriBuilder.SetQueryParam("source_assets", String.Join(",", encodedAssets));
            return this;
        }
    }
}