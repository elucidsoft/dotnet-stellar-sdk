using System.Net;
using System.Net.Http;
using Moq;
using static stellar_dotnet_sdk_test.FederationServerTest;

namespace stellar_dotnet_sdk_test
{
    public static class FakeHttpClient
    {
        public static HttpClient CreateFakeHttpClient(string content)
        {
            var mockFakeHttpMesssageHandler = new Mock<FakeHttpMessageHandler> {CallBase = true};
            var httpClient = new HttpClient(mockFakeHttpMesssageHandler.Object);

            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(content)
            };

            httpResponseMessage.Headers.Add("X-Ratelimit-Limit", "-1");
            httpResponseMessage.Headers.Add("X-Ratelimit-Remaining", "-1");
            httpResponseMessage.Headers.Add("X-Ratelimit-Reset", "-1");

            mockFakeHttpMesssageHandler.Setup(a => a.Send(It.IsAny<HttpRequestMessage>())).Returns(httpResponseMessage);

            return httpClient;
        }
    }
}