using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.operations
{
    [TestClass]
    public class SetTrustlineFlagsOperationResponseTest
    {
        //Set Trustline Flags
        [TestMethod]
        public void TestSetTrustlineFlags()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/setTrustlineFlags", "setTrustlineFlags.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertTestSetTrustlineFlagsData(back);
        }

        private static void AssertTestSetTrustlineFlagsData(OperationResponse instance)
        {
            Assert.IsTrue(instance is SetTrustlineFlagsOperationResponse);
            var operation = (SetTrustlineFlagsOperationResponse)instance;
            var operation2 = new SetTrustlineFlagsOperationResponse("credit_alphanum4", "EUR", "GDIROJW2YHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM", "GTRUSTORYHMSFZJJ4R5XWWNUVND5I45YEWS5DSFKXCHMADZ5V374U2LM", new string[1] { "authorized" }, new string[1] { "authorized_to_maintain_liabilites" });

            Assert.AreEqual(operation.AssetType, operation2.AssetType);
            Assert.AreEqual(operation.AssetCode, operation2.AssetCode);
            Assert.AreEqual(operation.AssetIssuer, operation2.AssetIssuer);
            Assert.AreEqual(operation.Trustor, operation2.Trustor);
            Assert.AreEqual(operation.SetFlags[0], operation2.SetFlags[0]);
            Assert.AreEqual(operation.ClearFlags[0], operation2.ClearFlags[0]);
            Assert.AreEqual(operation.Asset, operation2.Asset);
        }
    }
}
