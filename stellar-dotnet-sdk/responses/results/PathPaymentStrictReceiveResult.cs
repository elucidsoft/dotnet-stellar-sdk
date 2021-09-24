using System;
using System.Linq;

namespace stellar_dotnet_sdk.responses.results
{
    public class PathPaymentStrictReceiveResult : OperationResult
    {
        public static PathPaymentStrictReceiveResult FromXdr(xdr.PathPaymentStrictReceiveResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_SUCCESS:
                    return new PathPaymentStrictReceiveSuccess
                    {
                        Offers = OffersFromXdr(result.Success.Offers),
                        Last = PathPaymentStrictReceiveSuccess.SimplePaymentResult.FromXdr(result.Success.Last)
                    };
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_MALFORMED:
                    return new PathPaymentStrictReceiveMalformed();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_UNDERFUNDED:
                    return new PathPaymentStrictReceiveUnderfunded();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_SRC_NO_TRUST:
                    return new PathPaymentStrictReceiveSrcNoTrust();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_SRC_NOT_AUTHORIZED:
                    return new PathPaymentStrictReceiveSrcNotAuthorized();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_NO_DESTINATION:
                    return new PathPaymentStrictReceiveNoDestination();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_NO_TRUST:
                    return new PathPaymentStrictReceiveNoTrust();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_NOT_AUTHORIZED:
                    return new PathPaymentStrictReceiveNotAuthorized();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_LINE_FULL:
                    return new PathPaymentStrictReceiveLineFull();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_NO_ISSUER:
                    return new PathPaymentStrictReceiveNoIssuer
                    {
                        NoIssuer = Asset.FromXdr(result.NoIssuer)
                    };
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_TOO_FEW_OFFERS:
                    return new PathPaymentStrictReceiveTooFewOffers();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_OFFER_CROSS_SELF:
                    return new PathPaymentStrictReceiveOfferCrossSelf();
                case xdr.PathPaymentStrictReceiveResultCode.PathPaymentStrictReceiveResultCodeEnum.PATH_PAYMENT_STRICT_RECEIVE_OVER_SENDMAX:
                    return new PathPaymentStrictReceiveOverSendmax();
                default:
                    throw new SystemException("Unknown PathPayment type");
            }
        }

        private static ClaimAtom[] OffersFromXdr(xdr.ClaimAtom[] offers)
        {
            return offers.Select(ClaimAtom.FromXdr).ToArray();
        }
    }
}