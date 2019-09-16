using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk_test.requests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Queue<FakeResponse> _responses = new Queue<FakeResponse>();

        // Requests that were sent via the handler
        private readonly List<HttpRequestMessage> _requests = new List<HttpRequestMessage>();

        public event EventHandler<HttpRequestMessage> RequestReceived;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (_responses.Count == 0)
                throw new InvalidOperationException("No response configured");

            RequestReceived?.Invoke(this, request);

            _requests.Add(request);

            var response = _responses.Dequeue();
            return Task.FromResult(response.MakeResponse(cancellationToken));
        }

        public void QueueResponse(FakeResponse response) => _responses.Enqueue(response);
        public IEnumerable<HttpRequestMessage> GetRequests() => _requests;
    }
}