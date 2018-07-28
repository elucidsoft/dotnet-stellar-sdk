using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class TransactionRequestBuilderTest
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

        [TestMethod]
        public async Task TestTransactionsExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "transactionPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var account = await server.Transactions
                    .ForAccount(KeyPair.FromAccountId("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7"))
                    .Execute();

                TransactionPageDeserializeTest.AssertTestData(account);
            }
        }

        [TestMethod]
        public void TestStream()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "transactionTransaction.json"));
            var streamableTest = new StreamableTest<TransactionResponse>(json, TransactionDeserializerTest.AssertTestData);

            streamableTest.AssertIsValid();
        }
    }
}
