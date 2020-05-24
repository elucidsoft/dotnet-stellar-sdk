using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
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
        [DataRow("SAA6NXOBOXP3RXGAXBW6PGFI5BPK4ODVAWITS4VDOMN5C2M4B66ZML", DisplayName = "Secret Key")]
        [DataRow("GAAAAAAAACGC6", DisplayName = "Invalid length (Ed25519 should be 32 bytes, not 5)")]
        //[DataRow("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZA", DisplayName = "Invalid length (congruent to 1 mod 8)")]
        [DataRow("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJUACUSI", DisplayName = "Invalid length (base-32 decoding should yield 35 bytes, not 36)")]
        [DataRow("G47QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVP2I", DisplayName = "Invalid algorithm (low 3 bits of version byte are 7)")]
        [DataRow("")]
        public void IsNotValidEd25519PublicKey(string address)
        {
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