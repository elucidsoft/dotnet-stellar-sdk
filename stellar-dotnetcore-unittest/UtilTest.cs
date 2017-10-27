using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void TestBytesToHexAndHexToBytes()
        {
            string test = "This is a test of this method, 1234567890:;''<>!@#$%^&*()";
            byte[] byteTest = Encoding.Default.GetBytes(test);

            var bytesToHex = Util.BytesToHex(byteTest);
            var hexToBytes = Util.HexToBytes(bytesToHex);

            var bytesToString = Encoding.Default.GetString(hexToBytes);

            Assert.AreEqual(test, bytesToString);
        }

        [TestMethod]
        public void TestPaddedByteArrayWithBytes()
        {
            byte[] testBytes = Encoding.Default.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            byte[] result = Util.PaddedByteArray(testBytes, 40);

            for (int i = 26; i < result.Length; i++)
            {
                Assert.AreEqual(result[i], 0);
            }
        }

        [TestMethod]
        public void TestPaddedByteArrayWithString()
        {
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] result = Util.PaddedByteArray(testString, 40);

            for (int i = 26; i < result.Length; i++)
            {
                Assert.AreEqual(result[i], 0);
            }
        }

        [TestMethod]
        public void TestPaddedByteArrayToString()
        {
            string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] result = Util.PaddedByteArray(testString, 40);

            string stringResult = Util.PaddedByteArrayToString(result);

            Assert.IsTrue(!stringResult.Contains("0"));
        }
    }
}
