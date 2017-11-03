using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class AccountFlagTest
    {
        [TestMethod]
        public void TestValues()
        {
            Assert.AreEqual(1, (int)AccountFlag.AUTH_REQUIRED_FLAG);
            Assert.AreEqual(2, (int)AccountFlag.AUTH_REVOCABLE_FLAG);
            Assert.AreEqual(4, (int)AccountFlag.AUTH_IMMUTABLE_FLAG);
        }
    }
}
