using System;

namespace stellar_dotnet_sdk.responses.results
{
    public class ManageSellOfferResult : OperationResult
    {
        public static ManageSellOfferResult FromXdr(xdr.ManageSellOfferResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_SUCCESS:
                    return ManageSellOfferSuccess.FromXdr(result.Success);
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_MALFORMED:
                    return new ManageSellOfferMalformed();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_UNDERFUNDED:
                    return new ManageSellOfferUnderfunded();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_SELL_NO_TRUST:
                    return new ManageSellOfferSellNoTrust();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_BUY_NO_TRUST:
                    return new ManageSellOfferBuyNoTrust();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_SELL_NOT_AUTHORIZED:
                    return new ManageSellOfferSellNotAuthorized();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_BUY_NOT_AUTHORIZED:
                    return new ManageSellOfferBuyNotAuthorized();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_LINE_FULL:
                    return new ManageSellOfferLineFull();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_CROSS_SELF:
                    return new ManageSellOfferCrossSelf();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_SELL_NO_ISSUER:
                    return new ManageSellOfferSellNoIssuer();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_BUY_NO_ISSUER:
                    return new ManageSellOfferBuyNoIssuer();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_NOT_FOUND:
                    return new ManageSellOfferNotFound();
                case xdr.ManageSellOfferResultCode.ManageSellOfferResultCodeEnum.MANAGE_SELL_OFFER_LOW_RESERVE:
                    return new ManageSellOfferLowReserve();
                default:
                    throw new SystemException("Unknown ManageOffer type");
            }
        }
    }
}