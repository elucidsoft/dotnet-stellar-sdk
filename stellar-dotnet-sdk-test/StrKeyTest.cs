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
        public void TestDecodeEncodeMuxedAccount()
        {
            var address = "MAAAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITLVL6";
            var (id, key) = StrKey.DecodeStellarMuxedAccount(address);
            Assert.IsTrue(StrKey.IsValidMuxedAccount(address));
            Assert.AreEqual(0UL, id);
            var encodedKey = StrKey.EncodeStellarAccountId(key);
            Assert.AreEqual("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ", encodedKey);
            Assert.AreEqual(address, StrKey.EncodeStellarMuxedAccount(key, id));
        }

        [TestMethod]
        public void TestDecodeEncodeMuxedAccountWithLargeId()
        {
            var address = "MCAAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITKNOG";
            var (id, key) = StrKey.DecodeStellarMuxedAccount(address);
            Assert.IsTrue(StrKey.IsValidMuxedAccount(address));
            Assert.AreEqual(9223372036854775808UL, id);
            var encodedKey = StrKey.EncodeStellarAccountId(key);
            Assert.AreEqual("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ", encodedKey);
            Assert.AreEqual(address, StrKey.EncodeStellarMuxedAccount(key, id));
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
        //[DataRow("MCAAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITKNOGA", DisplayName = "Invalid length (congruent to 6 mod 8)")]
        [DataRow("MAAAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITIADJPA", DisplayName = "Invalid length (base-32 decoding should yield 43 bytes, not 44)")]
        [DataRow("M4AAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITIU2K", DisplayName = "Invalid algorithm (low 3 bits of version byte are 7)")]
        //[DataRow("MAAAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITLVL6", DisplayName = "Padding bytes are not allowed")]
        [DataRow("MAAAAAAAAAAAAAB7BQ2L7E5NBWMXDUCMZSIPOBKRDSBYVLMXGSSKF6YNPIB7Y77ITLVL4", DisplayName = "Invalid checksum")]
        public void IsNotValidMuxedAccount(string address)
        {
            var result = StrKey.IsValidMuxedAccount(address);
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