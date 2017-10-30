using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest.xdr
{
    [TestClass]
    public class XdrDataStreamTest
    {
        public static string BackAndForthStreaming(string inputString)
        {
            XdrDataOutputStream xdrOutputStream = new XdrDataOutputStream();
        }

        [TestMethod]
        public void BackAndForthXdrStreamingWithStandardAscii()
        {

        }
    }
}
