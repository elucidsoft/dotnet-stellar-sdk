using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class PaymentResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAABAAAAAAAAAAA=", typeof(PaymentSuccess), true);
        }

        [TestMethod]
        public void TestMalformed()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB/////wAAAAA=", typeof(PaymentMalformed), false);
        }

        [TestMethod]
        public void TestUnderfunded()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB/////gAAAAA=", typeof(PaymentUnderfunded), false);
        }

        [TestMethod]
        public void TestSrcNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB/////QAAAAA=", typeof(PaymentSrcNoTrust), false);
        }

        [TestMethod]
        public void TestSrcNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB/////AAAAAA=", typeof(PaymentSrcNotAuthorized), false);
        }

        [TestMethod]
        public void TestNoDestination()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB////+wAAAAA=", typeof(PaymentNoDestination), false);
        }

        [TestMethod]
        public void TestNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB////+gAAAAA=", typeof(PaymentNoTrust), false);
        }

        [TestMethod]
        public void TestNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB////+QAAAAA=", typeof(PaymentNotAuthorized), false);
        }

        [TestMethod]
        public void TestLineFull()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB////+AAAAAA=", typeof(PaymentLineFull), false);
        }

        [TestMethod]
        public void TestNoIssuer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAB////9wAAAAA=", typeof(PaymentNoIssuer), false);
        }
    }
}