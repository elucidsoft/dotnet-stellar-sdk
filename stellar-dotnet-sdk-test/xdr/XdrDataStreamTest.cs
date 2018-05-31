using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.xdr
{
    [TestClass]
    public class XdrDataStreamTest
    {
        public static string BackAndForthXdrStreaming(string inputString)
        {
            var xdrOutputStream = new XdrDataOutputStream();
            xdrOutputStream.WriteString(inputString);

            var xdrByteOutput = xdrOutputStream.ToArray();

            //XDR back to string
            var xdrInputStream = new XdrDataInputStream(xdrByteOutput);
            var outputString = xdrInputStream.ReadString();

            return outputString;
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithStandardAscii()
        {
            var memo = "Dollar Sign $";
            Assert.AreEqual(memo, BackAndForthXdrStreaming(memo));
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithNonStandardAscii()
        {
            var memo = "Euro Sign €";
            Assert.AreEqual(memo, BackAndForthXdrStreaming(memo));
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithAllNonStandardAscii()
        {
            var memo = "øûý™€♠♣♥†‡µ¢£€";
            Assert.AreEqual(memo, BackAndForthXdrStreaming(memo));
        }
    }
}