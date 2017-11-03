using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_unittest.requests
{
    [TestClass]
    public class AccountsRequestBuilderTest
    {
        [TestMethod]
        public void TestAccounts()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Accounts
                .Cursor("13537736921089")
                .Limit(200)
                .Order(OrderDirection.ASC)
                .BuildUri();

            Assert.AreEqual("https://horizon-testnet.stellar.org/accounts?cursor=13537736921089&limit=200&order=asc", uri.ToString());
        }
    }
}