using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class RequestBuilderStreamableTest
    {
        [TestMethod]
        public void TestHelloStream()
        {
            var streamableTest = new StreamableTest<object>($"\"hello\"{Environment.NewLine}", r => { Assert.IsNull(r); });

            streamableTest.AssertIsValid();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestUknownStream()
        {
            var streamableTest = new StreamableTest<object>($"", r => { Assert.IsNull(r); });

            streamableTest.AssertIsValid();
        }
    }
}