using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class SetOptionsResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAFAAAAAAAAAAA=", typeof(SetOptionsSuccess), true);
        }

        [TestMethod]
        public void TestLowReserve()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF/////wAAAAA=", typeof(SetOptionsLowReserve),
                false);
        }

        [TestMethod]
        public void TestTooManySigner()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF/////gAAAAA=", typeof(SetOptionsTooManySigners),
                false);
        }

        [TestMethod]
        public void TestBadFlag()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF/////QAAAAA=", typeof(SetOptionsBadFlags), false);
        }

        [TestMethod]
        public void TestInvalidInflation()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF/////AAAAAA=", typeof(SetOptionsInvalidInflation),
                false);
        }

        [TestMethod]
        public void TestCantChange()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF////+wAAAAA=", typeof(SetOptionsCantChange),
                false);
        }

        [TestMethod]
        public void TestUnknownFlag()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF////+gAAAAA=", typeof(SetOptionsUnknownFlag),
                false);
        }

        [TestMethod]
        public void TestThresholdOutOfRange()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF////+QAAAAA=",
                typeof(SetOptionsThresholdOutOfRange), false);
        }

        [TestMethod]
        public void TestBadSigner()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF////+AAAAAA=", typeof(SetOptionsBadSigner), false);
        }

        [TestMethod]
        public void TestInvalidHomeDomain()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAF////9wAAAAA=", typeof(SetOptionsInvalidHomeDomain),
                false);
        }
    }
}