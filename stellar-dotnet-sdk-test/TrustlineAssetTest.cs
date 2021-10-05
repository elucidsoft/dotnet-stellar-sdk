using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using XDR = stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class TrustlineAssetTest
    {
        [TestMethod]
        public void TestCreateCanonicalName()
        {
            var keypair = KeyPair.Random();
            var trustlineAsset = TrustlineAsset.Create($"USD:{keypair.AccountId}");
            Assert.AreEqual(((TrustlineAsset.Wrapper)trustlineAsset).Asset.CanonicalName(), $"USD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestCreate()
        {
            var keypair = KeyPair.Random();
            var trustlineAsset = TrustlineAsset.Create("non-native", "USD", keypair.AccountId);
            Assert.AreEqual(((TrustlineAsset.Wrapper)trustlineAsset).Asset.CanonicalName(), $"USD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestCreateParameters()
        {
            var keypair = KeyPair.Random();
            var keypair2 = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypair.AccountId}");
            var assetB = Asset.Create($"USD:{keypair2.AccountId}");

            var trustlineAsset = (LiquidityPoolShareTrustlineAsset)TrustlineAsset.Create(LiquidityPoolParameters.Create(stellar_dotnet_sdk.xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA, assetB, 30));
            var trustlineAsset2 = new LiquidityPoolShareTrustlineAsset(trustlineAsset.ID);
            Assert.AreEqual(trustlineAsset.ID, trustlineAsset2.ID);
        }

        [TestMethod]
        public void TestCreateShareChangeTrust()
        {
            var keypair = KeyPair.Random();
            var keypair2 = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypair.AccountId}");
            var assetB = Asset.Create($"USD:{keypair2.AccountId}");

            var liquidityPoolParameters = LiquidityPoolParameters.Create(stellar_dotnet_sdk.xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA, assetB, 30);
            var trustlineAsset = (LiquidityPoolShareTrustlineAsset)TrustlineAsset.Create(new LiquidityPoolShareChangeTrustAsset(liquidityPoolParameters));
            var trustlineAsset2 = new LiquidityPoolShareTrustlineAsset(trustlineAsset.ID);
            Assert.AreEqual(trustlineAsset.ID, trustlineAsset2.ID);
        }

        [TestMethod]
        public void TestCreateLiquidityPoolID()
        {
            var keypair = KeyPair.Random();
            var keypair2 = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypair.AccountId}");
            var assetB = Asset.Create($"USD:{keypair2.AccountId}");

            var liquidityPoolParameters = LiquidityPoolParameters.Create(stellar_dotnet_sdk.xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA, assetB, 30);
            var trustlineAsset = (LiquidityPoolShareTrustlineAsset)TrustlineAsset.Create(new LiquidityPoolShareChangeTrustAsset(liquidityPoolParameters));
            var trustlineAsset2 = (LiquidityPoolShareTrustlineAsset)TrustlineAsset.Create(trustlineAsset.ID);
            Assert.AreEqual(trustlineAsset.ID, trustlineAsset2.ID);
        }

        [TestMethod]
        public void TestFromXDRAlphaNum4()
        {
            var keypair = KeyPair.Random();

            var trustlineAssetNativeXdr = new XDR.TrustLineAsset();
            trustlineAssetNativeXdr.Discriminant = new XDR.AssetType() { InnerValue = XDR.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4 };
            trustlineAssetNativeXdr.AlphaNum4 = Asset.Create($"USD:{keypair.AccountId}").ToXdr().AlphaNum4;

            var trustlineAsset = (TrustlineAsset.Wrapper)TrustlineAsset.FromXdr(trustlineAssetNativeXdr);
            Assert.AreEqual(trustlineAsset.Asset.CanonicalName(), $"USD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestFromXDRAlphaNum12()
        {
            var keypair = KeyPair.Random();

            var trustlineAssetNativeXdr = new XDR.TrustLineAsset();
            trustlineAssetNativeXdr.Discriminant = new XDR.AssetType() { InnerValue = XDR.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12 };
            trustlineAssetNativeXdr.AlphaNum12 = Asset.Create($"USDUSD:{keypair.AccountId}").ToXdr().AlphaNum12;

            var trustlineAsset = (TrustlineAsset.Wrapper)TrustlineAsset.FromXdr(trustlineAssetNativeXdr);
            Assert.AreEqual(trustlineAsset.Asset.CanonicalName(), $"USDUSD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestFromXDRNative()
        {
            var trustlineAssetNativeXdr = new XDR.TrustLineAsset();
            trustlineAssetNativeXdr.Discriminant = new XDR.AssetType() { InnerValue = XDR.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE };

            var trustlineAsset = (TrustlineAsset.Wrapper)TrustlineAsset.FromXdr(trustlineAssetNativeXdr);
            Assert.AreEqual(trustlineAsset.Asset.CanonicalName(), "native");
        }
    }
}
