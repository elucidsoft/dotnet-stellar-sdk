using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using Signer = stellar_dotnet_sdk.responses.Signer;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class ServerCheckMemoRequiredTest
    {
        private const HttpStatusCode HttpOk = HttpStatusCode.OK;
        private const HttpStatusCode HttpBadRequest = HttpStatusCode.BadRequest;

        private Mock<ServerTest.FakeHttpMessageHandler> _fakeHttpMessageHandler;
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

            _fakeHttpMessageHandler = new Mock<ServerTest.FakeHttpMessageHandler> { CallBase = true };
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);
            _server = new Server("https://horizon.stellar.org", _httpClient);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Network.Use(null);
            _httpClient.Dispose();
            _server.Dispose();
        }

        [TestMethod]
        public async Task TestFailsIfMemoIsRequired()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var data = new Dictionary<string, string>()
            {
                {"config.memo_required", "MQ=="},
            };
            var json = BuildAccountResponse(accountId, data);
            When().Returns(ServerTest.ResponseMessage(HttpOk, json));

            var tx = BuildTransaction(accountId);
            await Assert.ThrowsExceptionAsync<AccountRequiresMemoException>(() => _server.CheckMemoRequired(tx));
        }

        [TestMethod]
        public async Task TestItDoesNotThrowIfAccountDoesNotExists()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var json = BuildAccountResponse(accountId);
            When().Returns(ServerTest.ResponseMessage(HttpStatusCode.NotFound, json));

            var tx = BuildTransaction(accountId);
            await _server.CheckMemoRequired(tx);
        }

        [TestMethod]
        public async Task TestItDoesNotThrowIfAccountDoesNotHaveDataField()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var json = BuildAccountResponse(accountId);
            When().Returns(ServerTest.ResponseMessage(HttpStatusCode.OK, json));

            var tx = BuildTransaction(accountId);
            await _server.CheckMemoRequired(tx);
        }

        [TestMethod]
        public async Task TestRethrowClientException()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var json = BuildAccountResponse(accountId);
            When().Returns(ServerTest.ResponseMessage(HttpStatusCode.BadRequest, json));

            var tx = BuildTransaction(accountId);
            await Assert.ThrowsExceptionAsync<HttpResponseException>(() => _server.CheckMemoRequired(tx));
        }

        [TestMethod]
        public async Task TestDoesNotCheckDestinationMoreThanOnce()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var json = BuildAccountResponse(accountId);
            When().Returns(ServerTest.ResponseMessage(HttpStatusCode.OK, json));

            var payment = new PaymentOperation
                    .Builder(KeyPair.FromAccountId(accountId), new AssetTypeNative(), "100.500")
                .Build();

            var tx = BuildTransaction(accountId, new Operation[] { payment });
            await _server.CheckMemoRequired(tx);
        }

        [TestMethod]
        public async Task TestCheckOtherOperationTypes()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var destinations = new string[]
            {
                "GASGNGGXDNJE5C2O7LDCATIVYSSTZKB24SHYS6F4RQT4M4IGNYXB4TIV",
                "GBBM6BKZPEHWYO3E3YKREDPQXMS4VK35YLNU7NFBRI26RAN7GI5POFBB",
                "GCEZWKCA5VLDNRLN3RPRJMRZOX3Z6G5CHCGSNFHEYVXM3XOJMDS674JZ",
            };

            var native = new AssetTypeNative();
            var gbp = Asset.CreateNonNativeAsset("GBP", "GBBM6BKZPEHWYO3E3YKREDPQXMS4VK35YLNU7NFBRI26RAN7GI5POFBB");
            var eur = Asset.CreateNonNativeAsset("EUR", "GDTNXRLOJD2YEBPKK7KCMR7J33AAG5VZXHAJTHIG736D6LVEFLLLKPDL");

            var operations = new Operation[]
            {
                new AccountMergeOperation.Builder(KeyPair.FromAccountId(destinations[0])).Build(),
                new PathPaymentStrictSendOperation.Builder(native, "5.00", KeyPair.FromAccountId(destinations[1]),
                        native, "5.00")
                    .SetPath(new[] {gbp, eur})
                    .Build(),
                new PathPaymentStrictReceiveOperation.Builder(native, "5.00", KeyPair.FromAccountId(destinations[2]),
                        native, "5.00")
                    .SetPath(new[] {gbp, eur})
                    .Build(),
                new ChangeTrustOperation.Builder(gbp, "10000").Build(),
            };

            When()
                .Returns(ServerTest.ResponseMessage(HttpStatusCode.OK, BuildAccountResponse(accountId)))
                .Returns(ServerTest.ResponseMessage(HttpStatusCode.OK, BuildAccountResponse(destinations[0])))
                .Returns(ServerTest.ResponseMessage(HttpStatusCode.OK, BuildAccountResponse(destinations[1])))
                .Returns(ServerTest.ResponseMessage(HttpStatusCode.OK, BuildAccountResponse(destinations[2])));

            var tx = BuildTransaction(accountId, operations, Memo.Text("foobar"));
            await _server.CheckMemoRequired(tx);
        }

        [TestMethod]
        public async Task TestSkipCheckIfHasMemo()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var tx = BuildTransaction(accountId, new Operation[] { }, Memo.Text("foobar"));
            await _server.CheckMemoRequired(tx);
        }

        [TestMethod]
        public async Task TestCheckFeeBumpTransaction()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";
            var innerTx = BuildTransaction(accountId, new Operation[] { }, Memo.Text("foobar"));
            var feeSource = KeyPair.FromAccountId("GD7HCWFO77E76G6BKJLRHRFRLE6I7BMPJQZQKGNYTT3SPE6BA4DHJAQY");
            var tx = TransactionBuilder.BuildFeeBumpTransaction(feeSource, innerTx, 200);
            await _server.CheckMemoRequired(tx);
        }

        [TestMethod]
        public async Task TestSkipCheckIfDestinationIsMuxedAccount()
        {
            var accountId = "GAYHAAKPAQLMGIJYMIWPDWCGUCQ5LAWY4Q7Q3IKSP57O7GUPD3NEOSEA";

            var muxed = MuxedAccountMed25519.FromMuxedAccountId(
                "MAAAAAAAAAAAJURAAB2X52XFQP6FBXLGT6LWOOWMEXWHEWBDVRZ7V5WH34Y22MPFBHUHY");

            var payment = new PaymentOperation
                    .Builder(muxed, new AssetTypeNative(), "100.500")
                .Build();

            var tx = BuildTransaction(accountId, new Operation[] { payment }, Memo.None(), skipDefaultOp: true);
            await _server.CheckMemoRequired(tx);
        }

        private string BuildAccountResponse(string accountId, Dictionary<string, string> data = null)
        {
            var accountData = data ?? new Dictionary<string, string>();
            var response = new AccountResponse(accountId, 3298702387052545)
            {
                Balances = new[]
                {
                    new Balance("native", null, null, "12345.6789", null, "0.0", "0.0", false, true),
                },
                Data = accountData,
                Flags = new Flags(false, false, false),
                HomeDomain = null,
                InflationDestination = null,
                Signers = new[]
                {
                    new Signer(accountId, "ed25519_public_key", 1),
                },
                Thresholds = new Thresholds(0, 0, 0),
                Links = null,
            };
            return JsonConvert.SerializeObject(response);
        }

        private Transaction BuildTransaction(string destination)
        {
            return BuildTransaction(destination, new Operation[] { });
        }

        private Transaction BuildTransaction(string destinationAccountId, Operation[] operations, Memo memo = null, bool skipDefaultOp = false)
        {
            var keypair = KeyPair.Random();
            var destination = KeyPair.FromAccountId(destinationAccountId);
            var account = new AccountResponse(destinationAccountId, 56199647068161);
            var builder = new TransactionBuilder(account);
            if (!skipDefaultOp)
            {
                builder.AddOperation(
                    new PaymentOperation.Builder(destination, new AssetTypeNative(), "100.50")
                        .Build());
            }

            if (memo != null)
            {
                builder.AddMemo(memo);
            }

            foreach (var operation in operations)
            {
                builder.AddOperation(operation);
            }

            var tx = builder.Build();
            tx.Sign(keypair);
            return tx;
        }
    }
}