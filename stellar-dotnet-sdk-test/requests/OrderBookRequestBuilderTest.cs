using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class OrderBookRequestBuilderTest
    {
        [TestMethod]
        public void TestOrderBook()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.OrderBook
                    .BuyingAsset(Asset.CreateNonNativeAsset("EUR", "GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W"))
                    .SellingAsset(Asset.CreateNonNativeAsset("USD", "GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ"))
                    .BuildUri();

                Assert.AreEqual(
                    "https://horizon-testnet.stellar.org/order_book?" +
                    "buying_asset_type=credit_alphanum4&" +
                    "buying_asset_code=EUR&" +
                    "buying_asset_issuer=GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W&" +
                    "selling_asset_type=credit_alphanum4&" +
                    "selling_asset_code=USD&" +
                    "selling_asset_issuer=GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ",
                    uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestOrderBookExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "orderBook.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var orderBookPage = await server.OrderBook
                    .BuyingAsset(new AssetTypeNative())
                    .SellingAsset(new AssetTypeCreditAlphaNum4("DEMO", "GC3BVJOU7SHHFLZ2LDYW6JU4YW36R2MRF6C37QJWQXZWG3JBYNODGHOB"))
                    .Execute();

                OrderBookDeserializerTest.AssertTestData(orderBookPage);
            }
        }
    }
}