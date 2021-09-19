using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public abstract class TrustlineAsset
    {
        public static TrustlineAsset Create(string canonicalForm)
        {
            return new Wrapper(Asset.Create(canonicalForm));
        }

        public static TrustlineAsset Create(string type, string code, string issuer)
        {
            return new Wrapper(Asset.Create(type, code, issuer));
        }

        public static TrustlineAsset Create(Asset asset)
        {
            return new Wrapper(asset);
        }

        public static TrustlineAsset Create(LiquidityPoolParameters parameters)
        {
            return new LiquidityPoolShareTrustlineAsset(parameters);
        }

        public static TrustlineAsset Create(LiquidityPoolID id)
        {
            return new LiquidityPoolShareTrustlineAsset(id);
        }

        public static TrustlineAsset Create(ChangeTrustAsset.Wrapper wrapper)
        {
            return new Wrapper(wrapper.Asset);
        }

        public static TrustlineAsset Create(LiquidityPoolShareChangeTrustAsset share)
        {
            return new LiquidityPoolShareTrustlineAsset(share.Parameters);
        }

        public static TrustlineAsset CreateNonNativeAsset(string code, string issuer)
        {
            return TrustlineAsset.Create(Asset.CreateNonNativeAsset(code, issuer));
        }

        public static TrustlineAsset FromXdr(xdr.TrustLineAsset trustLineAssetXdr)
        {
            string accountID;
            string assetCode;

            switch(trustLineAssetXdr.Discriminant.InnerValue)
            {
                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                    return TrustlineAsset.Create(new AssetTypeNative());

                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                    assetCode = Util.PaddedByteArrayToString(trustLineAssetXdr.AlphaNum4.AssetCode.InnerValue);
                    accountID = KeyPair.FromXdrPublicKey(trustLineAssetXdr.AlphaNum4.Issuer.InnerValue).AccountId;
                    return TrustlineAsset.Create(new AssetTypeCreditAlphaNum4(assetCode, accountID));

                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                    assetCode = Util.PaddedByteArrayToString(trustLineAssetXdr.AlphaNum12.AssetCode.InnerValue);
                    accountID = KeyPair.FromXdrPublicKey(trustLineAssetXdr.AlphaNum12.Issuer.InnerValue).AccountId;
                    return TrustlineAsset.Create(new AssetTypeCreditAlphaNum12(assetCode, accountID));

                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE:
                    return new LiquidityPoolShareTrustlineAsset(LiquidityPoolID.FromXdr(trustLineAssetXdr.LiquidityPoolID));

                default:
                    throw new ArgumentException($"Unknown asset type {trustLineAssetXdr.Discriminant.InnerValue}");
            }
        }

        public new abstract string GetType();

        public new abstract bool Equals(object obj);

        public abstract int CompareTo(TrustlineAsset asset);

        public abstract xdr.TrustLineAsset ToXdr();

        public class Wrapper :TrustlineAsset
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
                if (obj != null || typeof(Wrapper).Equals(obj.GetType()))
                {
                    return false;
                }

                TrustlineAsset.Wrapper other = (TrustlineAsset.Wrapper)obj;
                return Asset.Equals(other.Asset);
            }

            public override int CompareTo(TrustlineAsset asset)
            {
                if(asset.GetType() == "pool_share")
                {
                    return -1;
                }

                return Asset.CompareTo(((TrustlineAsset.Wrapper)asset).Asset);
            }

            public override xdr.TrustLineAsset ToXdr()
            {
                xdr.TrustLineAsset trustlineAssetXdr = new xdr.TrustLineAsset();

                xdr.Asset assetXdr = Asset.ToXdr();
                trustlineAssetXdr.Discriminant = assetXdr.Discriminant;
                trustlineAssetXdr.AlphaNum4 = assetXdr.AlphaNum4;
                trustlineAssetXdr.AlphaNum12 = assetXdr.AlphaNum12;

                return trustlineAssetXdr;
            }
        }
    }
}
