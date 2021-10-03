using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnet_sdk.requests;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class LiquidityPoolRequestBuilderTest
    {
        [TestMethod]
        public void TestLiquidityPools()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.LiquidityPools
                        .Cursor("13537736921089")
                        .Limit(200)
                        .Order(OrderDirection.ASC)
                        .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/liquidity_pools?cursor=13537736921089&limit=200&order=asc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForReserves()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.LiquidityPools
                        .ForReserves("EURT:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S", "PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S")
                        .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/liquidity_pools?reserves=EURT:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S,PHP:GAP5LETOV6YIE62YAM56STDANPRDO7ZFDBGSNHJQIYGGKSMOZAHOOS2S", uri.ToString());
            }
        }
    }
}
