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
    public class ChangeTrustOperationResponseTest
    {
        //Change Trust
        [TestMethod]
        public void TestDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/changeTrust", "changeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertChangeTrustData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeChangeTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/changeTrust", "changeTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertChangeTrustData(back);
        }

        private static void AssertChangeTrustData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ChangeTrustOperationResponse);
            var operation = (ChangeTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustee, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.TrustorMuxed);
            Assert.IsNull(operation.TrustorMuxedID);
            Assert.AreEqual(operation.Limit, "922337203685.4775807");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }

        //Change Trust (Muxed)
        [TestMethod]
        public void TestDeserializeChangeTrustOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/changeTrust", "changeTrustMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertChangeTrustDataMuxed(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeChangeTrustOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/changeTrust", "changeTrustMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertChangeTrustDataMuxed(back);
        }

        private static void AssertChangeTrustDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is ChangeTrustOperationResponse);
            var operation = (ChangeTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustee, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.Trustor, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.TrustorMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.TrustorMuxedID, 5123456789UL);
            Assert.AreEqual(operation.Limit, "922337203685.4775807");
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }
    }
}
