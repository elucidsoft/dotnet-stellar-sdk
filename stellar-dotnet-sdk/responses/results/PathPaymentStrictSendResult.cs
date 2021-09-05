using System;
using System.Linq;

namespace stellar_dotnet_sdk.responses.results
{
    public class PathPaymentStrictSendResult : OperationResult
    {
        public static PathPaymentStrictSendResult FromXdr(xdr.PathPaymentStrictSendResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_SUCCESS:
                    return new PathPaymentStrictSendSuccess
                    {
                        Offers = OffersFromXdr(result.Success.Offers),
                        Last = PathPaymentStrictSendSuccess.SimplePaymentResult.FromXdr(result.Success.Last)
                    };
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_MALFORMED:
                    return new PathPaymentStrictSendMalformed();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_UNDERFUNDED:
                    return new PathPaymentStrictSendUnderfunded();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_SRC_NO_TRUST:
                    return new PathPaymentStrictSendSrcNoTrust();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_SRC_NOT_AUTHORIZED:
                    return new PathPaymentStrictSendSrcNotAuthorized();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_NO_DESTINATION:
                    return new PathPaymentStrictSendNoDestination();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_NO_TRUST:
                    return new PathPaymentStrictSendNoTrust();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_NOT_AUTHORIZED:
                    return new PathPaymentStrictSendNotAuthorized();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_LINE_FULL:
                    return new PathPaymentStrictSendLineFull();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_NO_ISSUER:
                    return new PathPaymentStrictSendNoIssuer
                    {
                        NoIssuer = Asset.FromXdr(result.NoIssuer)
                    };
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_TOO_FEW_OFFERS:
                    return new PathPaymentStrictSendTooFewOffers();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_OFFER_CROSS_SELF:
                    return new PathPaymentStrictSendOfferCrossSelf();
                case xdr.PathPaymentStrictSendResultCode.PathPaymentStrictSendResultCodeEnum.PATH_PAYMENT_STRICT_SEND_UNDER_DESTMIN:
                    return new PathPaymentStrictSendUnderDestMin();
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