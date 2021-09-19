using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolID
    {
        public byte[] Hash { get; set; }

        public LiquidityPoolID(LiquidityPoolType type, Asset assetA, Asset assetB, int fee)
        {
            if(assetA.CompareTo(assetB) >= 0)
            {
                throw new Exception("Asset A must be < Asset B (Lexicographic Order)");
            }

            xdr.XdrDataOutputStream xdrDataOutputStream = new xdr.XdrDataOutputStream();

            try
            {
                LiquidityPoolParameters.Create(type, assetA, assetB, fee).ToXdr().Encode(xdrDataOutputStream);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid Liquidity Pool ID", e);
            }

            Hash = Util.Hash(xdrDataOutputStream.ToArray());
        }

        public LiquidityPoolID(string hex)
        {
            Hash = Util.HexToBytes(hex);
        }

        public LiquidityPoolID(byte[] hash)
        {
            Hash = hash;
        }

        public static LiquidityPoolID FromXdr(xdr.PoolID poolIDXdr)
        {
            return new LiquidityPoolID(poolIDXdr.InnerValue.InnerValue);
        }

        public override string ToString()
        {
            return Util.BytesToHex(Hash);
        }

        public xdr.PoolID ToXdr()
        {
            xdr.PoolID poolIDXdr = new xdr.PoolID(new xdr.Hash(Hash));
            return poolIDXdr;
        }
    }
}
