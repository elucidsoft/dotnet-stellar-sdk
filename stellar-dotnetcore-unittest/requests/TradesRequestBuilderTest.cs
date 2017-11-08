using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_unittest.requests
{
    [TestClass]
    public class TradesRequestBuilderTest
    {
        [TestMethod]
        public void TestOrderBook()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Trades
                .BuyingAsset(Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W")))
                .SellingAsset(Asset.CreateNonNativeAsset("USD", KeyPair.FromAccountId("GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ")))
                .Cursor("13537736921089")
                .Limit(200)
                .Order(OrderDirection.ASC)
                .BuildUri();

            Assert.AreEqual("https://horizon-testnet.stellar.org/order_book/trades?" +
                         "buying_asset_type=credit_alphanum4&" +
                         "buying_asset_code=EUR&" +
                         "buying_asset_issuer=GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W&" +
                         "selling_asset_type=credit_alphanum4&" +
                         "selling_asset_code=USD&" +
                         "selling_asset_issuer=GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ&" +
                         "cursor=13537736921089&" +
                         "limit=200&" +
                         "order=asc", uri.ToString());
        }
    }
}
