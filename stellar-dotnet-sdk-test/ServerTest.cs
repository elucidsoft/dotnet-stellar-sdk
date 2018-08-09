using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class ServerTest
    {
        private const HttpStatusCode HttpOk = HttpStatusCode.OK;
        private const HttpStatusCode HttpBadRequest = HttpStatusCode.BadRequest;

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
            Network.UseTestNetwork();

            _server = new Server("https://horizon.stellar.org");

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> {CallBase = true};
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            Server.HttpClient = _httpClient;
        }

        [TestCleanup]
        public void Cleanup()
        {
            Network.Use(null);
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

        public Transaction BuildTransaction()
        {
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source, 2908908335136768L);
            var builder = new Transaction.Builder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddMemo(Memo.Text("Hello world!"));

            Assert.AreEqual(1, builder.OperationsCount);
            var transaction = builder.Build();
            Assert.AreEqual(2908908335136769L, transaction.SequenceNumber);
            Assert.AreEqual(2908908335136769L, account.SequenceNumber);
            transaction.Sign(source);

            return transaction;
        }

        [TestMethod]
        public async Task TestSubmitTransactionSuccess()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "serverSuccess.json"));
            When().Returns(ResponseMessage(HttpOk, json));

            var response = await _server.SubmitTransaction(BuildTransaction());
            Assert.IsTrue(response.IsSuccess());
            Assert.AreEqual(response.Ledger, 826150L);
            Assert.AreEqual(response.Hash, "2634d2cf5adcbd3487d1df042166eef53830115844fdde1588828667bf93ff42");
            Assert.IsNull(response.SubmitTransactionResponseExtras);
        }

        [TestMethod]
        public async Task TestSubmitTransactionFail()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "serverFailure.json"));
            When().Returns(ResponseMessage(HttpBadRequest, json));

            var response = await _server.SubmitTransaction(BuildTransaction());
            Assert.IsFalse(response.IsSuccess());
            Assert.IsNull(response.Ledger);
            Assert.IsNull(response.Hash);
            Assert.AreEqual(response.SubmitTransactionResponseExtras.EnvelopeXdr, "AAAAAK4Pg4OEkjGmSN0AN37K/dcKyKPT2DC90xvjjawKp136AAAAZAAKsZQAAAABAAAAAAAAAAEAAAAJSmF2YSBGVFchAAAAAAAAAQAAAAAAAAABAAAAAG9wfBI7rRYoBlX3qRa0KOnI75W5BaPU6NbyKmm2t71MAAAAAAAAAAABMS0AAAAAAAAAAAEKp136AAAAQOWEjL+Sm+WP2puE9dLIxWlOibIEOz8PsXyG77jOCVdHZfQvkgB49Mu5wqKCMWWIsDSLFekwUsLaunvmXrpyBwQ=");
            Assert.AreEqual(response.SubmitTransactionResponseExtras.ResultXdr, "AAAAAAAAAGT/////AAAAAQAAAAAAAAAB////+wAAAAA=");
            Assert.IsNotNull(response.SubmitTransactionResponseExtras);
            Assert.AreEqual("tx_failed", response.SubmitTransactionResponseExtras.ExtrasResultCodes.TransactionResultCode);
            Assert.AreEqual("op_no_destination", response.SubmitTransactionResponseExtras.ExtrasResultCodes.OperationsResultCodes[0]);
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