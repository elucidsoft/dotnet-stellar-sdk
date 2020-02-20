using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class TimeBoundsTest
    {
        [TestMethod]
        public void TestTimeBoundsHashEquality()
        {
            var timeBounds = new TimeBounds(56, 65);
            var timeBounds2 = new TimeBounds(56, 65);

            Assert.AreEqual(timeBounds.GetHashCode(), timeBounds.GetHashCode());
            Assert.AreEqual(timeBounds2, timeBounds2);
        }

        [TestMethod]
        public void TestTimeBoundsWithoutBounds()
        {
            var timeBounds = new TimeBounds(0, 0);
            Assert.AreEqual(0, timeBounds.MinTime);
            Assert.AreEqual(0, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsWithoutMinTime()
        {
            var timeBounds = new TimeBounds(0, 100);
            Assert.AreEqual(0, timeBounds.MinTime);
            Assert.AreEqual(100, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsWithoutMaxTime()
        {
            var timeBounds = new TimeBounds(100, 0);
            Assert.AreEqual(100, timeBounds.MinTime);
            Assert.AreEqual(0, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsThrowsIfMaxTimeGreaterThanMinTime()
        {
            Assert.ThrowsException<ArgumentException>(() => new TimeBounds(20, 10));
        }

        [TestMethod]
        public void TestTimeBoundsWithDateTime()
        {
            var now = new DateTime(2018, 12, 01, 17, 30, 30, DateTimeKind.Utc);
            var timeBounds = new TimeBounds(now);

            Assert.AreEqual(1543685430, timeBounds.MinTime);
            Assert.AreEqual(0, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsWithDateTimeWithoutMinTime()
        {
            var now = new DateTime(2018, 12, 01, 17, 30, 30, DateTimeKind.Utc);
            var timeBounds = new TimeBounds(maxTime: now);

            Assert.AreEqual(0, timeBounds.MinTime);
            Assert.AreEqual(1543685430, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsWithDateTimeThrowsIfMaxTimeGreaterThanMinTime()
        {
            var now = new DateTime(2018, 12, 01, 17, 30, 30);
            var yesterday = now.Subtract(TimeSpan.FromDays(1));

            Assert.ThrowsException<ArgumentException>(() => new TimeBounds(now, yesterday));
        }

        [TestMethod]
        public void TestTimeBoundsWithDuration()
        {
            var now = new DateTime(2018, 12, 01, 17, 30, 30);
            var duration = TimeSpan.FromDays(2.0);
            var timeBounds = new TimeBounds(now, duration);

            Assert.AreEqual(1543685430, timeBounds.MinTime);
            Assert.AreEqual(1543858230, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsWithDurationFromUtcNow()
        {
            var now = DateTimeOffset.UtcNow;
            var duration = TimeSpan.FromDays(2.0);
            var timeBounds = new TimeBounds(duration);
            var maxDateTime = DateTimeOffset.FromUnixTimeSeconds(timeBounds.MaxTime);

            Assert.AreNotEqual(0, timeBounds.MinTime);
            Assert.IsTrue(maxDateTime > now);
        }
    }
}