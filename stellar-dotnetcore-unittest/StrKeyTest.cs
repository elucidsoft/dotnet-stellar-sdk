using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class StrKeyTest
    {
        [TestMethod]
        public void TestDecodeEncode()
        {
            string seed = "SDJHRQF4GCMIIKAAAQ6IHY42X73FQFLHUULAPSKKD4DFDM7UXWWCRHBE";
            byte[] secret = StrKey.DecodeCheck(StrKey.VersionByte.SEED, seed);
            string encoded = StrKey.EncodeCheck(StrKey.VersionByte.SEED, secret);

            Assert.AreEqual(seed, encoded);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDecodeInvalidVersionByte()
        {
            string address = "GCZHXL5HXQX5ABDM26LHYRCQZ5OJFHLOPLZX47WEBP3V2PF5AVFK2A5D";
            StrKey.DecodeCheck(StrKey.VersionByte.SEED, address);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestDecodeInvalidSeed()
        {
            string seed = "SAA6NXOBOXP3RXGAXBW6PGFI5BPK4ODVAWITS4VDOMN5C2M4B66ZML";
            StrKey.DecodeCheck(StrKey.VersionByte.SEED, seed);
        }
    }
}
