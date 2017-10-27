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
        public void TestBytesToHex()
        {
            string test = "This is a test of this method, 1234567890:;''<>!@#$%^&*()";
            byte[] byteTest = Encoding.Default.GetBytes(test);

            var bytesToHex = Util.BytesToHex(byteTest);
            var hexToBytes = Util.HexToBytes(bytesToHex);

            var bytesToString = Encoding.Default.GetString(hexToBytes);

            Assert.AreEqual(test, bytesToString);
        }
    }
}
