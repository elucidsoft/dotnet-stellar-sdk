﻿using System.IO;
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

            Assert.AreEqual(tradesPage.Links.Next.Href, "https://horizon.stellar.org/trades?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&cursor=68836785177763841-0&limit=2&order=desc");
            Assert.AreEqual(tradesPage.Links.Prev.Href, "https://horizon.stellar.org/trades?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&cursor=68836918321750017-0&limit=2&order=asc");
            Assert.AreEqual(tradesPage.Links.Self.Href, "https://horizon.stellar.org/trades?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&cursor=&limit=2&order=desc");

            Assert.AreEqual(tradesPage.Records[0].Id, "68836918321750017-0");
            Assert.AreEqual(tradesPage.Records[0].PagingToken, "68836918321750017-0");

            Assert.AreEqual(tradesPage.Records[0].LedgerCloseTime, "2018-02-02T00:20:10Z");
            Assert.AreEqual(tradesPage.Records[0].OfferId, "695254");
            Assert.AreEqual(tradesPage.Records[0].BaseAccount.AccountId, "GBZXCJIUEPDXGHMS64UBJHUVKV6ETWYOVHADLTBXJNJFUC7A7RU5B3GN");
            Assert.AreEqual(tradesPage.Records[0].BaseAmount, "0.1217566");
            Assert.AreEqual(tradesPage.Records[0].BaseAssetType, "native");
            Assert.AreEqual(tradesPage.Records[0].CounterAccount.AccountId, "GBHKUQDYXGK5IEYORI7DZMMXANOIEHHOF364LNT4Q7EWPUL7FOO2SP6D");
            Assert.AreEqual(tradesPage.Records[0].CounterAmount, "0.0199601");
            Assert.AreEqual(tradesPage.Records[0].CounterAssetType, "credit_alphanum4");
            Assert.AreEqual(tradesPage.Records[0].CounterAssetCode, "SLT");
            Assert.AreEqual(tradesPage.Records[0].CounterAssetIssuer, "GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP");
            Assert.AreEqual(tradesPage.Records[0].BaseIsSeller, true);



        }
    }
}
