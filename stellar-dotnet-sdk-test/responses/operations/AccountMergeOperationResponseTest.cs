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
    public class AccountMergeOperationResponseTest
    {
        [TestMethod]
        public void TestDeserializeAccountMergeOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/accountMerge", "accountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAccountMergeData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountMergeOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/accountMerge", "accountMerge.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAccountMergeData(back);
        }

        private static void AssertAccountMergeData(OperationResponse instance)
        {
            Assert.IsTrue(instance is AccountMergeOperationResponse);
            var operation = (AccountMergeOperationResponse)instance;

            Assert.AreEqual(operation.Account, "GD6GKRABNDVYDETEZJQEPS7IBQMERCN44R5RCI4LJNX6BMYQM2KPGGZ2");
            Assert.AreEqual(operation.Into, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.IsNull(operation.IntoMuxed);
            Assert.IsNull(operation.IntoMuxedID);
        }

        //Account Merge (Muxed)
        [TestMethod]
        public void TestDeserializeAccountMergeOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/accountMerge", "accountMergeMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);

            AssertAccountMergeDataMuxed(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountMergeOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/accountMerge", "accountMergeMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertAccountMergeDataMuxed(back);
        }

        private static void AssertAccountMergeDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is AccountMergeOperationResponse);
            var operation = (AccountMergeOperationResponse)instance;

            Assert.AreEqual(operation.Account, "GD6GKRABNDVYDETEZJQEPS7IBQMERCN44R5RCI4LJNX6BMYQM2KPGGZ2");
            Assert.AreEqual(operation.Into, "GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2");
            Assert.AreEqual(operation.IntoMuxed, "MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24");
            Assert.AreEqual(operation.IntoMuxedID, "5123456789");
        }
    }
}
