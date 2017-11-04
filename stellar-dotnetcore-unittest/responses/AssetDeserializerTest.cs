using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using stellar_dotnetcore_sdk.responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnetcore_unittest.responses
{
    [TestClass]
    public class AssetDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeNative()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "assetAssetTypeNative.json"));
            Asset asset = JsonSingleton.GetInstance<Asset>(json);

            Assert.AreEqual(asset.GetType(), "native");

        }

        [TestMethod]
        public void TestDeserializeCredit()
        {
            var json = File.ReadAllText(Path.Combine("responses", "testdata", "assetAssetTypeCredit.json"));
            Asset asset = JsonSingleton.GetInstance<Asset>(json);
            Assert.AreEqual(asset.GetType(), "credit_alphanum4");
            AssetTypeCreditAlphaNum creditAsset = (AssetTypeCreditAlphaNum)asset;
            Assert.AreEqual(creditAsset.Code, "CNY");
            Assert.AreEqual(creditAsset.Issuer.AccountId, "GAREELUB43IRHWEASCFBLKHURCGMHE5IF6XSE7EXDLACYHGRHM43RFOX");
        }
    }
}