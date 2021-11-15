using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class AccountFlagTest
    {
        [TestMethod]
        public void TestValues()
        {
            Assert.AreEqual(1, (int)AccountFlag.AuthRequiredFlag);
            Assert.AreEqual(2, (int)AccountFlag.AuthRevocableFlag);
            Assert.AreEqual(4, (int)AccountFlag.AuthImmutableFlag);
            Assert.AreEqual(8, (int)AccountFlag.AuthClawbackFlag);
        }
    }
}