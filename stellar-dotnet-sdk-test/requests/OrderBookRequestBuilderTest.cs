using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class OrderBookRequestBuilderTest
    {
        [TestMethod]
        public void TestOrderBook()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.OrderBook
                    .BuyingAsset(Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W")))
                    .SellingAsset(Asset.CreateNonNativeAsset("USD", KeyPair.FromAccountId("GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ")))
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
    }
}
