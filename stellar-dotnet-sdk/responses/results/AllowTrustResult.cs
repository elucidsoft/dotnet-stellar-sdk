using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class AllowTrustResult : OperationResult
    {
        public static AllowTrustResult FromXdr(xdr.AllowTrustResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.AllowTrustResultCode.AllowTrustResultCodeEnum.ALLOW_TRUST_SUCCESS:
                    return new AllowTrustSuccess();
                case xdr.AllowTrustResultCode.AllowTrustResultCodeEnum.ALLOW_TRUST_MALFORMED:
                    return new AllowTrustMalformed();
                case xdr.AllowTrustResultCode.AllowTrustResultCodeEnum.ALLOW_TRUST_NO_TRUST_LINE:
                    return new AllowTrustNoTrustline();
                case xdr.AllowTrustResultCode.AllowTrustResultCodeEnum.ALLOW_TRUST_TRUST_NOT_REQUIRED:
                    return new AllowTrustNotRequired();
                case xdr.AllowTrustResultCode.AllowTrustResultCodeEnum.ALLOW_TRUST_CANT_REVOKE:
                    return new AllowTrustCantRevoke();
                case xdr.AllowTrustResultCode.AllowTrustResultCodeEnum.ALLOW_TRUST_SELF_NOT_ALLOWED:
                    return new AllowTrustSelfNotAllowed();
                default:
                    throw new SystemException("Unknown AllowTrust type");
            }
        }
    }
}