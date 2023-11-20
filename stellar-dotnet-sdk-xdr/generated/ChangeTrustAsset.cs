// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  union ChangeTrustAsset switch (AssetType type)
//  {
//  case ASSET_TYPE_NATIVE: // Not credit
//      void;
//  
//  case ASSET_TYPE_CREDIT_ALPHANUM4:
//      AlphaNum4 alphaNum4;
//  
//  case ASSET_TYPE_CREDIT_ALPHANUM12:
//      AlphaNum12 alphaNum12;
//  
//  case ASSET_TYPE_POOL_SHARE:
//      LiquidityPoolParameters liquidityPool;
//  
//      // add other asset types here in the future
//  };

//  ===========================================================================
public class ChangeTrustAsset
{
    public AssetType Discriminant { get; set; } = new();

    public AlphaNum4 AlphaNum4 { get; set; }
    public AlphaNum12 AlphaNum12 { get; set; }
    public LiquidityPoolParameters LiquidityPool { get; set; }

    public static void Encode(XdrDataOutputStream stream, ChangeTrustAsset encodedChangeTrustAsset)
    {
        stream.WriteInt((int)encodedChangeTrustAsset.Discriminant.InnerValue);
        switch (encodedChangeTrustAsset.Discriminant.InnerValue)
        {
            case AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                break;
            case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                AlphaNum4.Encode(stream, encodedChangeTrustAsset.AlphaNum4);
                break;
            case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                AlphaNum12.Encode(stream, encodedChangeTrustAsset.AlphaNum12);
                break;
            case AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE:
                LiquidityPoolParameters.Encode(stream, encodedChangeTrustAsset.LiquidityPool);
                break;
        }
    }

    public static ChangeTrustAsset Decode(XdrDataInputStream stream)
    {
        var decodedChangeTrustAsset = new ChangeTrustAsset();
        var discriminant = AssetType.Decode(stream);
        decodedChangeTrustAsset.Discriminant = discriminant;
        switch (decodedChangeTrustAsset.Discriminant.InnerValue)
        {
            case AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                break;
            case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                decodedChangeTrustAsset.AlphaNum4 = AlphaNum4.Decode(stream);
                break;
            case AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                decodedChangeTrustAsset.AlphaNum12 = AlphaNum12.Decode(stream);
                break;
            case AssetType.AssetTypeEnum.ASSET_TYPE_POOL_SHARE:
                decodedChangeTrustAsset.LiquidityPool = LiquidityPoolParameters.Decode(stream);
                break;
        }

        return decodedChangeTrustAsset;
    }
}