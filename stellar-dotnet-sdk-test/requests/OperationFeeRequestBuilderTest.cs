using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class OperationFeeRequestBuilderTest
    {
        [TestMethod]
        public void TestBuilder()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.OperationFeeStats.BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operation_fee_stats", uri.ToString());
        }

        [TestMethod]
        public async Task TestExecute()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            var task = await server.OperationFeeStats.Execute();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operation_fee_stats", task.Uri.ToString());
        }
    }
}
