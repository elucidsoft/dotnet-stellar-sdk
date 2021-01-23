using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language;
using stellar_dotnet_sdk.federation;
using static stellar_dotnet_sdk_test.FederationServerTest;

namespace stellar_dotnet_sdk_test.federation
{
    [TestClass]
    public class FederationTest
    {
        private const string StellarToml = "FEDERATION_SERVER = \"https://api.stellar.org/federation\"";
        private const string SuccessResponse = "{\"stellar_address\":\"bob*stellar.org\",\"account_id\":\"GCW667JUHCOP5Y7KY6KGDHNPHFM4CS3FCBQ7QWDUALXTX3PGXLSOEALY\"}";

        private const HttpStatusCode HttpOk = HttpStatusCode.OK;

        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;
        private FederationServer _server;

        [TestInitialize]
        public void Setup()
        {
            _server = new FederationServer("https://api.stellar.org/federation", "stellar.org");

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

        private ISetupSequentialResult<HttpResponseMessage> When()
        {
            return _fakeHttpMessageHandler.SetupSequence(a => a.Send(It.IsAny<HttpRequestMessage>()));
        }

        private HttpResponseMessage ResponseMessage(HttpStatusCode statusCode, string content)
        {
            return new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            };
        }

        [TestMethod]
        public async Task TestResolveSuccess()
        {
            When()
                .Returns(ResponseMessage(HttpOk, StellarToml))
                .Returns(ResponseMessage(HttpOk, SuccessResponse));

            var response = await Federation.Resolve("bob*stellar.org");
            Assert.AreEqual(response.StellarAddress, "bob*stellar.org");
            Assert.AreEqual(response.AccountId, "GCW667JUHCOP5Y7KY6KGDHNPHFM4CS3FCBQ7QWDUALXTX3PGXLSOEALY");
            Assert.IsNull(response.MemoType);
            Assert.IsNull(response.Memo);
        }

        [TestMethod]
        [ExpectedException(typeof(MalformedAddressException))]
        public async Task TestMalformedAddress()
        {
            var unused = await Federation.Resolve("bob*stellar.org*test");
        }
    }
}