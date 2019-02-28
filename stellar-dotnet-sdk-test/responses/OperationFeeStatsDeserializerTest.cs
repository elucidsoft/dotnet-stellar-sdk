using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class OperationFeeStatsDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationFeeStats.json"));
            var stats = JsonSingleton.GetInstance<OperationFeeStatsResponse>(json);
            AssertTestData(stats);
        }

        [TestMethod]
        public void TestSerialize()
        {
            var stats = new OperationFeeStatsResponse(101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 0.97f, 100, 20882791);
            var json = JsonConvert.SerializeObject(stats);
            var back = JsonConvert.DeserializeObject<OperationFeeStatsResponse>(json);
            Assert.AreEqual(stats.Min, back.Min);
            Assert.AreEqual(stats.Mode, back.Mode);
            Assert.AreEqual(stats.LastLedger, back.LastLedger);
            Assert.AreEqual(stats.LastLedgerBaseFee, back.LastLedgerBaseFee);
        }

        public static void AssertTestData(OperationFeeStatsResponse stats)
        {
            Assert.AreEqual(20882791L, stats.LastLedger);
            Assert.AreEqual(100L, stats.LastLedgerBaseFee);
            Assert.AreEqual(0.97f, stats.LedgerCapacityUsage);
            Assert.AreEqual(101L, stats.Min);
            Assert.AreEqual(102L, stats.Mode);
            Assert.AreEqual(103L, stats.P10);
            Assert.AreEqual(104L, stats.P20);
            Assert.AreEqual(105L, stats.P30);
            Assert.AreEqual(106L, stats.P40);
            Assert.AreEqual(107L, stats.P50);
            Assert.AreEqual(108L, stats.P60);
            Assert.AreEqual(109L, stats.P70);
            Assert.AreEqual(110L, stats.P80);
            Assert.AreEqual(111, stats.P90);
            Assert.AreEqual(112, stats.P95);
            Assert.AreEqual(113L, stats.P99);
        }
    }
}
