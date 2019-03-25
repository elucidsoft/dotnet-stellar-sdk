using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class ManageDataResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAKAAAAAAAAAAA=", typeof(ManageDataSuccess), true);
        }

        [TestMethod]
        public void TestNotSupportedYet()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAK/////wAAAAA=", typeof(ManageDataNotSupportedYet),
                false);
        }

        [TestMethod]
        public void TestNameNotFound()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAK/////gAAAAA=", typeof(ManageDataNameNotFound),
                false);
        }

        [TestMethod]
        public void TestLowReserve()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAK/////QAAAAA=", typeof(ManageDataLowReserve),
                false);
        }

        [TestMethod]
        public void TestInvalidName()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAK/////AAAAAA=", typeof(ManageDataInvalidName),
                false);
        }
    }
}