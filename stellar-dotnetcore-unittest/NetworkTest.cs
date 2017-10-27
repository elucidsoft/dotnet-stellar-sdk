using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class NetworkTest
    {
        [TestCleanup]
        public void ResetNetwork()
        {
            Network.Use(null);
        }

        [TestMethod]
        public void TestNoDefaultNetwork()
        {
            Assert.IsNull(Network.Current);
        }

        [TestMethod]
        public void TestSwitchToTestNetwork()
        {
            Network.UseTestNetwork();
            Assert.AreEqual("Test SDF Network ; September 2015", Network.Current.NetworkPassphrase);
        }

        [TestMethod]
        public void TestSwitchToPublicNework()
        {
            Network.UsePublicNetwork();
            Assert.AreEqual("Public Global Stellar Network ; September 2015", Network.Current.NetworkPassphrase);
        }
    }
}
