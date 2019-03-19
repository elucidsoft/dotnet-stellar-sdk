using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class AllowTrustResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAHAAAAAAAAAAA=", typeof(AllowTrustSuccess), true);
        }

        [TestMethod]
        public void TestMalformed()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAH/////wAAAAA=", typeof(AllowTrustMalformed), false);
        }

        [TestMethod]
        public void TestNoTrustLine()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAH/////gAAAAA=", typeof(AllowTrustNoTrustline),
                false);
        }

        [TestMethod]
        public void TestTrustNotRequired()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAH/////QAAAAA=", typeof(AllowTrustNotRequired),
                false);
        }

        [TestMethod]
        public void TestCantRevoke()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAH/////AAAAAA=", typeof(AllowTrustCantRevoke),
                false);
        }

        [TestMethod]
        public void TestSelfNotAllowed()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAH////+wAAAAA=", typeof(AllowTrustSelfNotAllowed),
                false);
        }
    }
}