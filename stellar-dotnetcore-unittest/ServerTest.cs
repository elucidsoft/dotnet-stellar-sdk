using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.federation;
using stellar_dotnetcore_unittest.federation;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class ServerTest
    {
        private const HttpStatusCode HttpOk = HttpStatusCode.OK;

        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;
        private Server _server;

        private ISetupSequentialResult<HttpResponseMessage> When()
        {
            return _fakeHttpMessageHandler.SetupSequence(a => a.Send(It.IsAny<HttpRequestMessage>()));
        }

        [TestInitialize]
        public void Setup()
        {
            _server = new Server("https://horizon.stellar.org");

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
            _server.HttpClient = _httpClient;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
            _server.Dispose();
        }

        private HttpResponseMessage ResponseMessage(HttpStatusCode statusCode, string content)
        {
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            };
        }

        public class FakeHttpMessageHandler : HttpMessageHandler
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