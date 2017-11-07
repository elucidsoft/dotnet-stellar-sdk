using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using System.IO;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class OrderBookDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "orderBook.json"));
            var orderBook = JsonSingleton.GetInstance<OrderBookResponse>(json);

            Assert.AreEqual(orderBook.OrderBookBase, new AssetTypeNative());
            Assert.AreEqual(orderBook.Counter, Asset.CreateNonNativeAsset("DEMO", KeyPair.FromAccountId("GBAMBOOZDWZPVV52RCLJQYMQNXOBLOXWNQAY2IF2FREV2WL46DBCH3BE")));

            Assert.AreEqual(orderBook.Bids[0].Amount, "31.4007644");
            Assert.AreEqual(orderBook.Bids[0].Price, "0.0024224");
            Assert.AreEqual(orderBook.Bids[0].PriceR.Numerator, 4638606);
            Assert.AreEqual(orderBook.Bids[0].PriceR.Denominator, 1914900241);

            Assert.AreEqual(orderBook.Bids[1].Amount, "5.9303650");
            Assert.AreEqual(orderBook.Bids[1].Price, "0.0024221");

            Assert.AreEqual(orderBook.Asks[0].Amount, "541.4550766");
            Assert.AreEqual(orderBook.Asks[0].Price, "0.0025093");

            Assert.AreEqual(orderBook.Asks[1].Amount, "121.9999600");
            Assert.AreEqual(orderBook.Asks[1].Price, "0.0025093");
        }

    }
}
