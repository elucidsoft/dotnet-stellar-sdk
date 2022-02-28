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
    public class ClawbackOperationResponseTest
    {
        //Clawback
        [TestMethod]
        public void TestSerializeClawback()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/clawback", "clawback.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClawbackData(back);
        }

        private static void AssertClawbackData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClawbackOperationResponse);
            var operation = (ClawbackOperationResponse)instance;

            Assert.AreEqual(3602979345141761, operation.Id);
            Assert.AreEqual(operation.Amount, "1000");
            Assert.AreEqual(operation.AssetCode, "EUR");
            Assert.AreEqual(operation.AssetIssuer, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.AssetType, "credit_alphanum4");
            Assert.AreEqual(operation.From, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.FromMuxed);
            Assert.IsNull(operation.FromMuxedID);
            Assert.AreEqual(operation.Asset.ToQueryParameterEncodedString(), "EUR:GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
        }

        //Clawback (Muxed)
        [TestMethod]
        public void TestSerializeClawbackMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/clawback", "clawbackMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClawbackDataMuxed(back);
        }

        private static void AssertClawbackDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClawbackOperationResponse);
            var operation = (ClawbackOperationResponse)instance;

            Assert.AreEqual(3602979345141761, operation.Id);
            Assert.AreEqual(operation.Amount, "1000");
            Assert.AreEqual(operation.AssetCode, "EUR");
            Assert.AreEqual(operation.AssetIssuer, "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
            Assert.AreEqual(operation.AssetType, "credit_alphanum4");
            Assert.AreEqual(operation.From, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.FromMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.FromMuxedID, 5123456789UL);
            Assert.AreEqual(operation.Asset.ToQueryParameterEncodedString(), "EUR:GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM");
        }
    }
}
