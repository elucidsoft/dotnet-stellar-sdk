using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolConstantProductParameters : LiquidityPoolParameters
    {
        public Asset AssetA { get; set; }
        public Asset AssetB { get; set; }
        public int Fee { get; set; }

        public LiquidityPoolConstantProductParameters(Asset assetA, Asset assetB, int feeBP)
        {
            AssetA = assetA ?? throw new ArgumentNullException(nameof(assetA), "assetA cannot be null");
            AssetB = assetB ?? throw new ArgumentNullException(nameof(assetB), "assetB cannot be null");
            Fee = feeBP;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || typeof(LiquidityPoolConstantProductParameters).Equals(obj.GetType()) {
                return false;
            }

            LiquidityPoolConstantProductParameters other = (LiquidityPoolConstantProductParameters)obj;
            return Equals(AssetA, other.AssetA) && Equals(AssetB, other.AssetB) && Equals(Fee, other.Fee);
        }

        public override xdr.LiquidityPoolParameters ToXdr()
        {
            xdr.LiquidityPoolParameters liquidityPoolParametersXdr = new xdr.LiquidityPoolParameters();
            liquidityPoolParametersXdr.Discriminant.InnerValue = xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT;

            xdr.LiquidityPoolConstantProductParameters parameters = new xdr.LiquidityPoolConstantProductParameters();
            parameters.AssetA = AssetA.ToXdr();
            parameters.AssetB = AssetB.ToXdr();
            parameters.Fee = new xdr.Int32(Fee);

            liquidityPoolParametersXdr.ConstantProduct = parameters;

            return liquidityPoolParametersXdr;
        }

        public static LiquidityPoolConstantProductParameters FromXdr(xdr.LiquidityPoolConstantProductParameters liquidityPoolConstantProductParametersXdr)
        {
            return new LiquidityPoolConstantProductParameters(Asset.FromXdr(liquidityPoolConstantProductParametersXdr.AssetA),
                                                              Asset.FromXdr(liquidityPoolConstantProductParametersXdr.AssetB),
                                                              liquidityPoolConstantProductParametersXdr.Fee.InnerValue);
        }

        public override LiquidityPoolID GetID()
        {
            return new LiquidityPoolID(LiquidityPoolType.LIQUIDITY_POOL_CONSTANT_PRODUCT, AssetA, AssetB, Fee);
        }
    }
}
