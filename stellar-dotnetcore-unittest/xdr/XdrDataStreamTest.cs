using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.xdr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnetcore_unittest.xdr
{
    [TestClass]
    public class XdrDataStreamTest
    {
        public static string BackAndForthXdrStreaming(string inputString)
        {
            XdrDataOutputStream xdrOutputStream = new XdrDataOutputStream();
            xdrOutputStream.WriteString(inputString);

            byte[] xdrByteOutput = xdrOutputStream.ToArray();

            //XDR back to string
            XdrDataInputStream xdrInputStream = new XdrDataInputStream(xdrByteOutput);
            string outputString = xdrInputStream.ReadString();

            return outputString;
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithStandardAscii()
        {
            string memo = "Dollar Sign $";
            Assert.AreEqual(memo, BackAndForthXdrStreaming(memo));
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithNonStandardAscii()
        {
            string memo = "Euro Sign €";
            Assert.AreEqual(memo, BackAndForthXdrStreaming(memo));
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithAllNonStandardAscii()
        {
            string memo = "øûý™€♠♣♥†‡µ¢£€";
            Assert.AreEqual(memo, BackAndForthXdrStreaming(memo));
    }
}
}
