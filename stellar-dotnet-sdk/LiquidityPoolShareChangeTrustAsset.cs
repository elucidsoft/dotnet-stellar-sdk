using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolShareChangeTrustAsset : ChangeTrustAsset
    {
        public LiquidityPoolParameters Parameters { get; set; }

        public LiquidityPoolShareChangeTrustAsset(LiquidityPoolParameters parameters)
        {
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters), "parameters cannot be null");
        }

        public LiquidityPoolID GetLiquidityPoolID()
        {
            return Parameters.GetID();
        }

        public override string GetType()
        {
            return "pool_share";
        }

        public override string ToString()
        {
            return GetLiquidityPoolID().ToString();
        }

        public override int GetHashCode()
        {
            return Parameters.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || typeof(LiquidityPoolShareChangeTrustAsset).Equals(obj.GetType()))
            {
                return false;
            }

            LiquidityPoolShareChangeTrustAsset other = (LiquidityPoolShareChangeTrustAsset)obj;
            return Parameters.Equals(other.Parameters);
        }

        public override int CompareTo(ChangeTrustAsset asset)
        {
            if (asset.GetType() != "pool_share")
            {
                return 1;
            }

            return ToString().CompareTo(((LiquidityPoolShareChangeTrustAsset)asset).ToString());
        }

        public override xdr.ChangeTrustAsset ToXdr()
        {
            xdr.ChangeTrustAsset changeTrustXdr = new xdr.ChangeTrustAsset();
            changeTrustXdr.Discriminant.InnerValue = xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE;

            xdr.LiquidityPoolParameters parameters = Parameters.ToXdr();
            changeTrustXdr.LiquidityPool = parameters;

            return changeTrustXdr;
        }
    }
}
