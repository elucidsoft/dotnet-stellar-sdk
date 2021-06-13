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
    public class ClawbackClaimableBalance
    {
        //Clawback Claimable Balance
        [TestMethod]
        public void TestClawbackClaimableBalance()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/clawbackClaimableBalance", "clawbackClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertClawbackClaimableBalanceData(back);
        }

        private static void AssertClawbackClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is ClawbackClaimableBalanceOperationResponse);
            var operation = (ClawbackClaimableBalanceOperationResponse)instance;

            Assert.AreEqual(214525026504705, operation.Id);
            Assert.AreEqual(new ClawbackClaimableBalanceOperationResponse("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7").BalanceID, operation.BalanceID);
        }
    }
}
