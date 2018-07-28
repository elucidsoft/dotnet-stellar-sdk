using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Memo = stellar_dotnet_sdk.Memo;
using TimeBounds = stellar_dotnet_sdk.TimeBounds;
using Transaction = stellar_dotnet_sdk.Transaction;
using XdrTransaction = stellar_dotnet_sdk.xdr.Transaction;

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