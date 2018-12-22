using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class TradeAggregationsPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "tradeAggregationsPage.json"));
            var tradeAggregationsPage = JsonSingleton.GetInstance<Page<TradeAggregationResponse>>(json);

            AssertTestData(tradeAggregationsPage);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "tradeAggregationsPage.json"));
            var tradeAggregationsPage = JsonSingleton.GetInstance<Page<TradeAggregationResponse>>(json);
            var serialized = JsonConvert.SerializeObject(tradeAggregationsPage);
            var back = JsonConvert.DeserializeObject<Page<TradeAggregationResponse>>(serialized);

            AssertTestData(back);
        }

        public static void AssertTestData(Page<TradeAggregationResponse> tradeAggregationsPage)
        {
            Assert.AreEqual(tradeAggregationsPage.Links.Next.Href,
                "https://horizon.stellar.org/trade_aggregations?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&end_time=1517532526000&limit=200&order=asc&resolution=3600000&start_time=1517529600000");
            Assert.AreEqual(tradeAggregationsPage.Links.Self.Href,
                "https://horizon.stellar.org/trade_aggregations?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&limit=200&order=asc&resolution=3600000&start_time=1517521726000&end_time=1517532526000");

            Assert.AreEqual(1517522400000, tradeAggregationsPage.Records[0].Timestamp);
            Assert.AreEqual(26, tradeAggregationsPage.Records[0].TradeCount);
            Assert.AreEqual("27575.0201596", tradeAggregationsPage.Records[0].BaseVolume);
            Assert.AreEqual("5085.6410385", tradeAggregationsPage.Records[0].CounterVolume);
            Assert.AreEqual("0.1844293", tradeAggregationsPage.Records[0].Avg);
            Assert.AreEqual("0.1915709", tradeAggregationsPage.Records[0].High);
            Assert.AreEqual("0.1506024", tradeAggregationsPage.Records[0].Low);
            Assert.AreEqual("0.1724138", tradeAggregationsPage.Records[0].Open);
            Assert.AreEqual("0.1506024", tradeAggregationsPage.Records[0].Close);
        }
    }
}