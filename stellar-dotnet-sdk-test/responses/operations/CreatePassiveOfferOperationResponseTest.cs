using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System.IO;
using FluentAssertions;

namespace stellar_dotnet_sdk_test.responses.operations;

[TestClass]
public class CreatePassiveOfferOperationResponseTest
{
    [TestMethod]
    public void TestDeserializeCreatePassiveOfferOperation()
    {
        var json = File.ReadAllText(Path.Combine("testdata/operations/createPassiveOffer", "createPassiveOffer.json"));
        var instance = JsonSingleton.GetInstance<CreatePassiveOfferOperationResponse>(json);

        AssertCreatePassiveOfferData(instance);
    }

    [TestMethod]
    public void TestSerializeDeserializeCreatePassiveOfferOperation()
    {
        var json = File.ReadAllText(Path.Combine("testdata/operations/createPassiveOffer", "createPassiveOffer.json"));
        var instance = JsonSingleton.GetInstance<CreatePassiveOfferOperationResponse>(json);
        var serialized = JsonConvert.SerializeObject(instance);
        var back = JsonConvert.DeserializeObject<CreatePassiveOfferOperationResponse>(serialized)!;

        AssertCreatePassiveOfferData(back);
    }

    private static void AssertCreatePassiveOfferData(CreatePassiveOfferOperationResponse operation)
    {
        Assert.AreEqual(operation.Amount, "11.27827");
        Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("USD", "GDS5JW5E6DRSSN5XK4LW7E6VUMFKKE2HU5WCOVFTO7P2RP7OXVCBLJ3Y"));
        Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());

        operation.Price
            .Should().Be("1.2");

        operation.PriceRatio.Numerator
            .Should().Be(11);

        operation.PriceRatio.Denominator
            .Should().Be(10);
    }
}