using System;

namespace stellar_dotnet_sdk
{
    public class ChangeTrustAsset : Asset
    {
        public Asset AssetA { get; set; }

        public Asset AssetB { get; set; }

        public string Code { get; set; }

        public string Issuer { get; set; }

        public ChangeTrustAsset(Asset assetA, Asset assetB)
        {
            AssetA = assetA;
            AssetB = assetB;

        }

        public ChangeTrustAsset(string code)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code), "code cannot be null");
        }

        public ChangeTrustAsset(string code, string issuer)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code), "code cannot be null");
            Issuer = issuer;
        }

        /// <inheritdoc />
        public override string GetType()
        {
            return "liquidity_pool_shares";
        }

        /// <inheritdoc />
        public override xdr.Asset ToXdr()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override string CanonicalName()
        {
            return $"{Code}:{Issuer}";
        }

        public override xdr.ChangeTrustAsset ToChangeTrustAssetXDR()
        {
            var assetAXDR = AssetA.ToXdr();
            var assetBXDR = AssetB.ToXdr();

            var paramsXDR = new xdr.LiquidityPoolConstantProductParameters();
            paramsXDR.AssetA = assetAXDR;
            paramsXDR.AssetB = assetBXDR;
            paramsXDR.Fee = new xdr.Int32(30);

            var changeTrustAssetXDR = new xdr.ChangeTrustAsset();
            changeTrustAssetXDR.LiquidityPool.ConstantProduct = paramsXDR;
            changeTrustAssetXDR.LiquidityPool.Discriminant.InnerValue = xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT;

            return changeTrustAssetXDR;
        }

        public static ChangeTrustAsset FromXDR(xdr.ChangeTrustAsset changeTrustAssetXDR)
        {
            ChangeTrustAsset result = null;

            switch(changeTrustAssetXDR.Discriminant.InnerValue)
            {
                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                    result = new ChangeTrustAsset("native");
                    break;

                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                    result = new ChangeTrustAsset(Util.PaddedByteArrayToString(changeTrustAssetXDR.AlphaNum4.AssetCode.InnerValue), KeyPair.FromXdrPublicKey(changeTrustAssetXDR.AlphaNum4.Issuer.InnerValue).AccountId);
                    break;

                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                    result = new ChangeTrustAsset(Util.PaddedByteArrayToString(changeTrustAssetXDR.AlphaNum12.AssetCode.InnerValue), KeyPair.FromXdrPublicKey(changeTrustAssetXDR.AlphaNum12.Issuer.InnerValue).AccountId);
                    break;

                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE:
                    var assetA = Asset.FromXdr(changeTrustAssetXDR.LiquidityPool.ConstantProduct.AssetA);
                    var assetB = Asset.FromXdr(changeTrustAssetXDR.LiquidityPool.ConstantProduct.AssetB);
                    result = new ChangeTrustAsset(assetA, assetB);
                    break;
            }

            return result;
        }
    }
}