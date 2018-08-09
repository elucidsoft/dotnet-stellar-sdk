using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk_test
{
    public abstract class FederationServerTest
    {
        public abstract class FakeHttpMessageHandler : HttpMessageHandler
        {
            public Uri RequestUri { get; private set; }

            public virtual HttpResponseMessage Send(HttpRequestMessage request)
            {
                throw new NotImplementedException();
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                RequestUri = request.RequestUri;
                return await Task.FromResult(Send(request));
            }
        }
    }
}