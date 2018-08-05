using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class AssetTest
    {
        [TestMethod]
        public void TestAssetTypeNative()
        {
            var asset = new AssetTypeNative();
            var thisXdr = asset.ToXdr();
            var parsedAsset = Asset.FromXdr(thisXdr);
            Assert.IsTrue(parsedAsset is AssetTypeNative);
        }

        [TestMethod]
        public void TestAssetCreation()
        {
            var nativeAsset = Asset.Create("native", null, null);
            Assert.IsTrue(nativeAsset is AssetTypeNative);

            var nonNativeAsset = Asset.Create(null, "XLMTEST", "GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");
            Assert.IsTrue(nonNativeAsset is AssetTypeCreditAlphaNum);
        }

        [TestMethod]
        public void TestAssetTypeCreditAlphaNum4()
        {
            var code = "USDA";
            var issuer = KeyPair.Random();
            var asset = new AssetTypeCreditAlphaNum4(code, issuer);
            var thisXdr = asset.ToXdr();
            var parsedAsset = (AssetTypeCreditAlphaNum4) Asset.FromXdr(thisXdr);
            Assert.AreEqual(code, asset.Code);
            Assert.AreEqual(issuer.AccountId, parsedAsset.Issuer.AccountId);
        }

        [TestMethod]
        public void TestAssetTypeCreditAlphaNum12()
        {
            var code = "TESTTEST";
            var issuer = KeyPair.Random();
            var asset = new AssetTypeCreditAlphaNum12(code, issuer);
            var thisXdr = asset.ToXdr();
            var parsedAsset = (AssetTypeCreditAlphaNum12) Asset.FromXdr(thisXdr);
            Assert.AreEqual(code, asset.Code);
            Assert.AreEqual(issuer.AccountId, parsedAsset.Issuer.AccountId);
        }

        [TestMethod]
        public void TestHashCode()
        {
            var issuer1 = KeyPair.Random();
            var issuer2 = KeyPair.Random();

            // Equal
            Assert.AreEqual(new AssetTypeNative().GetHashCode(), new AssetTypeNative().GetHashCode());
            Assert.AreEqual(new AssetTypeCreditAlphaNum4("USD", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum4("USD", issuer1).GetHashCode());
            Assert.AreEqual(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum12("ABCDE", issuer1).GetHashCode());

            // Not equal
            Assert.AreNotEqual(new AssetTypeNative().GetHashCode(), new AssetTypeCreditAlphaNum4("USD", issuer1).GetHashCode());
            Assert.AreNotEqual(new AssetTypeNative().GetHashCode(), new AssetTypeCreditAlphaNum12("ABCDE", issuer1).GetHashCode());
            Assert.AreNotEqual(new AssetTypeCreditAlphaNum4("EUR", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum4("USD", issuer1).GetHashCode());
            Assert.AreNotEqual(new AssetTypeCreditAlphaNum4("EUR", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum4("EUR", issuer2).GetHashCode());
            Assert.AreNotEqual(new AssetTypeCreditAlphaNum4("EUR", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum12("EUROPE", issuer1).GetHashCode());
            Assert.AreNotEqual(new AssetTypeCreditAlphaNum4("EUR", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum12("EUROPE", issuer2).GetHashCode());
            Assert.AreNotEqual(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum12("EDCBA", issuer1).GetHashCode());
            Assert.AreNotEqual(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).GetHashCode(), new AssetTypeCreditAlphaNum12("ABCDE", issuer2).GetHashCode());
        }

        [TestMethod]
        public void TestAssetEquals()
        {
            var issuer1 = KeyPair.Random();
            var issuer2 = KeyPair.Random();

            Assert.AreEqual(new AssetTypeNative(), new AssetTypeNative());
            Assert.AreEqual(new AssetTypeCreditAlphaNum4("USD", issuer1), new AssetTypeCreditAlphaNum4("USD", issuer1));
            Assert.AreEqual(new AssetTypeCreditAlphaNum12("ABCDE", issuer1), new AssetTypeCreditAlphaNum12("ABCDE", issuer1));

            Assert.AreNotEqual(new AssetTypeNative(), new AssetTypeCreditAlphaNum4("USD", issuer1));
            Assert.AreNotEqual(new AssetTypeNative(), new AssetTypeCreditAlphaNum12("ABCDE", issuer1));
            Assert.IsFalse(new AssetTypeCreditAlphaNum4("EUR", issuer1).Equals(new AssetTypeCreditAlphaNum4("USD", issuer1)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum4("EUR", issuer1).Equals(new AssetTypeCreditAlphaNum4("EUR", issuer2)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).Equals(new AssetTypeCreditAlphaNum12("EDCBA", issuer1)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).Equals(new AssetTypeCreditAlphaNum12("ABCDE", issuer2)));
        }
    }
}