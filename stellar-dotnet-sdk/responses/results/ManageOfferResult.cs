using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ManageOfferResult : OperationResult
    {
        public static ManageOfferResult FromXdr(xdr.ManageOfferResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_SUCCESS:
                    return ManageOfferSuccess.FromXdr(result.Success);
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_MALFORMED:
                    return new ManageOfferMalformed();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_UNDERFUNDED:
                    return new ManageOfferUnderfunded();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_SELL_NO_TRUST:
                    return new ManageOfferSellNoTrust();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_BUY_NO_TRUST:
                    return new ManageOfferBuyNoTrust();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_SELL_NOT_AUTHORIZED:
                    return new ManageOfferSellNotAuthorized();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_BUY_NOT_AUTHORIZED:
                    return new ManageOfferBuyNotAuthorized();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_LINE_FULL:
                    return new ManageOfferLineFull();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_CROSS_SELF:
                    return new ManageOfferCrossSelf();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_SELL_NO_ISSUER:
                    return new ManageOfferSellNoIssuer();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_BUY_NO_ISSUER:
                    return new ManageOfferBuyNoIssuer();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_NOT_FOUND:
                    return new ManageOfferNotFound();
                case xdr.ManageOfferResultCode.ManageOfferResultCodeEnum.MANAGE_OFFER_LOW_RESERVE:
                    return new ManageOfferLowReserve();
                default:
                    throw new SystemException("Unknown ManageOffer type");
            }
        }
    }
}