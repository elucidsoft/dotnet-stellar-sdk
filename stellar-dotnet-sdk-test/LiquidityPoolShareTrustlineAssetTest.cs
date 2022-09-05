using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class LiquidityPoolShareTrustlineAssetTest
    {
        [TestMethod]
        public void TestEquality()
        {
            var keypair = KeyPair.Random();
            var keypair2 = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypair.AccountId}");
            var assetB = Asset.Create($"USD:{keypair2.AccountId}");

            var trustlineAsset = (LiquidityPoolShareTrustlineAsset)TrustlineAsset.Create(LiquidityPoolParameters.Create(stellar_dotnet_sdk.xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA, assetB, 30));
            var trustlineAsset2 = new LiquidityPoolShareTrustlineAsset(trustlineAsset.ID);
            Assert.IsTrue(trustlineAsset.Equals(trustlineAsset2));
        }

        [TestMethod]
        public void TestType()
        {
            var keypair = KeyPair.Random();
            var keypair2 = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypair.AccountId}");
            var assetB = Asset.Create($"USD:{keypair2.AccountId}");

            var trustlineAsset = (LiquidityPoolShareTrustlineAsset)TrustlineAsset.Create(LiquidityPoolParameters.Create(stellar_dotnet_sdk.xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA, assetB, 30));
            var trustlineAsset2 = new LiquidityPoolShareTrustlineAsset(trustlineAsset.ID);
            Assert.AreEqual(trustlineAsset.Type, "pool_share");
        }
    }
}
