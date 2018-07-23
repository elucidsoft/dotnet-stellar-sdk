using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class LedgersRequestBuilderTest
    {
        [TestMethod]
        public void TestAccounts()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Ledgers
                    .Limit(200)
                    .Order(OrderDirection.ASC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers?limit=200&order=asc", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestLedgersExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "ledgerPage.json"));
            var fakeHttpClient = RequestBuilderMock.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var ledgersPage = await server.Ledgers
                    .Execute();

                LedgerPageDeserializerTest.AssertTestData(ledgersPage);
            }
        }
    }
}
