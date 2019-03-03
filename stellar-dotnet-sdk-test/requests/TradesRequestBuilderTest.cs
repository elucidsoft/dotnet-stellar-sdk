using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class TradesRequestBuilderTest
    {
        [TestMethod]
        public void TestTrades()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Trades
                .BaseAsset(Asset.CreateNonNativeAsset("EUR", "GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W"))
                .CounterAsset(Asset.CreateNonNativeAsset("USD", "GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ"))
                .Cursor("13537736921089")
                .Limit(200)
                .Order(OrderDirection.ASC)
                .BuildUri();

            Assert.AreEqual("https://horizon-testnet.stellar.org/trades?" +
                            "base_asset_type=credit_alphanum4&" +
                            "base_asset_code=EUR&" +
                            "base_asset_issuer=GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W&" +
                            "counter_asset_type=credit_alphanum4&" +
                            "counter_asset_code=USD&" +
                            "counter_asset_issuer=GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ&" +
                            "cursor=13537736921089&" +
                            "limit=200&" +
                            "order=asc", uri.ToString());
        }

        [TestMethod]
        public async Task TestTradesExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "tradesPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var trades = await server.Trades
                    .BaseAsset(new AssetTypeCreditAlphaNum4("EUR", "GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W"))
                    .CounterAsset(new AssetTypeCreditAlphaNum4("USD", "GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ"))
                    .Execute();

                TradesPageDeserializerTest.AssertTestData(trades);
            }
        }

        [TestMethod]
        public void TestTradesForAccount()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Trades
                    .ForAccount("GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ")
                    .Cursor("13537736921089")
                    .Limit(200)
                    .Order(OrderDirection.ASC)
                    .BuildUri();

            Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ/trades?" +
                    "cursor=13537736921089&" +
                    "limit=200&" +
                    "order=asc", uri.ToString());
        }

    }
}