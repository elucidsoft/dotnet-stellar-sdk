using System;
using System.Collections.Generic;
using System.Linq;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk.responses
{
    public class TransactionResult
    {
        /// <summary>
        /// Actual fee charged for the transaction.
        /// </summary>
        public string FeeCharged { get; set; }

        public virtual bool IsSuccess { get; }

        public static TransactionResult FromXdr(string encoded)
        {
            byte[] bytes = Convert.FromBase64String(encoded);
            var result = xdr.TransactionResult.Decode(new xdr.XdrDataInputStream(bytes));
            return FromXdr(result);
        }

        public static TransactionResult FromXdr(xdr.TransactionResult result)
        {
            var feeCharged = Amount.FromXdr(result.FeeCharged.InnerValue);

            switch (result.Result.Discriminant.InnerValue)
            {
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txSUCCESS:
                    return new TransactionResultSuccess
                    {
                        FeeCharged = feeCharged,
                        Results = ResultsFromXdr(result.Result.Results)
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txFAILED:
                    return new TransactionResultFailed
                    {
                        FeeCharged = feeCharged,
                        Results = ResultsFromXdr(result.Result.Results)
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txTOO_EARLY:
                    return new TransactionResultTooEarly
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txTOO_LATE:
                    return new TransactionResultTooLate
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txMISSING_OPERATION:
                    return new TransactionResultMissingOperation
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txBAD_SEQ:
                    return new TransactionResultBadSeq
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txBAD_AUTH:
                    return new TransactionResultBadAuth
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txINSUFFICIENT_BALANCE:
                    return new TransactionResultInsufficientBalance
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txNO_ACCOUNT:
                    return new TransactionResultNoAccount
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txINSUFFICIENT_FEE:
                    return new TransactionResultInsufficientFee
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txBAD_AUTH_EXTRA:
                    return new TransactionResultBadAuthExtra
                    {
                        FeeCharged = feeCharged
                    };
                case xdr.TransactionResultCode.TransactionResultCodeEnum.txINTERNAL_ERROR:
                    return new TransactionResultInternalError
                    {
                        FeeCharged = feeCharged
                    };
                default:
                    throw new SystemException("Unknown TransactionResult type");
            }
        }

        private static IList<OperationResult> ResultsFromXdr(xdr.OperationResult[] xdrResults)
        {
            return xdrResults.Select(OperationResult.FromXdr).ToList();
        }
    }
}