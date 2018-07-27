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
    public class AssetsRequestBuilderTest
    {
        [TestMethod]
        public void TestAssets()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Assets
                .Limit(200)
                .Order(OrderDirection.DESC)
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/assets?limit=200&order=desc", uri.ToString());
        }

        [TestMethod]
        public void TestAssetsAssetCode()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Assets
                .AssetCode("USD")
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/assets?asset_code=USD", uri.ToString());
        }

        [TestMethod]
        public void TestAssetsAssetIssuer()
        {
            Server server = new Server("https://horizon-testnet.stellar.org");
            Uri uri = server.Assets
                .AssetIssuer("GA2HGBJIJKI6O4XEM7CZWY5PS6GKSXL6D34ERAJYQSPYA6X6AI7HYW36")
                .BuildUri();
            Assert.AreEqual("https://horizon-testnet.stellar.org/assets?asset_issuer=GA2HGBJIJKI6O4XEM7CZWY5PS6GKSXL6D34ERAJYQSPYA6X6AI7HYW36", uri.ToString());
        }

        [TestMethod]
        public async Task TestAssetExecute()
        {
            var jsonResponse = File.ReadAllText(Path.Combine("testdata", "assetPage.json"));
            var fakeHttpClient = FakeHttpClient.CreateFakeHttpClient(jsonResponse);

            using (var server = new Server("https://horizon-testnet.stellar.org", fakeHttpClient))
            {
                //the assetcode string really doesn't matter for testing, as the response is static for testing purposes...
                var assetsPage = await server.Assets.AssetCode("") 
                    .Execute();

                AssetPageDeserializerTest.AssertTestData(assetsPage);
            }
        }
    }
}
