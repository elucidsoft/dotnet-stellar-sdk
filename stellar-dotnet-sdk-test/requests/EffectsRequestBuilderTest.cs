using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.requests;
using stellar_dotnet_sdk.responses.effects;
using stellar_dotnet_sdk_test.responses;
using System.IO;
using System.Threading.Tasks;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class EffectsRequestBuilderTest
    {
        [TestMethod]
        public void TestEffects()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Effects
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/effects?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForAccount()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Effects
                    .ForAccount("GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H")
                    .Limit(200)
                    .Order(OrderDirection.DESC)
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/accounts/GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H/effects?limit=200&order=desc", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForLedger()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Effects
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
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Effects
                    .ForTransaction("991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3")
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/transactions/991534d902063b7715cd74207bef4e7bd7aa2f108f62d3eba837ce6023b2d4f3/effects", uri.ToString());
            }
        }

        [TestMethod]
        public void TestForOperation()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var uri = server.Effects
                    .ForOperation(28798257847L)
                    .Cursor("85794837")
                    .BuildUri();
                Assert.AreEqual("https://horizon-testnet.stellar.org/operations/28798257847/effects?cursor=85794837", uri.ToString());
            }
        }

        [TestMethod]
        public async Task TestEffectsCreatedExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata/effects", "effectPage.json"));

            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var effectsPage = await server.Effects
                    .ForAccount("GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7")
                    .Execute();

                EffectsPageDeserializerTest.AssertTestData(effectsPage);
            }
        }

        [TestMethod]
        public async Task TestStream()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountCreated.json"));

            var streamableTest = new StreamableTest<EffectResponse>(json, EffectDeserializerTest.AssertAccountCreatedData);
            await streamableTest.Run();
        }

        [TestMethod]
        public async Task TestStreamCursor()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountCreated.json"));
            var eventId = "65571265847297-1";
            var streamableTest = new StreamableTest<EffectResponse>(json, EffectDeserializerTest.AssertAccountCreatedData, eventId);
            await streamableTest.Run();

            Assert.AreEqual(eventId, streamableTest.LastEventId);
            Assert.AreEqual("https://horizon-testnet.stellar.org/test?cursor=65571265847297-1", streamableTest.Uri);
        }
    }
}
