using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.xdr
{
    [TestClass]
    public class PaddingTest
    {
        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void TestString()
        {
            byte[] bytes = { 0, 0, 0, 2, (byte)'a', (byte)'b', 1, 0 };

            try
            {
                String32.Decode(new XdrDataInputStream(bytes));
            }
            catch (IOException expectedException)
            {
                Assert.AreEqual("non-zero padding", expectedException.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(IOException))]
        public void TestVarOpaque()
        {
            byte[] bytes = { 0, 0, 0, 2, (byte)'a', (byte)'b', 1, 0 };
            try
            {
                DataValue.Decode(new XdrDataInputStream(bytes));
            }
            catch (IOException expectedException)
            {
                Assert.AreEqual("non-zero padding", expectedException.Message);
                throw;
            }
        }
    }
}