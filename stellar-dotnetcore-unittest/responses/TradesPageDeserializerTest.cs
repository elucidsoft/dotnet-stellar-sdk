using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.responses;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class TradesPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "tradesPage.json"));
            var tradesPage = JsonSingleton.GetInstance<Page<TradeResponse>>(json);

            Assert.AreEqual(tradesPage.Links.Next.Href, "https://horizon.stellar.org/order_book/trades?order=asc&limit=10&cursor=43186647780560897-5");
            Assert.AreEqual(tradesPage.Links.Prev.Href, "https://horizon.stellar.org/order_book/trades?order=desc&limit=10&cursor=37129640086605825-1");
            Assert.AreEqual(tradesPage.Links.Self.Href, "https://horizon.stellar.org/order_book/trades?order=asc&limit=10&cursor=");

            Assert.AreEqual(tradesPage.Records[0].Id, "37129640086605825-1");
            Assert.AreEqual(tradesPage.Records[0].PagingToken, "37129640086605825-1");
            Assert.AreEqual(tradesPage.Records[1].Seller.AccountId, "GCI7ILB37OFVHLLSA74UCXZFCTPEBJOZK7YCNBI7DKH7D76U4CRJBL2A");
        }
    }
}
