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
            var muxed = MuxedAccountMed25519.FromMuxedAccountId(
                "MAAAAAAAAAAAJURAAB2X52XFQP6FBXLGT6LWOOWMEXWHEWBDVRZ7V5WH34Y22MPFBHUHY");
            Assert.AreEqual(1234UL, muxed.Id);
            Assert.AreEqual("GAQAA5L65LSYH7CQ3VTJ7F3HHLGCL3DSLAR2Y47263D56MNNGHSQSTVY", muxed.Key.Address);
            Assert.AreEqual("MAAAAAAAAAAAJURAAB2X52XFQP6FBXLGT6LWOOWMEXWHEWBDVRZ7V5WH34Y22MPFBHUHY", muxed.Address);
        }
    }
}