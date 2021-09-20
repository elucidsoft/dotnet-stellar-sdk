using stellar_dotnet_sdk.requests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolsRequestBuilder : RequestBuilder<LiquidityPoolsRequestBuilder>
    {
        public LiquidityPoolsRequestBuilder(Uri serverUri, HttpClient httpClient)
            : base(serverUri, "liquidity_pools", httpClient)
        {
        }

        public LiquidityPoolResponse LiquidityPool(Uri httpURL)
        {
            ResponseHandler<LiquidityPoolResponse> responseHandler = new ResponseHandler<LiquidityPoolResponse>();
        }
    }
}
