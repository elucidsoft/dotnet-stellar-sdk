using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public abstract class Asset
    {
        /**
   * Creates one of AssetTypeCreditAlphaNum4 or AssetTypeCreditAlphaNum12 object based on a <code>code</code> length
   * @param code Asset code
   * @param issuer Asset issuer
   */
        public static Asset CreateNonNativeAsset(string code, KeyPair issuer)
        {
            if (code.Length >= 1 && code.Length <= 4)
                return new AssetTypeCreditAlphaNum4(code, issuer);
            if (code.Length >= 5 && code.Length <= 12)
                return new AssetTypeCreditAlphaNum12(code, issuer);
            throw new AssetCodeLengthInvalidException();
        }

        /**
         * Generates Asset object from a given XDR object
         * @param xdr XDR object
         */
        public static Asset FromXdr(xdr.Asset thisXdr)
        {
            switch (thisXdr.Discriminant.InnerValue)
            {
                case AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                    return new AssetTypeNative();
                case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                    var assetCode4 = Util.PaddedByteArrayToString(thisXdr.AlphaNum4.AssetCode);
                    var issuer4 = KeyPair.FromXdrPublicKey(thisXdr.AlphaNum4.Issuer.InnerValue);
                    return new AssetTypeCreditAlphaNum4(assetCode4, issuer4);
                case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                    var assetCode12 = Util.PaddedByteArrayToString(thisXdr.AlphaNum12.AssetCode);
                    var issuer12 = KeyPair.FromXdrPublicKey(thisXdr.AlphaNum12.Issuer.InnerValue);
                    return new AssetTypeCreditAlphaNum12(assetCode12, issuer12);
                default:
                    throw new ArgumentException("Unknown asset type " + thisXdr.Discriminant.InnerValue);
            }
        }

        /**
         * Returns asset type. Possible types:
         * <ul>
         *   <li><code>native</code></li>
         *   <li><code>credit_alphanum4</code></li>
         *   <li><code>credit_alphanum12</code></li>
         * </ul>
         */
        public new abstract string GetType();


        public abstract override bool Equals(object obj);

        /**
         * Generates XDR object from a given Asset object
         */
        public abstract xdr.Asset ToXdr();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Asset CreateNonNativeAsset(string assetType, string accountId, string code)
        {
            if (assetType == "native")
                return new AssetTypeNative();

            KeyPair issuer = KeyPair.FromAccountId(accountId);
            return CreateNonNativeAsset(code, issuer);
        }
    }
}