using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class CreateClaimableBalanceResult : OperationResult
    {
        public static CreateClaimableBalanceResult FromXdr(xdr.CreateClaimableBalanceResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_LOW_RESERVE:
                    return new CreateClaimableBalanceLowReserve();
                case xdr.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_MALFORMED:
                    return new CreateClaimableBalanceMalformed();
                case xdr.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_NOT_AUTHORIZED:
                    return new CreateClaimableBalanceNotAuthorized();
                case xdr.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_NO_TRUST:
                    return new CreateClaimableBalanceNoTrust();
                case xdr.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_SUCCESS:
                    return new CreateClaimableBalanceSuccess();
                case xdr.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_UNDERFUNDED:
                    return new CreateClaimableBalanceUnderfunded();
                default:
                    throw new SystemException("Unknown CreateClaimableBalance type");
            }
        }
    }
}
