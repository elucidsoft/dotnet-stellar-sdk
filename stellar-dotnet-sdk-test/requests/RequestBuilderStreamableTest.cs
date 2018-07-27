using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.effects;
using stellar_dotnet_sdk_test.responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class RequestBuilderStreamableTest
    {

        [TestMethod]
        public void TestHelloStream()
        {
            var streamableTest = new StreamableTest<object>($"\"hello\"{Environment.NewLine}", (r) =>
            {
                Assert.IsNull(r);
            });

            streamableTest.AssertIsValid();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestUknownStream()
        {
            var streamableTest = new StreamableTest<object>($"", (r) =>
            {
                Assert.IsNull(r);
            });

            streamableTest.AssertIsValid();
        }

    }
}
