using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses;

[TestClass]
public class OfferPageDeserializerTest
{
    [TestMethod]
    public void TestDeserialize()
    {
        var json = File.ReadAllText(Path.Combine("testdata", "offerPage.json"));
        var offerResponsePage = JsonSingleton.GetInstance<Page<OfferResponse>>(json);

        AssertTestData(offerResponsePage);
    }

    [TestMethod]
    public void TestSerializeDeserialize()
    {
        var json = File.ReadAllText(Path.Combine("testdata", "offerPage.json"));
        var offerResponsePage = JsonSingleton.GetInstance<Page<OfferResponse>>(json);
        var serialized = JsonConvert.SerializeObject(offerResponsePage);
        var back = JsonConvert.DeserializeObject<Page<OfferResponse>>(serialized);

        AssertTestData(back);
    }

    //Before Horizon 1.0.0 the ID in the json was a long.
    [TestMethod]
    public void TestDeserializePre100()
    {
        var json = File.ReadAllText(Path.Combine("testdata", "offerPagePre100.json"));
        var offerResponsePage = JsonSingleton.GetInstance<Page<OfferResponse>>(json);

        AssertTestData(offerResponsePage);
    }

    //Before Horizon 1.0.0 the ID in the json was a long.
    [TestMethod]
    public void TestSerializeDeserializePre100()
    {
        var json = File.ReadAllText(Path.Combine("testdata", "offerPagePre100.json"));
        var offerResponsePage = JsonSingleton.GetInstance<Page<OfferResponse>>(json);
        var serialized = JsonConvert.SerializeObject(offerResponsePage);
        var back = JsonConvert.DeserializeObject<Page<OfferResponse>>(serialized);

        AssertTestData(back);
    }

    public static void AssertTestData(Page<OfferResponse> offerResponsePage)
    {
        Assert.AreEqual(offerResponsePage.Records[0].Id, "241");
        Assert.AreEqual(offerResponsePage.Records[0].Seller, "GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD");
        Assert.AreEqual(offerResponsePage.Records[0].PagingToken, "241");
        Assert.AreEqual(offerResponsePage.Records[0].Selling, Asset.CreateNonNativeAsset("INR", "GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD"));
        Assert.AreEqual(offerResponsePage.Records[0].Buying, Asset.CreateNonNativeAsset("USD", "GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD"));
        Assert.AreEqual(offerResponsePage.Records[0].Amount, "10.0000000");

        offerResponsePage.Records[0].Price
            .Should().Be("1.1000000");

        offerResponsePage.Records[0].PriceRatio.Numerator
            .Should().Be(11);

        offerResponsePage.Records[0].PriceRatio.Denominator
            .Should().Be(10);

        Assert.AreEqual(offerResponsePage.Records[0].LastModifiedLedger, 22200794);
        Assert.AreEqual(offerResponsePage.Records[0].LastModifiedTime, "2019-01-28T12:30:38Z");

        Assert.AreEqual(offerResponsePage.Links.Next.Href,
            "https://horizon-testnet.stellar.org/accounts/GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD/offers?order=asc&limit=10&cursor=241");
        Assert.AreEqual(offerResponsePage.Links.Prev.Href,
            "https://horizon-testnet.stellar.org/accounts/GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD/offers?order=desc&limit=10&cursor=241");
        Assert.AreEqual(offerResponsePage.Links.Self.Href,
            "https://horizon-testnet.stellar.org/accounts/GA2IYMIZSAMDD6QQTTSIEL73H2BKDJQTA7ENDEEAHJ3LMVF7OYIZPXQD/offers?order=asc&limit=10&cursor=");
    }
}