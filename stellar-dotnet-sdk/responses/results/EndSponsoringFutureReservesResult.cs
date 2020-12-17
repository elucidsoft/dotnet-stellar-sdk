using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class EndSponsoringFutureReservesResult : OperationResult
    {
        public static EndSponsoringFutureReservesResult FromXdr(xdr.EndSponsoringFutureReservesResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.EndSponsoringFutureReservesResultCode.EndSponsoringFutureReservesResultCodeEnum.END_SPONSORING_FUTURE_RESERVES_NOT_SPONSORED:
                    return new EndSponsoringFutureReservesNotSponsored();
                case xdr.EndSponsoringFutureReservesResultCode.EndSponsoringFutureReservesResultCodeEnum.END_SPONSORING_FUTURE_RESERVES_SUCCESS:
                    return new EndSponsoringFutureReservesSuccess();
                default:
                    throw new SystemException("Unknown EndSponsoringFutureReserves type");
            }
        }
    }
}
