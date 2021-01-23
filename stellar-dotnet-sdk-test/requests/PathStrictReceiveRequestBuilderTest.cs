using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class PathStrictReceiveRequestBuilderTest
    {
        [TestMethod]
        public void TestUriBuilder()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var destinationAsset =
                    Asset.CreateNonNativeAsset("USD", "GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V");

                // Technically not a valid request since it contains both a source account and assets
                var req = server.PathStrictReceive
                    .SourceAccount("GARSFJNXJIHO6ULUBK3DBYKVSIZE7SC72S5DYBCHU7DKL22UXKVD7MXP")
                    .SourceAssets(new[] { new AssetTypeNative(), destinationAsset })
                    .DestinationAccount("GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V")
                    .DestinationAsset(destinationAsset)
                    .DestinationAmount("10.1");

                Assert.AreEqual("https://horizon-testnet.stellar.org/paths/strict-receive?" +
                                "source_account=GARSFJNXJIHO6ULUBK3DBYKVSIZE7SC72S5DYBCHU7DKL22UXKVD7MXP&" +
                                "source_assets=native,USD:GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V&" +
                                "destination_account=GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V&" +
                                "destination_asset_type=credit_alphanum4&" +
                                "destination_asset_code=USD&" +
                                "destination_asset_issuer=GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V&" +
                                "destination_amount=10.1",
                    req.BuildUri().ToString());
            }
        }

        [TestMethod]
        public async Task TestExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "pathsPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                var destinationAsset =
                    Asset.CreateNonNativeAsset("USD", "GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V");

                var assets = await server.PathStrictReceive
                    .SourceAccount("GARSFJNXJIHO6ULUBK3DBYKVSIZE7SC72S5DYBCHU7DKL22UXKVD7MXP")
                    .DestinationAccount("GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V")
                    .DestinationAsset(destinationAsset)
                    .DestinationAmount("10.1")
                    .Execute();

                PathsPageDeserializerTest.AssertTestData(assets);
            }
        }
    }
}