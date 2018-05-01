using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace stellar_dotnetcore_sdk.requests
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetsRequestBuilder : RequestBuilder<AssetsRequestBuilder>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverUri"></param>
        public AssetsRequestBuilder(Uri serverUri) : base(serverUri, "assets")
        {
        }

        public static async Task<Page<AssetResponse>> Execute(Uri uri)
        {
            var responseHandler = new ResponseHandler<Page<AssetResponse>>();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                return await responseHandler.HandleResponse(response);
            }
        }

        /// <summary>
        /// Code of the Asset to filter by
        /// </summary>
        /// <param name="assetCode"></param>
        /// <returns></returns>
        public AssetsRequestBuilder AssetCode(string assetCode)
        {
            base._uriBuilder.SetQueryParam("asset_code", assetCode);
            return this;
        }

        /// <summary>
        /// Issuer of the Asset to filter by
        /// </summary>
        /// <param name="assetIssuer"></param>
        /// <returns></returns>
        public AssetsRequestBuilder AssetIssuer(string assetIssuer)
        {
            base._uriBuilder.SetQueryParam("asset_issuer", assetIssuer);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Page<AssetResponse>> Execute()
        {
            return await Execute(BuildUri());
        }


        //Don't think these are needed. Polymorphism and all that via the base class.
        //public override AssetsRequestBuilder Cursor(string token)
        //{
        //    base.Cursor(token);
        //    return this;
        //}

        //public override AssetsRequestBuilder Limit(int number)
        //{
        //    base.Limit(number);
        //    return this;
        //}

        //public override AssetsRequestBuilder Order(OrderDirection direction)
        //{
        //    base.Order(direction);
        //    return this;
        //}
    }
}
