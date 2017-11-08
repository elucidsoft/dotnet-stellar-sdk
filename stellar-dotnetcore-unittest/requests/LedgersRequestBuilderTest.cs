using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using System;


namespace stellar_dotnetcore_unittest.requests
{
    [TestClass]
    public class LedgersRequestBuilderTest
    {
        [TestMethod]
        public void TestAccounts()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Ledgers
                    .Limit(200)
                    .Order(OrderDirection.ASC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers?limit=200&order=asc", uri.ToString());
            }
        }
    }
}
