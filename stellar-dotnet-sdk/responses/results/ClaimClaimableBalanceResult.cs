using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ClaimClaimableBalanceResult : OperationResult
    {
        public static ClaimClaimableBalanceResult FromXdr(xdr.ClaimClaimableBalanceResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_CANNOT_CLAIM:
                    return new ClaimClaimableBalanceCannotClaim();
                case xdr.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_DOES_NOT_EXIST:
                    return new ClaimClaimableBalanceDoesNotExist();
                case xdr.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_LINE_FULL:
                    return new ClaimClaimableBalanceLineFull();
                case xdr.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_NOT_AUTHORIZED:
                    return new ClaimClaimableBalanceNotAuthorized();
                case xdr.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_NO_TRUST:
                    return new ClaimClaimableBalanceNoTrust();
                case xdr.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_SUCCESS:
                    return new ClaimClaimableBalanceSuccess();
                default:
                    throw new SystemException("Unknown ClaimClaimableBalance type");
            }
        }
    }
}
