using System;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Assets are uniquely identified by the asset code and the issuer. Ultimately, it’s up to the issuer to set the asset code. By convention, however, currencies should be represented by 
    /// the appropriate ISO 4217 code. For stocks and bonds, use the appropriate ISIN number.
    /// </summary>
    public class ChangeTrustAsset : Asset
    {
        public Asset AssetA { get; set; }

        public Asset AssetB { get; set; }

        public string Code { get; set; }

        public string Issuer { get; set; }

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
            throw new NotSupportedException();
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

        public ChangeTrustAsset FromXDR(xdr.ChangeTrustAsset changeTrustAssetXDR)
        {
            ChangeTrustAsset result = null;

            switch(changeTrustAssetXDR.Discriminant.InnerValue)
            {
                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                    result = new ChangeTrustAsset("native");
            }


        }
    }
}