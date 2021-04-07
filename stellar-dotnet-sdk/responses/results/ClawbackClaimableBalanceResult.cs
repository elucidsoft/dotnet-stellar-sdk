using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ClawbackClaimableBalanceResult : OperationResult
    {
        public static ClawbackClaimableBalanceResult FromXdr(xdr.ClawbackClaimableBalanceResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ClawbackClaimableBalanceResultCode.ClawbackClaimableBalanceResultCodeEnum.CLAWBACK_CLAIMABLE_BALANCE_DOES_NOT_EXIST:
                    return new ClawbackClaimableBalanceDoesNotExist();
                case xdr.ClawbackClaimableBalanceResultCode.ClawbackClaimableBalanceResultCodeEnum.CLAWBACK_CLAIMABLE_BALANCE_NOT_CLAWBACK_ENABLED:
                    return new ClawbackClaimableBalanceNotClawbackEnabled();
                case xdr.ClawbackClaimableBalanceResultCode.ClawbackClaimableBalanceResultCodeEnum.CLAWBACK_CLAIMABLE_BALANCE_NOT_ISSUER:
                    return new ClawbackClaimableBalanceNotIssuer();
                case xdr.ClawbackClaimableBalanceResultCode.ClawbackClaimableBalanceResultCodeEnum.CLAWBACK_CLAIMABLE_BALANCE_SUCCESS:
                    return new ClawbackClaimableBalanceSuccess();
                default:
                    throw new SystemException("Unknown ClawbackClaimableBalance type");
            }
        }
    }
}
