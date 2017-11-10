using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_sdk.requests
{
    /// <summary>
    /// Builds requests connected to paths.
    /// </summary>
    public class PathsRequestBuilder : RequestBuilder<PathsRequestBuilder>
    {
        public PathsRequestBuilder(Uri serverUri) 
            : base(serverUri, "paths")
        {
        }

        public PathsRequestBuilder DestinationAccount(KeyPair account)
        {
            _uriBuilder.SetQueryParam("destination_account", account.AccountId);
            return this;
        }

        public PathsRequestBuilder SourceAccount(KeyPair account)
        {
            _uriBuilder.SetQueryParam("source_account", account.AccountId);
            return this;
        }

        public PathsRequestBuilder DestinationAmount(string amount)
        {
            _uriBuilder.SetQueryParam("destination_amount", amount);
            return this;
        }

        public PathsRequestBuilder DestinationAsset(Asset asset)
        {
            _uriBuilder.SetQueryParam("destination_asset_type", asset.GetType());


            if (asset is AssetTypeCreditAlphaNum)
            {
                AssetTypeCreditAlphaNum creditAlphaNumAsset = (AssetTypeCreditAlphaNum) asset;
                _uriBuilder.SetQueryParam("destination_asset_code", creditAlphaNumAsset.Code);
                _uriBuilder.SetQueryParam("destination_asset_issuer", creditAlphaNumAsset.Issuer.AccountId);
            }

            return this;
        }

        public static async Task<Page<PathResponse>> Execute(Uri uri)
        {
            ResponseHandler<Page<PathResponse>> responseHandler = new ResponseHandler<Page<PathResponse>>();

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }
    }
}
