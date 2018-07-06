using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class FriendBotRequestBuilderTest
    {

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
    }
}
