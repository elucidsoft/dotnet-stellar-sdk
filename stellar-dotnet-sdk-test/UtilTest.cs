using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class UtilTest
    {
        [TestMethod]
        public void TestBytesToHexAndHexToBytes()
        {
            const string test = "This is a test of this method, 1234567890:;''<>!@#$%^&*()";
            var byteTest = Encoding.Default.GetBytes(test);

            var bytesToHex = Util.BytesToHex(byteTest);
            var hexToBytes = Util.HexToBytes(bytesToHex);

            var bytesToString = Encoding.Default.GetString(hexToBytes);

            Assert.AreEqual(test, bytesToString);
        }

        [TestMethod]
        public void TestPaddedByteArrayWithBytes()
        {
            var testBytes = Encoding.Default.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var result = Util.PaddedByteArray(testBytes, 40);

            for (var i = 26; i < result.Length; i++)
                Assert.AreEqual(result[i], 0);
        }

        [TestMethod]
        public void TestPaddedByteArrayWithString()
        {
            const string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = Util.PaddedByteArray(testString, 40);

            for (var i = 26; i < result.Length; i++)
                Assert.AreEqual(result[i], 0);
        }

        [TestMethod]
        public void TestPaddedByteArrayToString()
        {
            const string testString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = Util.PaddedByteArray(testString, 40);

            var stringResult = Util.PaddedByteArrayToString(result);

            Assert.IsTrue(!stringResult.Contains("0"));
        }

        [TestMethod]
        public void TestIsIdentical()
        {
            var bytes = Encoding.UTF8.GetBytes("Something cool");
            var bytes2 = Encoding.UTF8.GetBytes("Something cool");

            Assert.IsTrue(bytes.IsIdentical(bytes2));
        }
    }
}