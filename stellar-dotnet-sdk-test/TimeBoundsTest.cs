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
            var now = new DateTime(2018, 12, 01, 17, 30, 30);
            var timeBounds = new TimeBounds(now);

            Assert.AreEqual(1543685430, timeBounds.MinTime);
            Assert.AreEqual(0, timeBounds.MaxTime);
        }

        [TestMethod]
        public void TestTimeBoundsWithDateTimeWithoutMinTime()
        {
            var now = new DateTime(2018, 12, 01, 17, 30, 30);
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
    }
}