using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class PaymentsRequestBuilderTest
    {
        [TestMethod]
        public void TestPayments()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Payments
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/payments?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForAccount()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Payments
                    .ForAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/payments?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForLedger()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Payments
                    .ForLedger(200000000000L)
                    .Limit(50)
                    .Order(OrderDirection.ASC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers/200000000000/payments?limit=50&order=asc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForTransaction()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Payments
                    .ForTransaction("991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3")
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/transactions/991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3/payments", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestPaymentsExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "operationPage.json"));
            var fakeHttpClient = RequestBuilderMock.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var payments = await server.Payments
                    .ForAccount(KeyPair.FromAccountId("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7"))
                    .Execute();

                OperationPageDeserializerTest.AssertTestData(payments);
            }
        }

    }
}
