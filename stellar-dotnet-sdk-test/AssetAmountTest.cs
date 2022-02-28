using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class AssetAmountTest
    {
        [TestMethod]
        public void TestDefaultConstructor()
        {
            try
            {
                new AssetAmount();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestCreation()
        {
            var issuer = KeyPair.Random();

            var assetAmount = new AssetAmount(Asset.CreateNonNativeAsset("TEST", issuer.AccountId), "100");

            Assert.AreEqual(assetAmount.Asset.CanonicalName(), $"TEST:{issuer.AccountId}");
            Assert.AreEqual(assetAmount.Amount, "100");
        }

        [TestMethod]
        public void TestEquality()
        {
            var issuer = KeyPair.Random();

            var assetAmount = new AssetAmount(Asset.CreateNonNativeAsset("TEST", issuer.AccountId), "100");
            var assetAmount2 = new AssetAmount(Asset.CreateNonNativeAsset("TEST", issuer.AccountId), "100");

            Assert.AreEqual(assetAmount, assetAmount2);
            Assert.AreEqual(assetAmount.GetHashCode(), assetAmount2.GetHashCode());
            Assert.IsFalse(assetAmount.Equals(issuer));
        }
    }
}
