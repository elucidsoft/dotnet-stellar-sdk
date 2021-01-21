using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk_test.responses;

namespace stellar_dotnet_sdk_test.requests
{
    [TestClass]
    public class PathStrictSendRequestBuilderTest
    {
        [TestMethod]
        public void TestUriBuilder()
        {
            using (var server = new Server("https://horizon-testnet.stellar.org"))
            {
                var sourceAsset =
                    Asset.CreateNonNativeAsset("USD", "GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V");

                // Technically not a valid request since it contains both a destination account and assets
                var req = server.PathStrictSend
                    .SourceAmount("10.1")
                    .SourceAsset(sourceAsset)
                    .DestinationAccount("GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V")
                    .DestinationAssets(new[] { new AssetTypeNative(), sourceAsset });

                Assert.AreEqual("https://horizon-testnet.stellar.org/paths/strict-send?" +
                                "source_amount=10.1&" +
                                "source_asset_type=credit_alphanum4&" +
                                "source_asset_code=USD&" +
                                "source_asset_issuer=GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V&" +
                                "destination_account=GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V&" +
                                "destination_assets=native,USD:GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V",
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
                var sourceAsset =
                    Asset.CreateNonNativeAsset("USD", "GAEDTJ4PPEFVW5XV2S7LUXBEHNQMX5Q2GM562RJGOQG7GVCE5H3HIB4V");

                var assets = await server.PathStrictSend
                    .SourceAmount("10.1")
                    .SourceAsset(sourceAsset)
                    .DestinationAssets(new[] { new AssetTypeNative(), sourceAsset })
                    .Execute();

                PathsPageDeserializerTest.AssertTestData(assets);
            }
        }
    }
}