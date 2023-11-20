// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  union ClaimableBalanceID switch (ClaimableBalanceIDType type)
//  {
//  case CLAIMABLE_BALANCE_ID_TYPE_V0:
//      Hash v0;
//  };

//  ===========================================================================
public class ClaimableBalanceID
{
    public ClaimableBalanceIDType Discriminant { get; set; } = new();

    public Hash V0 { get; set; }

    public static void Encode(XdrDataOutputStream stream, ClaimableBalanceID encodedClaimableBalanceID)
    {
        stream.WriteInt((int)encodedClaimableBalanceID.Discriminant.InnerValue);
        switch (encodedClaimableBalanceID.Discriminant.InnerValue)
        {
            case ClaimableBalanceIDType.ClaimableBalanceIDTypeEnum.CLAIMABLE_BALANCE_ID_TYPE_V0:
                Hash.Encode(stream, encodedClaimableBalanceID.V0);
                break;
        }
    }

    public static ClaimableBalanceID Decode(XdrDataInputStream stream)
    {
        var decodedClaimableBalanceID = new ClaimableBalanceID();
        var discriminant = ClaimableBalanceIDType.Decode(stream);
        decodedClaimableBalanceID.Discriminant = discriminant;
        switch (decodedClaimableBalanceID.Discriminant.InnerValue)
        {
            case ClaimableBalanceIDType.ClaimableBalanceIDTypeEnum.CLAIMABLE_BALANCE_ID_TYPE_V0:
                decodedClaimableBalanceID.V0 = Hash.Decode(stream);
                break;
        }

        return decodedClaimableBalanceID;
    }
}