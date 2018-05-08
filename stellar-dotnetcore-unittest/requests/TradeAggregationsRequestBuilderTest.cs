using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest.requests
{
    [TestClass()]
    public class TradeAggregationsRequestBuilderTest
    {
        [TestMethod()]
        public void TestTradeAggregations()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.TradeAggregations(
                    new AssetTypeNative(),
                    Asset.CreateNonNativeAsset("BTC", KeyPair.FromAccountId("GATEMHCCKCY67ZUCKTROYN24ZYT5GK4EQZ65JJLDHKHRUZI3EUEKMTCH")),
                    1512689100000L,
                    1512775500000L,
                    300000L
            ).Limit(200).Order(OrderDirection.ASC).BuildUri();

            Assert.AreEqual(uri.ToString(), "https://horizon-testnet.stellar.org/trade_aggregations?" +
                    "base_asset_type=native&" +
                    "counter_asset_type=credit_alphanum4&" +
                    "counter_asset_code=BTC&" +
                    "counter_asset_issuer=GATEMHCCKCY67ZUCKTROYN24ZYT5GK4EQZ65JJLDHKHRUZI3EUEKMTCH&" +
                    "start_time=1512689100000&" +
                    "end_time=1512775500000&" +
                    "resolution=300000&" +
                    "limit=200&" +
                    "order=asc");

        }
    }
}
