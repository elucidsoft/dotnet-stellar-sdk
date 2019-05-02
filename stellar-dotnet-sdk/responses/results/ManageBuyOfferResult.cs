using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ManageBuyOfferResult : OperationResult
    {
        public static ManageBuyOfferResult FromXdr(xdr.ManageBuyOfferResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_SUCCESS:
                    return ManageBuyOfferSuccess.FromXdr(result.Success);
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_MALFORMED:
                    return new ManageBuyOfferMalformed();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_UNDERFUNDED:
                    return new ManageBuyOfferUnderfunded();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_SELL_NO_TRUST:
                    return new ManageBuyOfferSellNoTrust();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_BUY_NO_TRUST:
                    return new ManageBuyOfferBuyNoTrust();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_SELL_NOT_AUTHORIZED:
                    return new ManageBuyOfferSellNotAuthorized();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_BUY_NOT_AUTHORIZED:
                    return new ManageBuyOfferBuyNotAuthorized();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_LINE_FULL:
                    return new ManageBuyOfferLineFull();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_CROSS_SELF:
                    return new ManageBuyOfferCrossSelf();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_SELL_NO_ISSUER:
                    return new ManageBuyOfferSellNoIssuer();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_BUY_NO_ISSUER:
                    return new ManageBuyOfferBuyNoIssuer();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_NOT_FOUND:
                    return new ManageBuyOfferNotFound();
                case xdr.ManageBuyOfferResultCode.ManageBuyOfferResultCodeEnum.MANAGE_BUY_OFFER_LOW_RESERVE:
                    return new ManageBuyOfferLowReserve();
                default:
                    throw new SystemException("Unknown ManageOffer type");
            }
        }
    }
}