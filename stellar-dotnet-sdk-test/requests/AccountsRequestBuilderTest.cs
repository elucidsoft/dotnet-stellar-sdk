using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class AccountsRequestBuilderTest
    {
        [TestMethod]
        public void TestAccountsBuildUri()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Accounts
                    .Cursor("13537736921089")
                    .Limit(200)
                    .Order(OrderDirection.ASC)
                    .BuildUri();

                Assert.AreEqual("https://horizon-testnet.stellar.org/accounts?cursor=13537736921089&limit=200&order=asc", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestAccountsAccount()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "account.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var account = await server.Accounts.Account(KeyPair.FromAccountId("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7"));

                AccountDeserializerTest.AssertTestData(account);
            }
        }
    }
}