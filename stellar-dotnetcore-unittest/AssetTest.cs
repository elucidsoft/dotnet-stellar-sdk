using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class AssetTest
    {
        [TestMethod]
        public void testAssetTypeNative()
        {
            AssetTypeNative asset = new AssetTypeNative();
            stellar_dotnetcore_sdk.xdr.Asset thisXdr = asset.ToXdr();
            Asset parsedAsset = Asset.FromXdr(thisXdr);
            Assert.IsTrue(parsedAsset is AssetTypeNative);
        }

        [TestMethod]
        public void testAssetTypeCreditAlphaNum4()
        {
            String code = "USDA";
            KeyPair issuer = KeyPair.Random();
            AssetTypeCreditAlphaNum4 asset = new AssetTypeCreditAlphaNum4(code, issuer);
            stellar_dotnetcore_sdk.xdr.Asset thisXdr = asset.ToXdr();
            AssetTypeCreditAlphaNum4 parsedAsset = (AssetTypeCreditAlphaNum4)Asset.FromXdr(thisXdr);
            Assert.AreEqual(code, asset.Code);
            Assert.AreEqual(issuer.AccountId, parsedAsset.Issuer.AccountId);
        }

        [TestMethod]
        public void testAssetTypeCreditAlphaNum12()
        {
            String code = "TESTTEST";
            KeyPair issuer = KeyPair.Random();
            AssetTypeCreditAlphaNum12 asset = new AssetTypeCreditAlphaNum12(code, issuer);
            stellar_dotnetcore_sdk.xdr.Asset thisXdr = asset.ToXdr();
            AssetTypeCreditAlphaNum12 parsedAsset = (AssetTypeCreditAlphaNum12)Asset.FromXdr(thisXdr);
            Assert.AreEqual(code, asset.Code);
            Assert.AreEqual(issuer.AccountId, parsedAsset.Issuer.AccountId);
        }

        [TestMethod]
        public void testHashCode()
        {
            KeyPair issuer1 = KeyPair.Random();
            KeyPair issuer2 = KeyPair.Random();

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
        public void testAssetEquals()
        {
            KeyPair issuer1 = KeyPair.Random();
            KeyPair issuer2 = KeyPair.Random();

            Assert.IsTrue(new AssetTypeNative().Equals(new AssetTypeNative()));
            Assert.IsTrue(new AssetTypeCreditAlphaNum4("USD", issuer1).Equals(new AssetTypeCreditAlphaNum4("USD", issuer1)));
            Assert.IsTrue(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).Equals(new AssetTypeCreditAlphaNum12("ABCDE", issuer1)));

            Assert.IsFalse(new AssetTypeNative().Equals(new AssetTypeCreditAlphaNum4("USD", issuer1)));
            Assert.IsFalse(new AssetTypeNative().Equals(new AssetTypeCreditAlphaNum12("ABCDE", issuer1)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum4("EUR", issuer1).Equals(new AssetTypeCreditAlphaNum4("USD", issuer1)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum4("EUR", issuer1).Equals(new AssetTypeCreditAlphaNum4("EUR", issuer2)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).Equals(new AssetTypeCreditAlphaNum12("EDCBA", issuer1)));
            Assert.IsFalse(new AssetTypeCreditAlphaNum12("ABCDE", issuer1).Equals(new AssetTypeCreditAlphaNum12("ABCDE", issuer2)));
        }
    }
}
