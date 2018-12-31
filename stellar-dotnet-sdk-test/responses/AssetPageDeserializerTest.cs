using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.page;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class AssetPageDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeAssetPage()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "assetPage.json"));
            var assetsPage = JsonSingleton.GetInstance<Page<AssetResponse>>(json);

            AssertTestData(assetsPage);
        }

        [TestMethod]
        public void TestSerializeDeserializeAssetPage()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "assetPage.json"));
            var assetsPage = JsonConvert.DeserializeObject<Page<AssetResponse>>(json);
            var serialized = JsonConvert.SerializeObject(assetsPage);
            var back = JsonConvert.DeserializeObject<Page<AssetResponse>>(serialized);
            AssertTestData(back);
        }

        public static void AssertTestData(Page<AssetResponse> assetsPage)
        {
            Assert.AreEqual("https://horizon-testnet.stellar.org/assets?cursor=&limit=200&order=desc", assetsPage.Links.Self.Href);
            Assert.AreEqual("https://horizon-testnet.stellar.org/assets?cursor=XYZ_GBHLV3AOWIUIHECHYPO5YAVCFJVJS3EKVH5F744VJBIQQQ6NZCFBJJEL_credit_alphanum4&limit=200&order=desc", assetsPage.Links.Next.Href);

            Assert.AreEqual("credit_alphanum4", assetsPage.Records[0].AssetType);
            Assert.AreEqual("ZZZ", assetsPage.Records[0].AssetCode);
            Assert.AreEqual("GCWMJP3GFA2V3M2GSTJUC7H3NM27XG6GDGWJZVM3S536JWYIS6BIWS35", assetsPage.Records[0].AssetIssuer);
            Assert.AreEqual("ZZZ_GCWMJP3GFA2V3M2GSTJUC7H3NM27XG6GDGWJZVM3S536JWYIS6BIWS35_credit_alphanum4", assetsPage.Records[0].PagingToken);
            Assert.AreEqual("1200000000.0000000", assetsPage.Records[0].Amount);
            Assert.AreEqual(1, assetsPage.Records[0].NumAccounts);
            Assert.AreEqual("", assetsPage.Records[0].Links.Toml.Href);
            Assert.AreEqual(false, assetsPage.Records[0].Flags.AuthRequired);
            Assert.AreEqual(false, assetsPage.Records[0].Flags.AuthRevocable);
        }
    }
}