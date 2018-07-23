using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using stellar_dotnet_sdk.federation;
using static stellar_dotnet_sdk_test.FederationServerTest;

namespace stellar_dotnet_sdk_test.federation
{
    [TestClass]
    public abstract partial class FederationServerTest
    {
        private const string SuccessResponse = "{\"stellar_address\":\"bob*stellar.org\",\"account_id\":\"GCW667JUHCOP5Y7KY6KGDHNPHFM4CS3FCBQ7QWDUALXTX3PGXLSOEALY\"}";
        private const string SuccessResponseWithMemo = "{\"stellar_address\":\"bob*stellar.org\",\"account_id\":\"GCW667JUHCOP5Y7KY6KGDHNPHFM4CS3FCBQ7QWDUALXTX3PGXLSOEALY\", \"memo_type\": \"text\", \"memo\": \"test\"}";
        private const string NotFoundResponse = "{\"code\":\"not_found\",\"message\":\"Account not found\"}";


        private const string StellarToml = "FEDERATION_SERVER = \"https://api.stellar.org/federation\"";
        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private HttpClient _httpClient;

        private const HttpStatusCode HttpNotFound = HttpStatusCode.NotFound;

        private const HttpStatusCode HttpOk = HttpStatusCode.OK;
        private FederationServer _server;

        [TestInitialize]
        public void Setup()
        {
            _server = new FederationServer("https://api.stellar.org/federation", "stellar.org");

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> {CallBase = true};
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
            _server.HttpClient = _httpClient;
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpClient.Dispose();
            _server.Dispose();
        }

        private void When(HttpStatusCode httpStatusCode, string content)
        {
            _fakeHttpMessageHandler.Setup(a => a.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = httpStatusCode,
                Content = new StringContent(content)
            });
        }

        [TestMethod]
        public async Task TestCreateForDomain()
        {
            When(HttpOk, StellarToml);

            using (var server = await FederationServer.CreateForDomain("stellar.org"))
            {

                Assert.AreEqual(server.ServerUri, "https://api.stellar.org/federation");
                Assert.AreEqual(server.Domain, "stellar.org");
            }

            _fakeHttpMessageHandler.Verify(a => a.Send(It.IsAny<HttpRequestMessage>()));

            Assert.AreEqual(new Uri("https://stellar.org/.well-known/stellar.toml"), _fakeHttpMessageHandler.Object.RequestUri);
        }

        [TestMethod]
        public async Task TestNameFederationSuccess()
        {
            When(HttpOk, SuccessResponse);

            var response = await _server.ResolveAddress("bob*stellar.org");
            Assert.AreEqual(response.StellarAddress, "bob*stellar.org");
            Assert.AreEqual(response.AccountId, "GCW667JUHCOP5Y7KY6KGDHNPHFM4CS3FCBQ7QWDUALXTX3PGXLSOEALY");
            Assert.IsNull(response.MemoType);
            Assert.IsNull(response.Memo);
        }

        [TestMethod]
        public async Task TestNameFederationSuccessWithMemo()
        {
            When(HttpOk, SuccessResponseWithMemo);

            var response = await _server.ResolveAddress("bob*stellar.org");
            Assert.AreEqual(response.StellarAddress, "bob*stellar.org");
            Assert.AreEqual(response.AccountId, "GCW667JUHCOP5Y7KY6KGDHNPHFM4CS3FCBQ7QWDUALXTX3PGXLSOEALY");
            Assert.AreEqual(response.MemoType, "text");
            Assert.AreEqual(response.Memo, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task TestNameFederationNotFound()
        {
            When(HttpNotFound, NotFoundResponse);

            var unused = await _server.ResolveAddress("bob*stellar.org");
        }
    }
}