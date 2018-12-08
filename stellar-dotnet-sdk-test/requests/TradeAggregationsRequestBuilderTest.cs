using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class TradeAggregationsRequestBuilderTest
    {
        [TestMethod]
        public void TestTradeAggregations()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.TradeAggregations
                .BaseAsset(new AssetTypeNative())
                .CounterAsset(Asset.CreateNonNativeAsset("BTC", KeyPair.FromAccountId("GATEMHCCKCY67ZUCKTROYN24ZYT5GK4EQZ65JJLDHKHRUZI3EUEKMTCH")))
                .StartTime(1512689100000L)
                .EndTime(1512775500000L)
                .Resolution(300000L)
                .Offset(3600L)
                .Limit(200)
                .Order(OrderDirection.ASC)
                .BuildUri();

            Assert.AreEqual(uri.ToString(), "https://horizon-testnet.stellar.org/trade_aggregations?" +
                                            "base_asset_type=native&" +
                                            "counter_asset_type=credit_alphanum4&" +
                                            "counter_asset_code=BTC&" +
                                            "counter_asset_issuer=GATEMHCCKCY67ZUCKTROYN24ZYT5GK4EQZ65JJLDHKHRUZI3EUEKMTCH&" +
                                            "start_time=1512689100000&" +
                                            "end_time=1512775500000&" +
                                            "resolution=300000&" +
                                            "offset=3600&" +
                                            "limit=200&" +
                                            "order=asc");
        }

        [TestMethod]
        public async Task TestTradeAggregationsExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "tradeAggregationsPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var account = await server.TradeAggregations
                    .BaseAsset(new AssetTypeNative())
                    .CounterAsset(new AssetTypeCreditAlphaNum4("BTC", KeyPair.FromAccountId("GATEMHCCKCY67ZUCKTROYN24ZYT5GK4EQZ65JJLDHKHRUZI3EUEKMTCH")))
                    .StartTime(1512689100000L)
                    .EndTime(1512775500000L)
                    .Resolution(300000L)
                    .Execute();

                TradeAggregationsPageDeserializerTest.AssertTestData(account);
            }
        }
    }
}