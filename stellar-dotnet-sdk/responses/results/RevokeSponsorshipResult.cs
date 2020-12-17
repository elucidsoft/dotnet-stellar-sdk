using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class RevokeSponsorshipResult : OperationResult
    {
        public static RevokeSponsorshipResult FromXdr(xdr.RevokeSponsorshipResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_DOES_NOT_EXIST:
                    return new RevokeSponsorshipDoesNotExist();
                case xdr.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_LOW_RESERVE:
                    return new RevokeSponsorshipLowReserve();
                case xdr.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_NOT_SPONSOR:
                    return new RevokeSponsorshipNotSponsor();
                case xdr.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_ONLY_TRANSFERABLE:
                    return new RevokeSponsorshipOnlyTransferable();
                case xdr.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_SUCCESS:
                    return new RevokeSponsorshipSuccess();
                default:
                    throw new SystemException("Unknown RevokeSponsorship type");
            }
        }
    }
}
