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
    public class OffersRequestBuilderTest
    {
        [TestMethod]
        public void TestForAccount()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Offers
                    .ForAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/offers?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestOffersExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "offerPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var offerResponsePage = await server.Offers.ForAccount(KeyPair.FromAccountId("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7"))
                    .Execute();

                OfferPageDeserializerTest.AssertTestData(offerResponsePage);
            }
        }
    }
}
