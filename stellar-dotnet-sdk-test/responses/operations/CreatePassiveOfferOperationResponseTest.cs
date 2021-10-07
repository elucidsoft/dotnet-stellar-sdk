using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.operations
{
    [TestClass]
    public class CreatePassiveOfferOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createPassiveOffer", "createPassiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertCreatePassiveOfferData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeCreatePassiveOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/createPassiveOffer", "createPassiveOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertCreatePassiveOfferData(back);
        }

        private static void AssertCreatePassiveOfferData(OperationResponse instance)
        {
            Assert.IsTrue(instance is CreatePassiveOfferOperationResponse);
            var operation = (CreatePassiveOfferOperationResponse)instance;

            Assert.AreEqual(operation.Amount, "11.27827");
            Assert.AreEqual(operation.BuyingAsset, Asset.CreateNonNativeAsset("USD", "GDS5JW5E6DRSSN5XK4LW7E6VUMFKKE2HU5WCOVFTO7P2RP7OXVCBLJ3Y"));
            Assert.AreEqual(operation.SellingAsset, new AssetTypeNative());
        }
    }
}
