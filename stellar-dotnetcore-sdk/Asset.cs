using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public abstract class Asset
    {
        /**
   * Creates one of AssetTypeCreditAlphaNum4 or AssetTypeCreditAlphaNum12 object based on a <code>code</code> length
   * @param code Asset code
   * @param issuer Asset issuer
   */
        public static Asset CreateNonNativeAsset(String code, KeyPair issuer)
        {
            if (code.Length >= 1 && code.Length <= 4)
            {
                return new AssetTypeCreditAlphaNum4(code, issuer);
            }
            else if (code.Length >= 5 && code.Length <= 12)
            {
                return new AssetTypeCreditAlphaNum12(code, issuer);
            }
            else
            {
                throw new AssetCodeLengthInvalidException();
            }
        }

        /**
         * Generates Asset object from a given XDR object
         * @param xdr XDR object
         */
        public static Asset FromXdr(xdr.Asset thisXdr)
        {
            switch (thisXdr.Discriminant.InnerValue)
            {
                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                    return new AssetTypeNative();
                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                    String assetCode4 = Util.PaddedByteArrayToString(thisXdr.AlphaNum4.AssetCode);
                    KeyPair issuer4 = KeyPair.FromXdrPublicKey(thisXdr.AlphaNum4.Issuer.InnerValue);
                    return new AssetTypeCreditAlphaNum4(assetCode4, issuer4);
                case xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                    String assetCode12 = Util.PaddedByteArrayToString(thisXdr.AlphaNum12.AssetCode);
                    KeyPair issuer12 = KeyPair.FromXdrPublicKey(thisXdr.AlphaNum12.Issuer.InnerValue);
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
        public abstract new String GetType();

        
        public override abstract bool Equals(Object obj);

        /**
         * Generates XDR object from a given Asset object
         */
        public abstract xdr.Asset ToXdr();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
