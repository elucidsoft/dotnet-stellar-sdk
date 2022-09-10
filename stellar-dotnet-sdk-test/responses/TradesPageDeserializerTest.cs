using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses;

[TestClass]
public class TradesPageDeserializerTest
{
    [TestMethod]
    public void TestDeserialize()
    {
        var json = File.ReadAllText(Path.Combine("testdata", "tradesPage.json"));
        var tradesPage = JsonSingleton.GetInstance<Page<TradeResponse>>(json);

        AssertTestData(tradesPage);
    }

    [TestMethod]
    public void TestSerializeDeserialize()
    {
        var json = File.ReadAllText(Path.Combine("testdata", "tradesPage.json"));
        var tradesPage = JsonSingleton.GetInstance<Page<TradeResponse>>(json);
        var serialized = JsonConvert.SerializeObject(tradesPage);
        var back = JsonConvert.DeserializeObject<Page<TradeResponse>>(serialized)!;

        AssertTestData(back);
    }

    public static void AssertTestData(Page<TradeResponse> tradesPage)
    {
        Assert.AreEqual(tradesPage.Links.Next.Href, "https://horizon.stellar.org/trades?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&cursor=68836785177763841-0&limit=2&order=desc");
        Assert.AreEqual(tradesPage.Links.Prev.Href, "https://horizon.stellar.org/trades?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&cursor=68836918321750017-0&limit=2&order=asc");
        Assert.AreEqual(tradesPage.Links.Self.Href, "https://horizon.stellar.org/trades?base_asset_type=native&counter_asset_code=SLT&counter_asset_issuer=GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP&counter_asset_type=credit_alphanum4&cursor=&limit=2&order=desc");

        Assert.AreEqual(tradesPage.Records[0].Id, "68836918321750017-0");
        Assert.AreEqual(tradesPage.Records[0].PagingToken, "68836918321750017-0");

        Assert.AreEqual(tradesPage.Records[0].LedgerCloseTime, "2018-02-02T00:20:10Z");
        Assert.AreEqual(tradesPage.Records[0].OfferId, "695254");
        Assert.AreEqual(tradesPage.Records[0].BaseOfferId, "10");
        Assert.AreEqual(tradesPage.Records[0].CounterOfferId, "11");
        Assert.AreEqual(tradesPage.Records[0].BaseAccount, "GBZXCJIUEPDXGHMS64UBJHUVKV6ETWYOVHADLTBXJNJFUC7A7RU5B3GN");
        Assert.AreEqual(tradesPage.Records[0].BaseAmount, "0.1217566");
        Assert.AreEqual(tradesPage.Records[0].BaseAssetType, "native");
        Assert.AreEqual(tradesPage.Records[0].CounterAccount, "GBHKUQDYXGK5IEYORI7DZMMXANOIEHHOF364LNT4Q7EWPUL7FOO2SP6D");
        Assert.AreEqual(tradesPage.Records[0].CounterAmount, "0.0199601");
        Assert.AreEqual(tradesPage.Records[0].CounterAssetType, "credit_alphanum4");
        Assert.AreEqual(tradesPage.Records[0].CounterAssetCode, "SLT");
        Assert.AreEqual(tradesPage.Records[0].CounterAssetIssuer, "GCKA6K5PCQ6PNF5RQBF7PQDJWRHO6UOGFMRLK3DYHDOI244V47XKQ4GP");
        Assert.AreEqual(tradesPage.Records[0].BaseIsSeller, true);

        tradesPage.Records[0].Price.Numerator
            .Should().Be(10);

        tradesPage.Records[0].Price.Denominator
            .Should().Be(61);
    }
}