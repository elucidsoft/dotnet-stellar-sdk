using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk_test.operations
{
    [TestClass]
    public class LiquidityPoolWithdrawOperationTest
    {
        [TestMethod]
        public void TestBuilder1()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var keypairAssetA = KeyPair.Random();
            var keypairAssetB = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypairAssetA.AccountId}");
            var assetB = Asset.Create($"USD:{keypairAssetB.AccountId}");

            var assetAmountA = new AssetAmount(assetA, "100");
            var assetAmountB = new AssetAmount(assetB, "200");

            var operation = new LiquidityPoolWithdrawOperation.Builder(assetAmountA, assetAmountB, "100")
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (LiquidityPoolWithdrawOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(operation.LiquidityPoolID, parsedOperation.LiquidityPoolID);
            Assert.AreEqual(operation.MinAmountA, parsedOperation.MinAmountA);
            Assert.AreEqual(operation.MinAmountB, parsedOperation.MinAmountB);
            Assert.AreEqual(operation.Amount, parsedOperation.Amount);

            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);
        }

        [TestMethod]
        public void TestBuilder2()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var keypairAssetA = KeyPair.Random();
            var keypairAssetB = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypairAssetA.AccountId}");
            var assetB = Asset.Create($"USD:{keypairAssetB.AccountId}");

            var liquidityPoolID = new LiquidityPoolID(
              stellar_dotnet_sdk.xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT,
              assetA,
              assetB,
              LiquidityPoolParameters.Fee
            );

            var operation = new LiquidityPoolWithdrawOperation.Builder(liquidityPoolID, "100", "100", "200")
                .SetSourceAccount(source)
                .Build();

            var xdr = operation.ToXdr();
            var parsedOperation = (LiquidityPoolWithdrawOperation)Operation.FromXdr(xdr);

            Assert.AreEqual(source.AccountId, parsedOperation.SourceAccount.AccountId);
            Assert.AreEqual(operation.LiquidityPoolID, parsedOperation.LiquidityPoolID);
            Assert.AreEqual(operation.MinAmountA, parsedOperation.MinAmountA);
            Assert.AreEqual(operation.MinAmountB, parsedOperation.MinAmountB);
            Assert.AreEqual(operation.Amount, parsedOperation.Amount);

            Assert.AreEqual(OperationThreshold.Medium, parsedOperation.Threshold);
        }

        [TestMethod]
        public void TestNotLexicographicOrder()
        {
            // GC5SIC4E3V56VOHJ3OZAX5SJDTWY52JYI2AFK6PUGSXFVRJQYQXXZBZF
            var source = KeyPair.FromSecretSeed("SC4CGETADVYTCR5HEAVZRB3DZQY5Y4J7RFNJTRA6ESMHIPEZUSTE2QDK");

            var keypairAssetA = KeyPair.Random();
            var keypairAssetB = KeyPair.Random();

            var assetA = Asset.Create($"EUR:{keypairAssetA.AccountId}");
            var assetB = Asset.Create($"USD:{keypairAssetB.AccountId}");

            var assetAmountA = new AssetAmount(assetA, "100");
            var assetAmountB = new AssetAmount(assetB, "200");

            try
            {
                var operation = new LiquidityPoolWithdrawOperation.Builder(assetAmountA, assetAmountB, "100")
                .SetSourceAccount(source)
                .Build();
            }
            catch(Exception e)
            {
                Assert.AreEqual(e.Message, "Asset A must be < Asset B (Lexicographic Order)");
            }
        }


    }
}
