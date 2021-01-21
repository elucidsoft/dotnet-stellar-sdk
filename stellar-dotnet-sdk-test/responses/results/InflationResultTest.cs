using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class InflationResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAAJAAAAAAAAAAIAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAJiWgAAAAAADLNchwR3S8r1eVV+aPJAT1CkmM4vNhQ3mitHQ34PP5AAAAAABMS0AAAAAAA==",
                typeof(InflationSuccess), true);
            var failed = (TransactionResultFailed)tx;
            var op = (InflationSuccess)failed.Results[0];
            Assert.AreEqual(2, op.Payouts.Length);
            var payout = op.Payouts[0];
            Assert.AreEqual("GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC", payout.Destination.AccountId);
            Assert.AreEqual("1", payout.Amount);
        }

        [TestMethod]
        public void TestNotTime()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAJ/////wAAAAA=", typeof(InflationNotTime), false);
        }
    }
}