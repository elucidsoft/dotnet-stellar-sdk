using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class SetOptionsResult : OperationResult
    {
        public static SetOptionsResult FromXdr(xdr.SetOptionsResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_SUCCESS:
                    return new SetOptionsSuccess();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_LOW_RESERVE:
                    return new SetOptionsLowReserve();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_TOO_MANY_SIGNERS:
                    return new SetOptionsTooManySigners();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_BAD_FLAGS:
                    return new SetOptionsBadFlags();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_INVALID_INFLATION:
                    return new SetOptionsInvalidInflation();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_CANT_CHANGE:
                    return new SetOptionsCantChange();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_UNKNOWN_FLAG:
                    return new SetOptionsUnknownFlag();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_THRESHOLD_OUT_OF_RANGE:
                    return new SetOptionsThresholdOutOfRange();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_BAD_SIGNER:
                    return new SetOptionsBadSigner();
                case xdr.SetOptionsResultCode.SetOptionsResultCodeEnum.SET_OPTIONS_INVALID_HOME_DOMAIN:
                    return new SetOptionsInvalidHomeDomain();
                default:
                    throw new SystemException("Unknown SetOptions type");
            }
        }
    }
}