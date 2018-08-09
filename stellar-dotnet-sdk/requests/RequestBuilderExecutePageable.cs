using System;
using System.Net.Http;
using System.Threading.Tasks;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk.requests
{
    public class RequestBuilderExecutePageable<T, TResponse> : RequestBuilder<T> where T : class where TResponse : class
    {
        public RequestBuilderExecutePageable(Uri serverUri, string defaultSegment, HttpClient httpClient)
            : base(serverUri, defaultSegment, httpClient)
        {
        }


        ///<Summary>
        /// Build and execute request.
        /// </Summary>
        public async Task<Page<TResponse>> Execute()
        {
            return await Execute<Page<TResponse>>(BuildUri());
        }
    }
}