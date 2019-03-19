using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class AccountMergeResult : OperationResult
    {
        public static AccountMergeResult FromXdr(xdr.AccountMergeResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_SUCCESS:
                    return new AccountMergeSuccess
                    {
                        SourceAccountBalance = Amount.FromXdr(result.SourceAccountBalance.InnerValue)
                    };
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_MALFORMED:
                    return new AccountMergeMalformed();
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_NO_ACCOUNT:
                    return new AccountMergeNoAccount();
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_IMMUTABLE_SET:
                    return new AccountMergeImmutableSet();
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_HAS_SUB_ENTRIES:
                    return new AccountMergeHasSubEntries();
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_SEQNUM_TOO_FAR:
                    return new AccountMergeSeqnumTooFar();
                case xdr.AccountMergeResultCode.AccountMergeResultCodeEnum.ACCOUNT_MERGE_DEST_FULL:
                    return new AccountMergeDestFull();
                default:
                    throw new SystemException("Unknown AccountMerge type");
            }
        }
    }
}