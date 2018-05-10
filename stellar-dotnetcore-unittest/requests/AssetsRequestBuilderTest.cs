using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest.requests
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
    }
}
