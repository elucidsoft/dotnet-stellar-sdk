using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class SetTrustlineFlagsResult : OperationResult
    {
        public static SetTrustlineFlagsResult FromXdr(xdr.SetTrustLineFlagsResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_CANT_REVOKE:
                    return new SetTrustlineFlagsCantRevoke();
                case xdr.SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_INVALID_STATE:
                    return new SetTrustlineFlagsInvalidState();
                case xdr.SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_MALFORMED:
                    return new SetTrustlineFlagsMalformed();
                case xdr.SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_NO_TRUST_LINE:
                    return new SetTrustlineFlagsNoTrustline();
                case xdr.SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_SUCCESS:
                    return new SetTrustlineFlagsSuccess();
                default:
                    throw new SystemException("Unknown SetTrustlineFlagsResult type");
            }
        }
    }
}
