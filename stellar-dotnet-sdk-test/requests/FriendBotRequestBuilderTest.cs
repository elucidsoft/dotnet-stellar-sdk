using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk_test.responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class FriendBotRequestBuilderTest
    {
        [TestCleanup]
        public void Cleanup()
        {
            Network.Use(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TestThrowExceptionIfNotTestnetNetwork()
        {
            Network.UsePublicNetwork();

            using (Server server = new Server("https://horizon.stellar.org"))
            {
                var unused = server.TestNetFriendBot;
            }
        }

        [TestMethod]
        public void TestDoNotThrowExceptionIfTestnetNetwork()
        {
            Network.UseTestNetwork();

            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                var unused = server.TestNetFriendBot;
            }
        }

        [TestMethod]
        public void TestFund()
        {
            Network.UseTestNetwork();
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.TestNetFriendBot
                    .FundAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                    .BuildUri();

                Assert.AreEqual("https://horizon-testnet.stellar.org/friendbot?addr=GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestFriendBotExecute()
        {
            Network.UseTestNetwork();

            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "friendBotSuccess.json"));
            var fakeHttpClient = RequestBuilderMock.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var friendBotResponse = await server.TestNetFriendBot
                    .FundAccount(KeyPair.FromAccountId("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7"))
                    .Execute();

                FriendBotResponseTest.AssertSuccessTestData(friendBotResponse);
            }
        }
    }
}
