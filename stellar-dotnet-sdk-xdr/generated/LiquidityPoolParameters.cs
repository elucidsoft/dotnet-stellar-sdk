// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  union LiquidityPoolParameters switch (LiquidityPoolType type)
    //  {
    //  case LIQUIDITY_POOL_CONSTANT_PRODUCT:
    //      LiquidityPoolConstantProductParameters constantProduct;
    //  };

    //  ===========================================================================
    public class LiquidityPoolParameters
    {
        public LiquidityPoolParameters() { }

        public LiquidityPoolType Discriminant { get; set; } = new LiquidityPoolType();

        public LiquidityPoolConstantProductParameters ConstantProduct { get; set; }
        public static void Encode(XdrDataOutputStream stream, LiquidityPoolParameters encodedLiquidityPoolParameters)
        {
            stream.WriteInt((int)encodedLiquidityPoolParameters.Discriminant.InnerValue);
            switch (encodedLiquidityPoolParameters.Discriminant.InnerValue)
            {
                case LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT:
                    LiquidityPoolConstantProductParameters.Encode(stream, encodedLiquidityPoolParameters.ConstantProduct);
                    break;
            }
        }
        public static LiquidityPoolParameters Decode(XdrDataInputStream stream)
        {
            LiquidityPoolParameters decodedLiquidityPoolParameters = new LiquidityPoolParameters();
            LiquidityPoolType discriminant = LiquidityPoolType.Decode(stream);
            decodedLiquidityPoolParameters.Discriminant = discriminant;
            switch (decodedLiquidityPoolParameters.Discriminant.InnerValue)
            {
                case LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT:
                    decodedLiquidityPoolParameters.ConstantProduct = LiquidityPoolConstantProductParameters.Decode(stream);
                    break;
            }
            return decodedLiquidityPoolParameters;
        }
    }
}
