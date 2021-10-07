using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolsRequestBuilder : RequestBuilderStreamable<LiquidityPoolsRequestBuilder, LiquidityPoolResponse>
    {
        public const string RESERVES_PARAMETER_NAME = "reserves";

        public LiquidityPoolsRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "liquidity_pools", httpClient)
        {
        }

        public async Task<LiquidityPoolResponse> LiquidityPool(Uri uri)
        {
            var responseHandler = new ResponseHandler<LiquidityPoolResponse>();

            var response = await HttpClient.GetAsync(uri);
            return await responseHandler.HandleResponse(response);
        }

        public async Task<LiquidityPoolResponse> LiquidityPool(string liquidityPoolID)
        {
            SetSegments("liquidity_pools", liquidityPoolID);
            return await LiquidityPool(BuildUri());
        }

        public async Task<LiquidityPoolResponse> LiquidityPool(LiquidityPoolID liquidityPoolID)
        {
            return await LiquidityPool(liquidityPoolID.ToString());
        }

        public LiquidityPoolsRequestBuilder ForReserves(params string[] reserves)
        {
            UriBuilder.SetQueryParam(RESERVES_PARAMETER_NAME, string.Join(",", reserves));
            return this;
        }
    }
}
