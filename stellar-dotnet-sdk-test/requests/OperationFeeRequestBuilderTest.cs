using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test.requests
{
    public class OperationFeeRequestBuilderTest
    {
        public void TestBuilder()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.OperationFeeStats.BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operation_fee_stats", uri.ToString());
        }
    }
}
