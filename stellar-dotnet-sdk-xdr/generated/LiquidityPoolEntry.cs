// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  struct LiquidityPoolEntry
    //  {
    //      PoolID liquidityPoolID;
    //  
    //      union switch (LiquidityPoolType type)
    //      {
    //      case LIQUIDITY_POOL_CONSTANT_PRODUCT:
    //          struct
    //          {
    //              LiquidityPoolConstantProductParameters params;
    //  
    //              int64 reserveA;        // amount of A in the pool
    //              int64 reserveB;        // amount of B in the pool
    //              int64 totalPoolShares; // total number of pool shares issued
    //              int64 poolSharesTrustLineCount; // number of trust lines for the associated pool shares
    //          } constantProduct;
    //      }
    //      body;
    //  };

    //  ===========================================================================
    public class LiquidityPoolEntry
    {
        public LiquidityPoolEntry() { }
        public PoolID LiquidityPoolID { get; set; }
        public LiquidityPoolEntryBody Body { get; set; }

        public static void Encode(XdrDataOutputStream stream, LiquidityPoolEntry encodedLiquidityPoolEntry)
        {
            PoolID.Encode(stream, encodedLiquidityPoolEntry.LiquidityPoolID);
            LiquidityPoolEntryBody.Encode(stream, encodedLiquidityPoolEntry.Body);
        }
        public static LiquidityPoolEntry Decode(XdrDataInputStream stream)
        {
            LiquidityPoolEntry decodedLiquidityPoolEntry = new LiquidityPoolEntry();
            decodedLiquidityPoolEntry.LiquidityPoolID = PoolID.Decode(stream);
            decodedLiquidityPoolEntry.Body = LiquidityPoolEntryBody.Decode(stream);
            return decodedLiquidityPoolEntry;
        }

        public class LiquidityPoolEntryBody
        {
            public LiquidityPoolEntryBody() { }

            public LiquidityPoolType Discriminant { get; set; } = new LiquidityPoolType();

            public LiquidityPoolEntryConstantProduct ConstantProduct { get; set; }
            public static void Encode(XdrDataOutputStream stream, LiquidityPoolEntryBody encodedLiquidityPoolEntryBody)
            {
                stream.WriteInt((int)encodedLiquidityPoolEntryBody.Discriminant.InnerValue);
                switch (encodedLiquidityPoolEntryBody.Discriminant.InnerValue)
                {
                    case LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT:
                        LiquidityPoolEntryConstantProduct.Encode(stream, encodedLiquidityPoolEntryBody.ConstantProduct);
                        break;
                }
            }
            public static LiquidityPoolEntryBody Decode(XdrDataInputStream stream)
            {
                LiquidityPoolEntryBody decodedLiquidityPoolEntryBody = new LiquidityPoolEntryBody();
                LiquidityPoolType discriminant = LiquidityPoolType.Decode(stream);
                decodedLiquidityPoolEntryBody.Discriminant = discriminant;
                switch (decodedLiquidityPoolEntryBody.Discriminant.InnerValue)
                {
                    case LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT:
                        decodedLiquidityPoolEntryBody.ConstantProduct = LiquidityPoolEntryConstantProduct.Decode(stream);
                        break;
                }
                return decodedLiquidityPoolEntryBody;
            }

            public class LiquidityPoolEntryConstantProduct
            {
                public LiquidityPoolEntryConstantProduct() { }
                public LiquidityPoolConstantProductParameters Params { get; set; }
                public Int64 ReserveA { get; set; }
                public Int64 ReserveB { get; set; }
                public Int64 TotalPoolShares { get; set; }
                public Int64 PoolSharesTrustLineCount { get; set; }

                public static void Encode(XdrDataOutputStream stream, LiquidityPoolEntryConstantProduct encodedLiquidityPoolEntryConstantProduct)
                {
                    LiquidityPoolConstantProductParameters.Encode(stream, encodedLiquidityPoolEntryConstantProduct.Params);
                    Int64.Encode(stream, encodedLiquidityPoolEntryConstantProduct.ReserveA);
                    Int64.Encode(stream, encodedLiquidityPoolEntryConstantProduct.ReserveB);
                    Int64.Encode(stream, encodedLiquidityPoolEntryConstantProduct.TotalPoolShares);
                    Int64.Encode(stream, encodedLiquidityPoolEntryConstantProduct.PoolSharesTrustLineCount);
                }
                public static LiquidityPoolEntryConstantProduct Decode(XdrDataInputStream stream)
                {
                    LiquidityPoolEntryConstantProduct decodedLiquidityPoolEntryConstantProduct = new LiquidityPoolEntryConstantProduct();
                    decodedLiquidityPoolEntryConstantProduct.Params = LiquidityPoolConstantProductParameters.Decode(stream);
                    decodedLiquidityPoolEntryConstantProduct.ReserveA = Int64.Decode(stream);
                    decodedLiquidityPoolEntryConstantProduct.ReserveB = Int64.Decode(stream);
                    decodedLiquidityPoolEntryConstantProduct.TotalPoolShares = Int64.Decode(stream);
                    decodedLiquidityPoolEntryConstantProduct.PoolSharesTrustLineCount = Int64.Decode(stream);
                    return decodedLiquidityPoolEntryConstantProduct;
                }

            }
        }
    }
}
