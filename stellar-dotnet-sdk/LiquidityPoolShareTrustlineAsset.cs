using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolShareTrustlineAsset : TrustlineAsset
    {
        public LiquidityPoolID ID { get; set; }

        public LiquidityPoolShareTrustlineAsset(LiquidityPoolParameters parameters)
        {
            ID = parameters.GetID() ?? throw new ArgumentNullException(nameof(parameters), "parameters cannot be null");
        }

        public LiquidityPoolShareTrustlineAsset(LiquidityPoolID id)
        {
            ID = id ?? throw new ArgumentNullException(nameof(id), "id cannot be null");
        }

        public override string GetType()
        {
            return "pool_share";
        }

        public override string ToString()
        {
            ID.ToString();
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !typeof(LiquidityPoolShareTrustlineAsset).Equals(obj.GetType()))
            {
                return false;
            }

            LiquidityPoolShareTrustlineAsset other = (LiquidityPoolShareTrustlineAsset)obj;

            return obj.ToString() == other.ToString();
        }

        public override int CompareTo(TrustlineAsset asset)
        {
            if(asset.GetType() != "pool_share")
            {
                return 1;
            }

            return ToString().CompareTo((LiquidityPoolShareTrustlineAsset)asset).ToString();
        }

        public override xdr.TrustLineAsset ToXdr()
        {
            xdr.TrustLineAsset trustlineAssetXdr = new xdr.TrustLineAsset();
            trustlineAssetXdr.Discriminant.InnerValue = xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE;
            trustlineAssetXdr.LiquidityPoolID = ID.ToXdr();
            return xdr;
        }
    }
}
