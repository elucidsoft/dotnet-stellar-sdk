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
    }
}