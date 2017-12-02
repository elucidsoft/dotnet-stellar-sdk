using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class AccountFlagTest
    {
        [TestMethod]
        public void TestValues()
        {
            Assert.AreEqual(1, (int) AccountFlag.AuthRequiredFlag);
            Assert.AreEqual(2, (int) AccountFlag.AuthRevocableFlag);
            Assert.AreEqual(4, (int) AccountFlag.AuthImmutableFlag);
        }
    }
}