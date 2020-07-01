using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk.requests
{
    public class FeeStatsRequestBuilder : RequestBuilder<FeeStatsRequestBuilder>
    {
        public FeeStatsRequestBuilder(Uri serverURI, HttpClient httpClient)
            : base(serverURI, "fee_stats", httpClient)
        {
        }

        public async Task<FeeStatsResponse> Execute()
        {
            return await Execute<FeeStatsResponse>(BuildUri());
        }
    }
}
