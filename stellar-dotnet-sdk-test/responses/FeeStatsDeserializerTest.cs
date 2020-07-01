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
    public class FeeStatsDeserializerTest
    {
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "feeStats.json"));
            var stats = JsonSingleton.GetInstance<FeeStatsResponse>(json);
            AssertTestData(stats);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "feeStats.json"));
            var stats = JsonConvert.DeserializeObject<FeeStatsResponse>(json);

            var serialized = JsonConvert.SerializeObject(stats);
            var back = JsonConvert.DeserializeObject<FeeStatsResponse>(serialized);

            Assert.AreEqual(stats.LastLedger, back.LastLedger);
            Assert.AreEqual(stats.LastLedgerBaseFee, back.LastLedgerBaseFee);
            Assert.AreEqual(stats.LedgerCapacityUsage, back.LedgerCapacityUsage);
        }

        public static void AssertTestData(FeeStatsResponse stats)
        {
            Assert.AreEqual(0.97m, stats.LedgerCapacityUsage);

            //Assert Fee Charged Data
            Assert.AreEqual(1L, stats.FeeCharged.Min);
            Assert.AreEqual(100L, stats.FeeCharged.Mode);
            Assert.AreEqual(10L, stats.FeeCharged.P10);
            Assert.AreEqual(20L, stats.FeeCharged.P20);
            Assert.AreEqual(30L, stats.FeeCharged.P30);
            Assert.AreEqual(40L, stats.FeeCharged.P40);
            Assert.AreEqual(50L, stats.FeeCharged.P50);
            Assert.AreEqual(60L, stats.FeeCharged.P60);
            Assert.AreEqual(70L, stats.FeeCharged.P70);
            Assert.AreEqual(80L, stats.FeeCharged.P80);
            Assert.AreEqual(90L, stats.FeeCharged.P90);
            Assert.AreEqual(95L, stats.FeeCharged.P95);
            Assert.AreEqual(99L, stats.FeeCharged.P99);

            //Assert Max Fee Data
            Assert.AreEqual(1L, stats.MaxFee.Min);
            Assert.AreEqual(100L, stats.MaxFee.Mode);
            Assert.AreEqual(10L, stats.MaxFee.P10);
            Assert.AreEqual(20L, stats.MaxFee.P20);
            Assert.AreEqual(30L, stats.MaxFee.P30);
            Assert.AreEqual(40L, stats.MaxFee.P40);
            Assert.AreEqual(50L, stats.MaxFee.P50);
            Assert.AreEqual(60L, stats.MaxFee.P60);
            Assert.AreEqual(70L, stats.MaxFee.P70);
            Assert.AreEqual(80L, stats.MaxFee.P80);
            Assert.AreEqual(90L, stats.MaxFee.P90);
            Assert.AreEqual(95L, stats.MaxFee.P95);
            Assert.AreEqual(99L, stats.MaxFee.P99);
        }
    }
}
