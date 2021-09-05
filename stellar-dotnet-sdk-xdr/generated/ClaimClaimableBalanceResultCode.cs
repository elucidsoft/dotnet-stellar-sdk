// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum ClaimClaimableBalanceResultCode
    //  {
    //      CLAIM_CLAIMABLE_BALANCE_SUCCESS = 0,
    //      CLAIM_CLAIMABLE_BALANCE_DOES_NOT_EXIST = -1,
    //      CLAIM_CLAIMABLE_BALANCE_CANNOT_CLAIM = -2,
    //      CLAIM_CLAIMABLE_BALANCE_LINE_FULL = -3,
    //      CLAIM_CLAIMABLE_BALANCE_NO_TRUST = -4,
    //      CLAIM_CLAIMABLE_BALANCE_NOT_AUTHORIZED = -5
    //  
    //  };

    //  ===========================================================================
    public class ClaimClaimableBalanceResultCode
    {
        public enum ClaimClaimableBalanceResultCodeEnum
        {
            CLAIM_CLAIMABLE_BALANCE_SUCCESS = 0,
            CLAIM_CLAIMABLE_BALANCE_DOES_NOT_EXIST = -1,
            CLAIM_CLAIMABLE_BALANCE_CANNOT_CLAIM = -2,
            CLAIM_CLAIMABLE_BALANCE_LINE_FULL = -3,
            CLAIM_CLAIMABLE_BALANCE_NO_TRUST = -4,
            CLAIM_CLAIMABLE_BALANCE_NOT_AUTHORIZED = -5,
        }
        public ClaimClaimableBalanceResultCodeEnum InnerValue { get; set; } = default(ClaimClaimableBalanceResultCodeEnum);

        public static ClaimClaimableBalanceResultCode Create(ClaimClaimableBalanceResultCodeEnum v)
        {
            return new ClaimClaimableBalanceResultCode
            {
                InnerValue = v
            };
        }

        public static ClaimClaimableBalanceResultCode Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_SUCCESS);
                case -1: return Create(ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_DOES_NOT_EXIST);
                case -2: return Create(ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_CANNOT_CLAIM);
                case -3: return Create(ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_LINE_FULL);
                case -4: return Create(ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_NO_TRUST);
                case -5: return Create(ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_NOT_AUTHORIZED);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, ClaimClaimableBalanceResultCode value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
