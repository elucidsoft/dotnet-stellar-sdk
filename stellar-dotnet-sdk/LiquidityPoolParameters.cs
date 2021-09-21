using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public abstract class LiquidityPoolParameters
    {
        public static int Fee = 30;

        public static LiquidityPoolParameters Create(xdr.LiquidityPoolType.LiquidityPoolTypeEnum type, Asset assetA, Asset assetB, int feeBP)
        {
            if (type != xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT)
            {
                throw new ArgumentException($"Unknown liquidity pool type {type}");
            }

            return new LiquidityPoolConstantProductParameters(assetA, assetB, feeBP);
        }

        public static LiquidityPoolParameters FromXdr(xdr.LiquidityPoolParameters liquidityPoolParametersXdr)
        {
            switch(liquidityPoolParametersXdr.Discriminant.InnerValue)
            {
                case xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT:
                    return LiquidityPoolConstantProductParameters.FromXdr(liquidityPoolParametersXdr.ConstantProduct);

                default:
                    throw new ArgumentException($"Unknown liquidity pool type {liquidityPoolParametersXdr.Discriminant.InnerValue}");
            }
        }

        public abstract xdr.LiquidityPoolParameters ToXdr();

        public abstract LiquidityPoolID GetID();
    }
}
