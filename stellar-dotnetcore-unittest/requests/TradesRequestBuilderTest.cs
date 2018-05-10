﻿using System;
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
                .BaseAsset(Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GAUPA4HERNBDPVO4IUA3MJXBCRRK5W54EVXTDK6IIUTGDQRB6D5W242W")))
                .CounterAsset(Asset.CreateNonNativeAsset("USD", KeyPair.FromAccountId("GDRRHSJMHXDTQBT4JTCILNGF5AS54FEMTXL7KOLMF6TFTHRK6SSUSUZZ")))
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
    }
}
