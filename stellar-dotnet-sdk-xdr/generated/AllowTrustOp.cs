// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

using System;

namespace stellar_dotnet_sdk.xdr
{
// === xdr source ============================================================
//  struct AllowTrustOp
//  {
//      AccountID trustor;
//      union switch (AssetType type)
//      {
//      // ASSET_TYPE_NATIVE is not allowed
//      case ASSET_TYPE_CREDIT_ALPHANUM4:
//          AssetCode4 assetCode4;
//  
//      case ASSET_TYPE_CREDIT_ALPHANUM12:
//          AssetCode12 assetCode12;
//  
//          // add other asset types here in the future
//      }
//      asset;
//  
//      // 0, or any bitwise combination of TrustLineFlags
//      uint32 authorize;
//  };
//  ===========================================================================
    public class AllowTrustOp
    {
        public AllowTrustOp()
        {
        }

        public AccountID Trustor { get; set; }
        public AllowTrustOpAsset Asset { get; set; }
        public Uint32 Authorize { get; set; }

        public static void Encode(XdrDataOutputStream stream, AllowTrustOp encodedAllowTrustOp)
        {
            AccountID.Encode(stream, encodedAllowTrustOp.Trustor);
            AllowTrustOpAsset.Encode(stream, encodedAllowTrustOp.Asset);
            Uint32.Encode(stream, encodedAllowTrustOp.Authorize);
        }

        public static AllowTrustOp Decode(XdrDataInputStream stream)
        {
            AllowTrustOp decodedAllowTrustOp = new AllowTrustOp();
            decodedAllowTrustOp.Trustor = AccountID.Decode(stream);
            decodedAllowTrustOp.Asset = AllowTrustOpAsset.Decode(stream);
            decodedAllowTrustOp.Authorize = Uint32.Decode(stream);
            return decodedAllowTrustOp;
        }

        public class AllowTrustOpAsset
        {
            public AllowTrustOpAsset()
            {
            }

            public AssetType Discriminant { get; set; } = new AssetType();

            public AssetCode4 AssetCode4 { get; set; }
            public AssetCode12 AssetCode12 { get; set; }

            public static void Encode(XdrDataOutputStream stream, AllowTrustOpAsset encodedAllowTrustOpAsset)
            {
                stream.WriteInt((int) encodedAllowTrustOpAsset.Discriminant.InnerValue);
                switch (encodedAllowTrustOpAsset.Discriminant.InnerValue)
                {
                    case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                        AssetCode4.Encode(stream, encodedAllowTrustOpAsset.AssetCode4);
                        break;
                    case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                        AssetCode12.Encode(stream, encodedAllowTrustOpAsset.AssetCode12);
                        break;
                }
            }

            public static AllowTrustOpAsset Decode(XdrDataInputStream stream)
            {
                AllowTrustOpAsset decodedAllowTrustOpAsset = new AllowTrustOpAsset();
                AssetType discriminant = AssetType.Decode(stream);
                decodedAllowTrustOpAsset.Discriminant = discriminant;
                switch (decodedAllowTrustOpAsset.Discriminant.InnerValue)
                {
                    case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                        decodedAllowTrustOpAsset.AssetCode4 = AssetCode4.Decode(stream);
                        break;
                    case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                        decodedAllowTrustOpAsset.AssetCode12 = AssetCode12.Decode(stream);
                        break;
                }

                return decodedAllowTrustOpAsset;
            }
        }
    }
}