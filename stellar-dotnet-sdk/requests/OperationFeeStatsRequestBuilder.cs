using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk.requests
{
    public class OperationFeeStatsRequestBuilder : RequestBuilder<OperationFeeStatsRequestBuilder>
    {
        public OperationFeeStatsRequestBuilder(Uri serverURI, HttpClient httpClient)
            : base(serverURI, "operation_fee_stats", httpClient)
        {
        }

        public async Task<OperationFeeStatsResponse> Execute()
        {
            return await Execute<OperationFeeStatsResponse>(BuildUri());
        }
    }
}
