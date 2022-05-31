using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class MuxedAccountTest
    {
        [TestMethod]
        public void TestFromAccountId()
        {
            var muxed = MuxedAccountMed25519.FromMuxedAccountId("MAQAA5L65LSYH7CQ3VTJ7F3HHLGCL3DSLAR2Y47263D56MNNGHSQSAAAAAAAAAAE2LP26");
            Assert.AreEqual(1234UL, muxed.Id);
            Assert.AreEqual("GAQAA5L65LSYH7CQ3VTJ7F3HHLGCL3DSLAR2Y47263D56MNNGHSQSTVY", muxed.Key.Address);
            Assert.AreEqual("MAQAA5L65LSYH7CQ3VTJ7F3HHLGCL3DSLAR2Y47263D56MNNGHSQSAAAAAAAAAAE2LP26", muxed.Address);
        }
    }
}