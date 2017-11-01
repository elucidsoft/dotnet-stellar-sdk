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
            MemoryStream byteOutputStream = new MemoryStream();
            XdrDataOutputStream xdrOutputStream = new XdrDataOutputStream(byteOutputStream);
            xdrOutputStream.WriteString(inputString);

            byte[] xdrByteOutput = byteOutputStream.ToArray();

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
    }
}
