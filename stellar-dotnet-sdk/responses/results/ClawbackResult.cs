using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ClawbackResult : OperationResult
    {
        public static ClawbackResult FromXdr(xdr.ClawbackResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ClawbackResultCode.ClawbackResultCodeEnum.CLAWBACK_MALFORMED:
                    return new ClawbackMalformed();
                case xdr.ClawbackResultCode.ClawbackResultCodeEnum.CLAWBACK_NOT_CLAWBACK_ENABLED:
                    return new ClawbackNotClawbackEnabled();
                case xdr.ClawbackResultCode.ClawbackResultCodeEnum.CLAWBACK_NO_TRUST:
                    return new ClawbackNoTrust();
                case xdr.ClawbackResultCode.ClawbackResultCodeEnum.CLAWBACK_SUCCESS:
                    return new ClawbackSuccess();
                case xdr.ClawbackResultCode.ClawbackResultCodeEnum.CLAWBACK_UNDERFUNDED:
                    return new ClawbackUnderfunded();
                default:
                    throw new SystemException("Unknown ClawbackResult type");
            }
        }
    }
}
