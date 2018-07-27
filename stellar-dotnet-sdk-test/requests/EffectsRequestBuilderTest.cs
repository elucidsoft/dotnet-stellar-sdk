using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.effects;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class EffectsRequestBuilderTest
    {
        [TestMethod]
        public void TestEffects()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Effects
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/effects?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForAccount()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Effects
                    .ForAccount(KeyPair.FromAccountId("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H"))
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/effects?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForLedger()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Effects
                    .ForLedger(200000000000L)
                    .Limit(50)
                    .Order(OrderDirection.ASC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/ledgers/200000000000/effects?limit=50&order=asc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForTransaction()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Effects
                    .ForTransaction("991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3")
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/transactions/991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3/effects", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForOperation()
        {
            using (Server server = new Server("https://horizon-testnet.stellar.org"))
            {
                Uri uri = server.Effects
                    .ForOperation(28798257847L)
                    .Cursor("85794837")
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/operations/28798257847/effects?cursor=85794837", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestEffectsCreatedExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "effectPage.json"));

            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var effectsPage = await server.Effects
                    .ForAccount(KeyPair.FromAccountId("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7"))
                    .Execute();

                EffectsPageDeserializeTest.AssertTestData(effectsPage);
            }
        }

        [TestMethod]
        public void TestStream()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCreated.json"));
            var streamableTest = new StreamableTest<EffectResponse>(json, EffectDeserializerTest.AssertAccountCreatedData);

            streamableTest.AssertIsValid();

        }

        [TestMethod]
        public void TestStreamCursor()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCreated.json"));
            var streamableTest = new StreamableTest<EffectResponse>(json, (r) => 
            {
                //do nothing
            });

            streamableTest.AssertIsValid();

            var url = streamableTest.Uri;
            Assert.AreEqual(streamableTest.Uri, "https://horizon-testnet.stellar.org/test?cursor=65571265847297-1");
        }
    }
}
