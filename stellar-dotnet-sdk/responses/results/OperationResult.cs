using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class OperationResult
    {
        /// <summary>
        /// Whether the operation was successful.
        /// </summary>
        public virtual bool IsSuccess => false;

        public static OperationResult FromXdr(xdr.OperationResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.OperationResultCode.OperationResultCodeEnum.opBAD_AUTH:
                    return new OperationResultBadAuth();
                case xdr.OperationResultCode.OperationResultCodeEnum.opNO_ACCOUNT:
                    return new OperationResultNoAccount();
                case xdr.OperationResultCode.OperationResultCodeEnum.opNOT_SUPPORTED:
                    return new OperationResultNotSupported();
                case xdr.OperationResultCode.OperationResultCodeEnum.opINNER:
                    return InnerOperationResultFromXdr(result.Tr);
                default:
                    throw new SystemException("Unknown OperationResult type");
            }
        }

        private static OperationResult InnerOperationResultFromXdr(xdr.OperationResult.OperationResultTr result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.OperationType.OperationTypeEnum.CREATE_ACCOUNT:
                    return CreateAccountResult.FromXdr(result.CreateAccountResult);
                case xdr.OperationType.OperationTypeEnum.PAYMENT:
                    return PaymentResult.FromXdr(result.PaymentResult);
                case xdr.OperationType.OperationTypeEnum.PATH_PAYMENT_STRICT_RECEIVE:
                    return PathPaymentStrictReceiveResult.FromXdr(result.PathPaymentStrictReceiveResult);
                case xdr.OperationType.OperationTypeEnum.MANAGE_BUY_OFFER:
                    return ManageBuyOfferResult.FromXdr(result.ManageBuyOfferResult);
                case xdr.OperationType.OperationTypeEnum.MANAGE_SELL_OFFER:
                    return ManageSellOfferResult.FromXdr(result.ManageSellOfferResult);
                case xdr.OperationType.OperationTypeEnum.CREATE_PASSIVE_SELL_OFFER:
                    return ManageSellOfferResult.FromXdr(result.CreatePassiveSellOfferResult);
                case xdr.OperationType.OperationTypeEnum.SET_OPTIONS:
                    return SetOptionsResult.FromXdr(result.SetOptionsResult);
                case xdr.OperationType.OperationTypeEnum.CHANGE_TRUST:
                    return ChangeTrustResult.FromXdr(result.ChangeTrustResult);
                case xdr.OperationType.OperationTypeEnum.ALLOW_TRUST:
                    return AllowTrustResult.FromXdr(result.AllowTrustResult);
                case xdr.OperationType.OperationTypeEnum.ACCOUNT_MERGE:
                    return AccountMergeResult.FromXdr(result.AccountMergeResult);
                case xdr.OperationType.OperationTypeEnum.INFLATION:
                    return InflationResult.FromXdr(result.InflationResult);
                case xdr.OperationType.OperationTypeEnum.MANAGE_DATA:
                    return ManageDataResult.FromXdr(result.ManageDataResult);
                case xdr.OperationType.OperationTypeEnum.BUMP_SEQUENCE:
                    return BumpSequenceResult.FromXdr(result.BumpSeqResult);
                case xdr.OperationType.OperationTypeEnum.PATH_PAYMENT_STRICT_SEND:
                    return PathPaymentStrictSendResult.FromXdr(result.PathPaymentStrictSendResult);
                case xdr.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE:
                    return CreateClaimableBalanceResult.FromXdr(result.CreateClaimableBalanceResult);
                case xdr.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE:
                    return ClaimClaimableBalanceResult.FromXdr(result.ClaimClaimableBalanceResult);
                case xdr.OperationType.OperationTypeEnum.BEGIN_SPONSORING_FUTURE_RESERVES:
                    return BeginSponsoringFutureReservesResult.FromXdr(result.BeginSponsoringFutureReservesResult);
                case xdr.OperationType.OperationTypeEnum.END_SPONSORING_FUTURE_RESERVES:
                    return EndSponsoringFutureReservesResult.FromXdr(result.EndSponsoringFutureReservesResult);
                default:
                    throw new SystemException("Unknown OperationType");
            }
        }
    }
}
