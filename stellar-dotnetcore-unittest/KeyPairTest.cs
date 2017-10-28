using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class KeyPairTest
    {
        private const string SEED = "1123740522f11bfef6b3671f51e159ccf589ccf8965262dd5f97d1721d383dd4";

        [TestMethod]
        public void TestSign()
        {
            string expectedSig = "587d4b472eeef7d07aafcd0b049640b0bb3f39784118c2e2b73a04fa2f64c9c538b4b2d0f5335e968a480021fdc23e98c0ddf424cb15d8131df8cb6c4bb58309";
            KeyPair keyPair = KeyPair.FromSecretSeed(Util.HexToBytes(SEED));
            string data = "hello world";

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] sig = keyPair.Sign(bytes);

            Assert.IsTrue(Util.HexToBytes(expectedSig).SequenceEqual(sig));
        }

        [TestMethod]
        public void TestVerifyTrue()
        {
            string sig = "587d4b472eeef7d07aafcd0b049640b0bb3f39784118c2e2b73a04fa2f64c9c538b4b2d0f5335e968a480021fdc23e98c0ddf424cb15d8131df8cb6c4bb58309";
            string data = "hello world";
            KeyPair keyPair = KeyPair.FromSecretSeed(Util.HexToBytes(SEED));

            var bytes = Encoding.UTF8.GetBytes(data);
            Assert.IsTrue(keyPair.Verify(bytes, Util.HexToBytes(sig)));
        }

        [TestMethod]
        public void TestVerifyFalse()
        {
            string badSig = "687d4b472eeef7d07aafcd0b049640b0bb3f39784118c2e2b73a04fa2f64c9c538b4b2d0f5335e968a480021fdc23e98c0ddf424cb15d8131df8cb6c4bb58309";
            byte[] corrupt = { 0x00 };
            string data = "hello world";
            KeyPair keyPair = KeyPair.FromSecretSeed(Util.HexToBytes(SEED));

            var bytes = Encoding.UTF8.GetBytes(data);
            Assert.IsFalse(keyPair.Verify(bytes, Util.HexToBytes(badSig)));
            Assert.IsFalse(keyPair.Verify(bytes, corrupt));
        }
    }
}

