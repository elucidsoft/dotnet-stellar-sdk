using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System.IO;
using FluentAssertions;

namespace stellar_dotnet_sdk_test.responses.operations;

[TestClass]
public class ManageBuyOfferOperationResponseTest
{
    [TestMethod]
    public void TestDeserializeManageBuyOfferOperation()
    {
        var json = File.ReadAllText(Path.Combine("testdata/operations/manageBuyOffer", "manageBuyOffer.json"));
        var instance = JsonSingleton.GetInstance<OperationResponse>(json);

        AssertManageBuyOfferData(instance);
    }

    [TestMethod]
    public void TestSerializeDeserializeManageBuyOfferOperation()
    {
        var json = File.ReadAllText(Path.Combine("testdata/operations/manageBuyOffer", "manageBuyOffer.json"));
        var instance = JsonSingleton.GetInstance<OperationResponse>(json);
        var serialized = JsonConvert.SerializeObject(instance);
        var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

        AssertManageBuyOfferData(back);
    }

    //Manage Buy Offer (Before Horizon 1.0.0)
    [TestMethod]
    public void TestDeserializeManageBuyOfferOperationPre100()
    {
        var json = File.ReadAllText(Path.Combine("testdata/operations/manageBuyOffer", "manageBuyOfferPre100.json"));
        var instance = JsonSingleton.GetInstance<OperationResponse>(json);

        AssertManageBuyOfferData(instance);
    }

    [TestMethod]
    public void TestSerializeDeserializeManageBuyOfferOperationPre100()
    {
        var json = File.ReadAllText(Path.Combine("testdata/operations/manageBuyOffer", "manageBuyOfferPre100.json"));
        var instance = JsonSingleton.GetInstance<OperationResponse>(json);
        var serialized = JsonConvert.SerializeObject(instance);
        var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

        AssertManageBuyOfferData(back);
    }

    private static void AssertManageBuyOfferData(OperationResponse instance)
    {
        Assert.IsTrue(instance is ManageBuyOfferOperationResponse);
        var operation = (ManageBuyOfferOperationResponse)instance;

        Assert.AreEqual(operation.OfferId, "1");
        Assert.AreEqual(operation.Amount, "50000.0000000");

        operation.Price
            .Should().Be("0.0463000");

        operation.PriceRatio.Numerator
            .Should().Be(463);

        operation.PriceRatio.Denominator
            .Should().Be(10000);

        Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("RMT", "GDEGOXPCHXWFYY234D2YZSPEJ24BX42ESJNVHY5H7TWWQSYRN5ZKZE3N"));
        Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
    }
}