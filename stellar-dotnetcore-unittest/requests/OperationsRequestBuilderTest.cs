using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_unittest.requests
{
    [TestClass]
    public class OperationsRequestBuilderTest
    {
        [TestMethod]
        public void TestOperations()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Operations
                .Limit(200)
                .Order(OrderDirection.DESC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operations?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestForAccount()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Operations
                .ForAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                .Limit(200)
                .Order(OrderDirection.DESC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/operations?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestLedger()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Operations
                .ForLedger(200000000000L)
                .Limit(50)
                .Order(OrderDirection.ASC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers/200000000000/operations?limit=50&order=asc", uri.ToString());
        }

        [TestMethod]
        public void TestTransaction()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Operations
                .ForTransaction("991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3")
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/transactions/991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3/operations", uri.ToString());
        }
    }
}
