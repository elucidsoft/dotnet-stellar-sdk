using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System.Collections.Generic;
using System.Linq;
using xdrSDK = stellar_dotnet_sdk.xdr;

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
            var address = "MA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJUAAAAAAAAAAAACJUQ";
            var muxed = StrKey.DecodeStellarMuxedAccount(address);
            Assert.IsTrue(StrKey.IsValidMuxedAccount(address));
            Assert.AreEqual(0UL, muxed.Med25519.Id.InnerValue);

            var encodedKey = StrKey.EncodeStellarAccountId(muxed.Med25519.Ed25519.InnerValue);
            Assert.AreEqual("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ", encodedKey);
            Assert.AreEqual(address, StrKey.EncodeStellarMuxedAccount(new MuxedAccountMed25519(KeyPair.FromPublicKey(muxed.Med25519.Ed25519.InnerValue), muxed.Med25519.Id.InnerValue).MuxedAccount));
        }

        [TestMethod]
        public void TestDecodeEncodeMuxedAccountWithLargeId()
        {
            var address = "MA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVAAAAAAAAAAAAAJLK";
            var muxed = StrKey.DecodeStellarMuxedAccount(address);
            Assert.IsTrue(StrKey.IsValidMuxedAccount(address));
            Assert.AreEqual(9223372036854775808UL, muxed.Med25519.Id.InnerValue);
            var encodedKey = StrKey.EncodeStellarAccountId(muxed.Med25519.Ed25519.InnerValue);
            Assert.AreEqual("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ", encodedKey);
            Assert.AreEqual(address, StrKey.EncodeStellarMuxedAccount(new MuxedAccountMed25519(KeyPair.FromPublicKey(muxed.Med25519.Ed25519.InnerValue), muxed.Med25519.Id.InnerValue).MuxedAccount));
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

        [TestMethod]
        public void TestValidSignedPayloadEncode()
        {
            var payload = Util.HexToBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f20");
            var signedPayloadSigner = new SignedPayloadSigner(StrKey.DecodeStellarAccountId("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ"), payload);
            var encoded = StrKey.EncodeSignedPayload(signedPayloadSigner);
            Assert.AreEqual(encoded, "PA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJUAAAAAQACAQDAQCQMBYIBEFAWDANBYHRAEISCMKBKFQXDAMRUGY4DUPB6IBZGM");

            // Valid signed payload with an ed25519 public key and a 29-byte payload.
            payload = Util.HexToBytes("0102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d");
            signedPayloadSigner = new SignedPayloadSigner(StrKey.DecodeStellarAccountId("GA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJVSGZ"), payload);
            encoded = StrKey.EncodeSignedPayload(signedPayloadSigner);
            Assert.AreEqual(encoded, "PA7QYNF7SOWQ3GLR2BGMZEHXAVIRZA4KVWLTJJFC7MGXUA74P7UJUAAAAAOQCAQDAQCQMBYIBEFAWDANBYHRAEISCMKBKFQXDAMRUGY4DUAAAAFGBU");
        }

        [TestMethod]
        public void TestRoundTripSignedPayloadVersionByte()
        {
            var data = new List<byte>()
            {
                //ED25519
                0x36, 0x3e, 0xaa, 0x38, 0x67, 0x84, 0x1f, 0xba,
                0xd0, 0xf4, 0xed, 0x88, 0xc7, 0x79, 0xe4, 0xfe,
                0x66, 0xe5, 0x6a, 0x24, 0x70, 0xdc, 0x98, 0xc0,
                0xec, 0x9c, 0x07, 0x3d, 0x05, 0xc7, 0xb1, 0x03,

                //Payload length
                0x00, 0x00, 0x00, 0x09,

                //Payload
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00,

                //Padding
                0x00, 0x00, 0x00
            }.ToArray();

            var hashX = "PA3D5KRYM6CB7OWQ6TWYRR3Z4T7GNZLKERYNZGGA5SOAOPIFY6YQGAAAAAEQAAAAAAAAAAAAAAAAAABBXA";
            Assert.AreEqual(hashX, StrKey.EncodeCheck(StrKey.VersionByte.SIGNED_PAYLOAD, data));
            Assert.IsTrue(data.SequenceEqual(StrKey.DecodeCheck(StrKey.VersionByte.SIGNED_PAYLOAD, hashX)));
        }
    }
}