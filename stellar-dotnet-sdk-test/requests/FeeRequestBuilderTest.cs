using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class FeeRequestBuilderTest
    {
        [TestMethod]
        public void TestBuilder()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.FeeStats.BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/fee_stats", uri.ToString());
        }

        [TestMethod]
        public async Task TestExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "feeStats.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var fees = await server.FeeStats.Execute();
                FeeStatsDeserializerTest.AssertTestData(fees);
            }
        }
    }
}
