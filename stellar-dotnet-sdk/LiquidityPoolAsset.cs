using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolAsset
    {
        public Asset AssetA { get; set; }
        public Asset AssetB { get; set; }
        public int Fee { get; set; }

        public LiquidityPoolAsset(Asset assetA, Asset assetB, int fee)
        {
            if (assetA == null)
            {
                throw new ArgumentNullException(nameof(assetA), "assetA is not valid");
            }

            if (assetB == null)
            {
                throw new ArgumentException(nameof(assetB), "assetB is not valid");
            }

            if (Asset.Compare(assetA, assetB) != -1)
            {
                throw new ArgumentException("Assets are not in lexicographic order");
            }

            //Only 30 for now is supported LiquidityPoolFeeV18
            if (fee != 30)
            {
                throw new ArgumentException("Fee is invalid");
            }

            AssetA = assetA;
            AssetB = assetB;
            Fee = fee;
        }

        public static LiquidityPoolAsset FromOperation(xdr.ChangeTrustAsset assetXDR)
        {
            var assetType = assetXDR.Discriminant.InnerValue;
            if (assetType == xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE)
            {
                var liquidityPoolParameters = assetXDR.LiquidityPool.ConstantProduct;
                return new LiquidityPoolAsset(
                    Asset.FromXdr(liquidityPoolParameters.AssetA),
                    Asset.FromXdr(liquidityPoolParameters.AssetB),
                    liquidityPoolParameters.Fee.InnerValue);
            }

            throw new ArgumentException($"Invalid asset type: {assetType}");
        }

        public xdr.ChangeTrustAsset ToXdr()
        {
            var constantProductParamsXDR = new xdr.LiquidityPoolConstantProductParameters() { AssetA = AssetA.ToXdr(), AssetB = AssetB.ToXdr(), Fee = new xdr.Int32(Fee) };
            var liquidityPoolParametersXDR = new xdr.LiquidityPoolParameters() { ConstantProduct = constantProductParamsXDR };
            liquidityPoolParametersXDR.Discriminant.InnerValue = xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT;

            var changeTrustAssetXDR = new xdr.ChangeTrustAsset();
            changeTrustAssetXDR.Discriminant.InnerValue = xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE;
            changeTrustAssetXDR.LiquidityPool = liquidityPoolParametersXDR;

            return changeTrustAssetXDR;
        }

    }
}
/**
 * @returns {LiquidityPoolParameters} Liquidity pool parameters.
 */
getLiquidityPoolParameters() {
    return clone({
    assetA: this.assetA,
      assetB: this.assetB,
      fee: this.fee
    });
}

/**
 * @see [Assets concept](https://www.stellar.org/developers/guides/concepts/assets.html)
 * @returns {AssetType.liquidityPoolShares} asset type. Can only be `liquidity_pool_shares`.
 */
getAssetType() {
    return 'liquidity_pool_shares';
}

/**
 * @param {LiquidityPoolAsset} other the LiquidityPoolAsset to compare
 * @returns {boolean} `true` if this asset equals the given asset.
 */
equals(other) {
    return (
      this.assetA.equals(other.assetA) &&
      this.assetB.equals(other.assetB) &&
      this.fee === other.fee
    );
}

toString() {
    const poolId = getLiquidityPoolId(
      'constant_product',
      this.getLiquidityPoolParameters()
    ).toString('hex');
    return `liquidity_pool:${ poolId}`;
}
}