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
    public class ClaimClaimableBalanceOperationResponseTest
    {
        //Claim Claimable Balance
        [TestMethod]
        public void TestSerializationClaimClaimableBalanceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/claimClaimableBalance", "claimClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClaimClaimableBalanceData(back);
        }

        private static void AssertClaimClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClaimClaimableBalanceOperationResponse);
            var operation = (ClaimClaimableBalanceOperationResponse)instance;

            Assert.AreEqual(214525026504705, operation.Id);
            Assert.AreEqual("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7", operation.BalanceID);
            Assert.AreEqual("GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2", operation.Claimant);
            Assert.IsNull(operation.ClaimantMuxed);
            Assert.IsNull(operation.ClaimantMuxedID);

            var back = new ClaimClaimableBalanceOperationResponse(operation.BalanceID, operation.Claimant);
            Assert.IsNotNull(back);
        }

        //Claim Claimable Balance (Muxed)
        [TestMethod]
        public void TestSerializationClaimClaimableBalanceOperationMuxed()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/claimClaimableBalance", "claimClaimableBalanceMuxed.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClaimClaimableBalanceDataMuxed(back);
        }

        private static void AssertClaimClaimableBalanceDataMuxed(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClaimClaimableBalanceOperationResponse);
            var operation = (ClaimClaimableBalanceOperationResponse)instance;

            Assert.AreEqual(214525026504705, operation.Id);
            Assert.AreEqual("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7", operation.BalanceID);
            Assert.AreEqual("GCKICEQ2SA3KWH3UMQFJE4BFXCBFHW46BCVJBRCLK76ZY5RO6TY5D7Q2", operation.Claimant);
            Assert.AreEqual("MAAAAAABGFQ36FMUQEJBVEBWVMPXIZAKSJYCLOECKPNZ4CFKSDCEWV75TR3C55HR2FJ24", operation.ClaimantMuxed);
            Assert.AreEqual(5123456789, operation.ClaimantMuxedID);

            var back = new ClaimClaimableBalanceOperationResponse(operation.BalanceID, operation.Claimant);
            Assert.IsNotNull(back);
        }
    }
}
