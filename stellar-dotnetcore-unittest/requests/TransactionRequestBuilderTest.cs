using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using System;

namespace stellar_dotnetcore_unittest.requests
{
    [TestClass]
    class TransactionRequestBuilderTest
    {
        [TestMethod]
        public void TestTransactions()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Transactions
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/transactions?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForAccount()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Transactions
                    .ForAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/transactions?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForLedger()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Transactions
                    .ForLedger(200000000000L)
                    .Limit(50)
                    .Order(OrderDirection.ASC)
                    .BuildUri();

                Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers/200000000000/transactions?limit=50&order=asc", uri.ToString());
            }
        }
    }
}
