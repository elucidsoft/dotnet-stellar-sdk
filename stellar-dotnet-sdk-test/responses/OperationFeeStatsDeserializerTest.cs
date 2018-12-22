using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    class OperationFeeStatsDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationPage.json"));
            var stats = JsonSingleton.GetInstance<OperationFeeStatsResponse>(json);
            AssertTestData(stats);
        }

        public static void AssertTestData(OperationFeeStatsResponse stats)
        {
            Assert.AreEqual(20882791L, stats.LastLedger);
            Assert.AreEqual(100L, stats.LastLedgerBaseFee);
            Assert.AreEqual(101L, stats.Min);
            Assert.AreEqual(102L, stats.Mode);
         }
    }
}
