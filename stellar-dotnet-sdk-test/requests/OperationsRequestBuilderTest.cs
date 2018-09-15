using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses.operations;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class OperationsRequestBuilderTest
    {
        [TestMethod]
        public void TestOperations()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Operations
                .Limit(200)
                .Order(OrderDirection.DESC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operations?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestOperationDetails()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Operations
                .Operation(77309415424)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/operations/77309415424", uri.ToString());
        }

        [TestMethod]
        public void TestForAccount()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Operations
                .ForAccount("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H")
                .Limit(200)
                .Order(OrderDirection.DESC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/operations?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestLedger()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Operations
                .ForLedger(200000000000L)
                .Limit(50)
                .Order(OrderDirection.ASC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers/200000000000/operations?limit=50&order=asc", uri.ToString());
        }

        [TestMethod]
        public void TestTransaction()
        {
            var server = new Server("https://horizon-testnet.stellar.org");
            var uri = server.Operations
                .ForTransaction("991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3")
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/transactions/991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3/operations", uri.ToString());
        }

        [TestMethod]
        public async Task TestOperationsExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "operationPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var account = await server.Operations.ForAccount("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7")
                    .Execute();

                OperationPageDeserializerTest.AssertTestData(account);
            }
        }

        [TestMethod]
        public void TestStream()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "operationCreateAccount.json"));
            var streamableTest = new StreamableTest<OperationResponse>(json, OperationDeserializerTest.AssertCreateAccountOperationData);

            streamableTest.AssertIsValid();
        }
    }
}