using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using System;

namespace stellar_dotnetcore_unittest.requests
{
    [TestClass]
    public class EffectsRequestBuilderTest
    {
        [TestMethod]
        public void TestEffects()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Effects
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/effects?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestForAccount()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Effects
                    .ForAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/effects?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestForLedger()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Effects
                    .ForLedger(200000000000L)
                    .Limit(50)
                    .Order(OrderDirection.ASC)
                    .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers/200000000000/effects?limit=50&order=asc", uri.ToString());
        }

        [TestMethod]
        public void TestForTransaction()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Effects
                    .ForTransaction("991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3")
                    .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/transactions/991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3/effects", uri.ToString());
        }

        [TestMethod]
        public void TestForOperation()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Effects
                    .ForOperation(28798257847L)
                    .Cursor("85794837")
                    .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operations/28798257847/effects?cursor=85794837", uri.ToString());
        }

    }
}
