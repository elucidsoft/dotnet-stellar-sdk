using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ManageDataResult : OperationResult
    {
        public static ManageDataResult FromXdr(xdr.ManageDataResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ManageDataResultCode.ManageDataResultCodeEnum.MANAGE_DATA_SUCCESS:
                    return new ManageDataSuccess();
                case xdr.ManageDataResultCode.ManageDataResultCodeEnum.MANAGE_DATA_NOT_SUPPORTED_YET:
                    return new ManageDataNotSupportedYet();
                case xdr.ManageDataResultCode.ManageDataResultCodeEnum.MANAGE_DATA_NAME_NOT_FOUND:
                    return new ManageDataNameNotFound();
                case xdr.ManageDataResultCode.ManageDataResultCodeEnum.MANAGE_DATA_LOW_RESERVE:
                    return new ManageDataLowReserve();
                case xdr.ManageDataResultCode.ManageDataResultCodeEnum.MANAGE_DATA_INVALID_NAME:
                    return new ManageDataInvalidName();
                default:
                    throw new SystemException("Unknown ManageData type");
            }
        }
    }
}