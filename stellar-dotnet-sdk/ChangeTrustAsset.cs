using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public abstract class ChangeTrustAsset
    {
        public static ChangeTrustAsset Create(string canonicalForm)
        {
            return new Wrapper(Asset.Create(canonicalForm));
        }

        public static ChangeTrustAsset Create(String type, String code, String issuer)
        {
            return new Wrapper(Asset.Create(type, code, issuer));
        }

        public static ChangeTrustAsset Create(Asset asset)
        {
            return new Wrapper(asset);
        }

        public static ChangeTrustAsset Create(LiquidityPoolParameters parameters)
        {
            return new LiquidityPoolShareChangeTrustAsset(parameters);
        }

        public static ChangeTrustAsset Create(TrustlineAsset.Wrapper wrapper)
        {
            return new Wrapper(wrapper.Asset);
        }

        public static ChangeTrustAsset CreateNonNativeAsset(string code, string issuer)
        {
            return Create(Asset.CreateNonNativeAsset(code, issuer));
        }

        public static ChangeTrustAsset FromXdr(xdr.ChangeTrustAsset changeTrustXdr)
        {
            string accountID;
            string assetCode;

            switch (changeTrustXdr.Discriminant.InnerValue)
            {
                case AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                    return Create(new AssetTypeNative());

                case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                    assetCode = Util.PaddedByteArrayToString(changeTrustXdr.AlphaNum4.AssetCode.InnerValue);
                    accountID = KeyPair.FromXdrPublicKey(changeTrustXdr.AlphaNum4.Issuer.InnerValue).AccountId;
                    return Create(new AssetTypeCreditAlphaNum4(assetCode, accountID));

                case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                    assetCode = Util.PaddedByteArrayToString(changeTrustXdr.AlphaNum12.AssetCode.InnerValue);
                    accountID = KeyPair.FromXdrPublicKey(changeTrustXdr.AlphaNum12.Issuer.InnerValue).AccountId;
                    return Create(new AssetTypeCreditAlphaNum12(assetCode, accountID));

                case AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE:
                    return new LiquidityPoolShareChangeTrustAsset(LiquidityPoolParameters.FromXdr(changeTrustXdr.LiquidityPool));

                default:
                    throw new ArgumentException($"Unkown asset type {changeTrustXdr.Discriminant.InnerValue}");
            }
        }

        public new abstract string GetType();

        public new abstract bool Equals(Object obj);

        public abstract int CompareTo(ChangeTrustAsset asset);

        public abstract xdr.ChangeTrustAsset ToXdr();

        public class Wrapper : ChangeTrustAsset
        {
            public Asset Asset { get; set; }

            public Wrapper(Asset asset)
            {
                Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            }

            public override string GetType()
            {
                return Asset.GetType();
            }

            public override bool Equals(object obj)
            {
                if (obj == null || typeof(ChangeTrustAsset).Equals(obj.GetType()))
                {
                    return false;
                }

                ChangeTrustAsset.Wrapper other = (ChangeTrustAsset.Wrapper)obj;
                return Asset.Equals(other.Asset);
            }

            public override int CompareTo(ChangeTrustAsset asset)
            {
                if (asset.GetType() == "pool_share")
                {
                    return -1;
                }

                return Asset.CompareTo(((ChangeTrustAsset.Wrapper)asset).Asset);
            }

            public override xdr.ChangeTrustAsset ToXdr()
            {
                xdr.ChangeTrustAsset changeTrustXdr = new xdr.ChangeTrustAsset();

                xdr.Asset assetXdr = Asset.ToXdr();
                changeTrustXdr.Discriminant = assetXdr.Discriminant;
                changeTrustXdr.AlphaNum4 = assetXdr.AlphaNum4;
                changeTrustXdr.AlphaNum12 = assetXdr.AlphaNum12;

                return changeTrustXdr;
            }
        }
    }
}