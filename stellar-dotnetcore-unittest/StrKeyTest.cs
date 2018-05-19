using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class StrKeyTest
    {
        [TestMethod]
        public void TestDecodeEncode()
        {
            var seed = "SDJHRQF4GCMIIKAAAQ6IHY42X73FQFLHUULAPSKKD4DFDM7UXWWCRHBE";
            var secret = StrKey.DecodeCheck(StrKey.VersionByte.SEED, seed);
            var encoded = StrKey.EncodeCheck(StrKey.VersionByte.SEED, secret);

            Assert.AreEqual(seed, encoded);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDecodeInvalidVersionByte()
        {
            var address = "GCZHXL5HXQX5ABDM26LHYRCQZ5OJFHLOPLZX47WEBP3V2PF5AVFK2A5D";
            StrKey.DecodeCheck(StrKey.VersionByte.SEED, address);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDecodeInvalidSeed()
        {
            var seed = "SAA6NXOBOXP3RXGAXBW6PGFI5BPK4ODVAWITS4VDOMN5C2M4B66ZML";
            StrKey.DecodeCheck(StrKey.VersionByte.SEED, seed);
        }

        [TestMethod]
        public void IsValidEd25519PublicKey()
        {
            var address = "GCZHXL5HXQX5ABDM26LHYRCQZ5OJFHLOPLZX47WEBP3V2PF5AVFK2A5D";
            var result = StrKey.IsValidEd25519PublicKey(address);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotValidEd25519PublicKey()
        {
            var address = "SAA6NXOBOXP3RXGAXBW6PGFI5BPK4ODVAWITS4VDOMN5C2M4B66ZML";
            var result = StrKey.IsValidEd25519PublicKey(address);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidEd25519SecretSeed()
        {
            var seed = "SDJHRQF4GCMIIKAAAQ6IHY42X73FQFLHUULAPSKKD4DFDM7UXWWCRHBE";
            var result = StrKey.IsValidEd25519SecretSeed(seed);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNotValidEd25519SecretSeed()
        {
            var seed = "GCZHXL5HXQX5ABDM26LHYRCQZ5OJFHLOPLZX47WEBP3V2PF5AVFK2A5D";
            var result = StrKey.IsValidEd25519SecretSeed(seed);

            Assert.IsFalse(result);
        }
    }
}