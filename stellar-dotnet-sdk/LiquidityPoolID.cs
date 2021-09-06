using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolID
    {
        public string PoolID { get; set; }
        public LiquidityPoolID(string poolID)
        {
            PoolID = poolID ?? throw new ArgumentNullException(nameof(PoolID), "poolID cannot be null");
            if(!Regex.IsMatch(poolID, "^[a-f0-9]{64}$"))
            {
                throw new ArgumentNullException(nameof(poolID), "poolID is not a valid hash");
            }

            PoolID = poolID;
        }

        public static LiquidityPoolID FromOperation(xdr.TrustLineAsset trustlineAssetXDR)
        {
            if(trustlineAssetXDR.Discriminant.InnerValue == xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE)
            {
                new LiquidityPoolID(Util.BytesToHex(trustlineAssetXDR.LiquidityPoolID.InnerValue.InnerValue));
            }

            throw new ArgumentNullException(nameof(trustlineAssetXDR), "Invalid Asset Type");
        }

        public xdr.TrustLineAsset ToXdr()
        {
            var xdrPoolID = Util.HexToBytes(PoolID);
            var xdrTrustlineAsset = new xdr.TrustLineAsset();
            xdrTrustlineAsset.Discriminant.InnerValue = xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE;
            xdrTrustlineAsset.LiquidityPoolID.InnerValue.InnerValue = xdrPoolID;
            return xdrTrustlineAsset;
        }
    }

    getAssetType()
    {
        return 'liquidity_pool_shares';
    }