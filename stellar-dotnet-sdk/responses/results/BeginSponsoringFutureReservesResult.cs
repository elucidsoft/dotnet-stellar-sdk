using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class BeginSponsoringFutureReservesResult : OperationResult
    {
        public static BeginSponsoringFutureReservesResult FromXdr(xdr.BeginSponsoringFutureReservesResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.BeginSponsoringFutureReservesResultCode.BeginSponsoringFutureReservesResultCodeEnum.BEGIN_SPONSORING_FUTURE_RESERVES_ALREADY_SPONSORED:
                    return new BeginSponsoringFutureReservesAlreadySponsored();
                case xdr.BeginSponsoringFutureReservesResultCode.BeginSponsoringFutureReservesResultCodeEnum.BEGIN_SPONSORING_FUTURE_RESERVES_MALFORMED:
                    return new BeginSponsoringFutureReservesMalformed();
                case xdr.BeginSponsoringFutureReservesResultCode.BeginSponsoringFutureReservesResultCodeEnum.BEGIN_SPONSORING_FUTURE_RESERVES_RECURSIVE:
                    return new BeginSponsoringFutureReservesRecursive();
                case xdr.BeginSponsoringFutureReservesResultCode.BeginSponsoringFutureReservesResultCodeEnum.BEGIN_SPONSORING_FUTURE_RESERVES_SUCCESS:
                    return new BeginSponsoringFutureReservesSuccess();
                default:
                    throw new SystemException("Unknown BeginSponsoringFutureReserves type");
            }
        }
    }
}
