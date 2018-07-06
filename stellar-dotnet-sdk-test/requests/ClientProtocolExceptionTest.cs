using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.requests;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class ClientProtocolExceptionTest
    {

        [TestMethod]
        public void TestCreation()
        {
            var clientProtocolException = new ClientProtocolException("Test");
            Assert.AreEqual("Test", clientProtocolException.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(ClientProtocolException))]
        public void TestThrow()
        {
            throw new ClientProtocolException("Test");
        }
    }
}
