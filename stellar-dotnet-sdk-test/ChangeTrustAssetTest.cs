using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using XDR = stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class ChangeTrustAssetTest
    {
        [TestMethod]
        public void TestCreateCanonical()
        {
            var keypair = KeyPair.Random();
            var changeTrustAsset = (ChangeTrustAsset.Wrapper)ChangeTrustAsset.Create($"USD:{keypair.AccountId}");
            Assert.AreEqual(changeTrustAsset.Asset.CanonicalName(), $"USD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestCreateLiquidityPoolParameters()
        {
            var keypair = KeyPair.Random();
            var keypair2 = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypair.AccountId}");
            var assetB = Asset.Create($"USD:{keypair2.AccountId}");

            var parameters = LiquidityPoolParameters.Create(XDR.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA, assetB, 30);

            var liquidityPoolShareChangeTrustAsset = (LiquidityPoolShareChangeTrustAsset)ChangeTrustAsset.Create(parameters);
            var liquidityPoolShareChangeTrustAsset2 = (LiquidityPoolShareChangeTrustAsset)LiquidityPoolShareChangeTrustAsset.FromXdr(liquidityPoolShareChangeTrustAsset.ToXdr());

            Assert.AreEqual(liquidityPoolShareChangeTrustAsset.Parameters.GetID(), liquidityPoolShareChangeTrustAsset2.Parameters.GetID());
        }

        [TestMethod]
        public void TestCreateTrustlineWrapper()
        {
            var keypair = KeyPair.Random();
            var trustlineAsset = (TrustlineAsset.Wrapper)TrustlineAsset.Create("non-native", "USD", keypair.AccountId);
            var changeTrustAsset = (ChangeTrustAsset.Wrapper)ChangeTrustAsset.Create(trustlineAsset);

            Assert.AreEqual(trustlineAsset.Asset, changeTrustAsset.Asset);
        }

        [TestMethod]
        public void TestCreateNonNative()
        {
            var keypair = KeyPair.Random();
            var changeTrustAsset = (ChangeTrustAsset.Wrapper)ChangeTrustAsset.CreateNonNativeAsset("USD", keypair.AccountId);
            Assert.AreEqual(changeTrustAsset.Asset.CanonicalName(), $"USD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestFromXDRAlphaNum4()
        {
            var keypair = KeyPair.Random();

            var changeTrustAssetXdr = new XDR.ChangeTrustAsset();
            changeTrustAssetXdr.Discriminant = new XDR.AssetType() { InnerValue = XDR.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4 };
            changeTrustAssetXdr.AlphaNum4 = Asset.Create($"USD:{keypair.AccountId}").ToXdr().AlphaNum4;

            var changeTrustAsset = (ChangeTrustAsset.Wrapper)ChangeTrustAsset.FromXdr(changeTrustAssetXdr);
            Assert.AreEqual(changeTrustAsset.Asset.CanonicalName(), $"USD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestFromXDRAlphaNum12()
        {
            var keypair = KeyPair.Random();

            var changeTrustAssetXdr = new XDR.ChangeTrustAsset();
            changeTrustAssetXdr.Discriminant = new XDR.AssetType() { InnerValue = XDR.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12 };
            changeTrustAssetXdr.AlphaNum12 = Asset.Create($"USDUSD:{keypair.AccountId}").ToXdr().AlphaNum12;

            var changeTrustAsset = (ChangeTrustAsset.Wrapper)ChangeTrustAsset.FromXdr(changeTrustAssetXdr);
            Assert.AreEqual(changeTrustAsset.Asset.CanonicalName(), $"USDUSD:{keypair.AccountId}");
        }

        [TestMethod]
        public void TestFromXDRNative()
        {
            var changeTrustAssetXdr = new XDR.ChangeTrustAsset();
            changeTrustAssetXdr.Discriminant = new XDR.AssetType() { InnerValue = XDR.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE };

            var trustlineAsset = (ChangeTrustAsset.Wrapper)ChangeTrustAsset.FromXdr(changeTrustAssetXdr);
            Assert.AreEqual(trustlineAsset.Asset.CanonicalName(), "native");
        }
    }
}
