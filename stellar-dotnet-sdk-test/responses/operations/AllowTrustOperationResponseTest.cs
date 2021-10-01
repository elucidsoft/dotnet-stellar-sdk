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
    public class AllowTrustOperationResponseTest
    {
        //Allow Trust
        [TestMethod]
        public void TestDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/allowTrust", "allowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAllowTrustOperationData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAllowTrustOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/allowTrust", "allowTrust.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAllowTrustOperationData(back);
        }

        private static void AssertAllowTrustOperationData(OperationResponse instance)
        {
            Assert.IsTrue(instance is AllowTrustOperationResponse);
            var operation = (AllowTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustor, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Trustee, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.TrusteeMuxed);
            Assert.IsNull(operation.TrusteeMuxedID);
            Assert.AreEqual(operation.Authorize, true);
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }


        //Allow Trust (Muxed)
        [TestMethod]
        public void TestDeserializeAllowTrustOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/allowTrust", "allowTrustMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAllowTrustOperationMuxed(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAllowTrustOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/allowTrust", "allowTrustMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAllowTrustOperationMuxed(back);
        }

        private static void AssertAllowTrustOperationMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is AllowTrustOperationResponse);
            var operation = (AllowTrustOperationResponse)instance;

            Assert.AreEqual(operation.Trustor, "GDZ55LVXECRTW4G36EZPTHI4XIYS5JUC33TUS22UOETVFVOQ77JXWY4F");
            Assert.AreEqual(operation.Trustee, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.TrusteeMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.TrusteeMuxedID, 5123456789);
            Assert.AreEqual(operation.Authorize, true);
            Assert.AreEqual(operation.Asset, Asset.CreateNonNativeAsset("EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM"));
        }
    }
}
