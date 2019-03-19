using System;
using System.Linq;

namespace stellar_dotnet_sdk.responses.results
{
    public class PathPaymentResult : OperationResult
    {
        public static PathPaymentResult FromXdr(xdr.PathPaymentResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_SUCCESS:
                    return new PathPaymentSuccess
                    {
                        Offers = OffersFromXdr(result.Success.Offers),
                        Last = PathPaymentSuccess.SimplePaymentResult.FromXdr(result.Success.Last)
                    };
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_MALFORMED:
                    return new PathPaymentMalformed();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_UNDERFUNDED:
                    return new PathPaymentUnderfunded();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_SRC_NO_TRUST:
                    return new PathPaymentSrcNoTrust();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_SRC_NOT_AUTHORIZED:
                    return new PathPaymentSrcNotAuthorized();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_NO_DESTINATION:
                    return new PathPaymentNoDestination();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_NO_TRUST:
                    return new PathPaymentNoTrust();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_NOT_AUTHORIZED:
                    return new PathPaymentNotAuthorized();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_LINE_FULL:
                    return new PathPaymentLineFull();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_NO_ISSUER:
                    return new PathPaymentNoIssuer
                    {
                        NoIssuer = Asset.FromXdr(result.NoIssuer)
                    };
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_TOO_FEW_OFFERS:
                    return new PathPaymentTooFewOffers();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_OFFER_CROSS_SELF:
                    return new PathPaymentOfferCrossSelf();
                case xdr.PathPaymentResultCode.PathPaymentResultCodeEnum.PATH_PAYMENT_OVER_SENDMAX:
                    return new PathPaymentOverSendmax();
                default:
                    throw new SystemException("Unknown PathPayment type");
            }
        }

        private static ClaimOfferAtom[] OffersFromXdr(xdr.ClaimOfferAtom[] offers)
        {
            return offers.Select(ClaimOfferAtom.FromXdr).ToArray();
        }
    }
}