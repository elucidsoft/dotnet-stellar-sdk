using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class KeyPairTest
    {
        private const string Seed = "1123740522f11bfef6b3671f51e159ccf589ccf8965262dd5f97d1721d383dd4";

        [TestMethod]
        public void TestSign()
        {
            const string expectedSig = "587d4b472eeef7d07aafcd0b049640b0bb3f39784118c2e2b73a04fa2f64c9c538b4b2d0f5335e968a480021fdc23e98c0ddf424cb15d8131df8cb6c4bb58309";
            var keyPair = KeyPair.FromSecretSeed(Util.HexToBytes(Seed));
            const string data = "hello world";

            var bytes = Encoding.UTF8.GetBytes(data);
            var sig = keyPair.Sign(bytes);

            Assert.IsTrue(Util.HexToBytes(expectedSig).SequenceEqual(sig));
        }

        [TestMethod]
        public void TestVerifyTrue()
        {
            const string sig = "587d4b472eeef7d07aafcd0b049640b0bb3f39784118c2e2b73a04fa2f64c9c538b4b2d0f5335e968a480021fdc23e98c0ddf424cb15d8131df8cb6c4bb58309";
            const string data = "hello world";
            var keyPair = KeyPair.FromSecretSeed(Util.HexToBytes(Seed));

            var bytes = Encoding.UTF8.GetBytes(data);
            Assert.IsTrue(keyPair.Verify(bytes, Util.HexToBytes(sig)));
        }

        [TestMethod]
        public void TestVerifyFalse()
        {
            const string badSig = "687d4b472eeef7d07aafcd0b049640b0bb3f39784118c2e2b73a04fa2f64c9c538b4b2d0f5335e968a480021fdc23e98c0ddf424cb15d8131df8cb6c4bb58309";
            byte[] corrupt = {0x00};
            const string data = "hello world";
            var keyPair = KeyPair.FromSecretSeed(Util.HexToBytes(Seed));

            var bytes = Encoding.UTF8.GetBytes(data);
            Assert.IsFalse(keyPair.Verify(bytes, Util.HexToBytes(badSig)));
            Assert.IsFalse(keyPair.Verify(bytes, corrupt));
        }

        [TestMethod]
        public void TestFromSecretSeed()
        {
            var keypairs = new Dictionary<string, string>
            {
                {"SDJHRQF4GCMIIKAAAQ6IHY42X73FQFLHUULAPSKKD4DFDM7UXWWCRHBE", "GCZHXL5HXQX5ABDM26LHYRCQZ5OJFHLOPLZX47WEBP3V2PF5AVFK2A5D"},
                {"SDTQN6XUC3D2Z6TIG3XLUTJIMHOSON2FMSKCTM2OHKKH2UX56RQ7R5Y4", "GDEAOZWTVHQZGGJY6KG4NAGJQ6DXATXAJO3AMW7C4IXLKMPWWB4FDNFZ"},
                {"SDIREFASXYQVEI6RWCQW7F37E6YNXECQJ4SPIOFMMMJRU5CMDQVW32L5", "GD2EVR7DGDLNKWEG366FIKXO2KCUAIE3HBUQP4RNY7LEZR5LDKBYHMM6"},
                {"SDAPE6RHEJ7745VQEKCI2LMYKZB3H6H366I33A42DG7XKV57673XLCC2", "GDLXVH2BTLCLZM53GF7ELZFF4BW4MHH2WXEA4Z5Z3O6DPNZNR44A56UJ"},
                {"SDYZ5IYOML3LTWJ6WIAC2YWORKVO7GJRTPPGGNJQERH72I6ZCQHDAJZN", "GABXJTV7ELEB2TQZKJYEGXBUIG6QODJULKJDI65KZMIZZG2EACJU5EA7"}
            };

            foreach (var pair in keypairs)
            {
                var accountId = pair.Value;
                var keypair = KeyPair.FromSecretSeed(pair.Key);

                Assert.AreEqual(accountId, keypair.Address);
                Assert.AreEqual(pair.Key, keypair.SecretSeed);
            }
        }

        [TestMethod]
        public void TestCanSign()
        {
            var keyPair = KeyPair.FromSecretSeed("SDJHRQF4GCMIIKAAAQ6IHY42X73FQFLHUULAPSKKD4DFDM7UXWWCRHBE");
            Assert.IsTrue(keyPair.CanSign());

            keyPair = KeyPair.FromAccountId("GABXJTV7ELEB2TQZKJYEGXBUIG6QODJULKJDI65KZMIZZG2EACJU5EA7");
            Assert.IsFalse(keyPair.CanSign());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestSignWithoutSecret()
        {
            var keyPair = KeyPair.FromAccountId("GDEAOZWTVHQZGGJY6KG4NAGJQ6DXATXAJO3AMW7C4IXLKMPWWB4FDNFZ");
            const string data = "hello world";

            try
            {
                var unused = keyPair.Sign(Encoding.UTF8.GetBytes(data));
            }
            catch (Exception e)
            {
                Assert.AreEqual("KeyPair does not contain secret key. Use KeyPair.fromSecretSeed method to create a new KeyPair with a secret key.", e.Message);
                throw;
            }
        }
    }
}