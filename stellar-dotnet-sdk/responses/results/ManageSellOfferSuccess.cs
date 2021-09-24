using System;
using System.Linq;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// Operation successful.
    /// </summary>
    public class ManageSellOfferSuccess : ManageSellOfferResult
    {
        public override bool IsSuccess => true;

        /// <summary>
        /// Offers that got claimed while creating this offer.
        /// </summary>
        public ClaimAtom[] OffersClaimed { get; set; }

        public static ManageSellOfferSuccess FromXdr(xdr.ManageOfferSuccessResult result)
        {
            var offersClaimed = result.OffersClaimed.Select(ClaimAtom.FromXdr).ToArray();

            switch (result.Offer.Discriminant.InnerValue)
            {
                case ManageOfferEffect.ManageOfferEffectEnum.MANAGE_OFFER_CREATED:
                    var createdOffer = OfferEntry.FromXdr(result.Offer.Offer);
                    return new ManageSellOfferCreated
                    {
                        OffersClaimed = offersClaimed,
                        Offer = createdOffer,
                    };
                case ManageOfferEffect.ManageOfferEffectEnum.MANAGE_OFFER_UPDATED:
                    var updatedOffer = OfferEntry.FromXdr(result.Offer.Offer);
                    return new ManageSellOfferUpdated
                    {
                        OffersClaimed = offersClaimed,
                        Offer = updatedOffer,
                    };
                case ManageOfferEffect.ManageOfferEffectEnum.MANAGE_OFFER_DELETED:
                    return new ManageSellOfferDeleted
                    {
                        OffersClaimed = offersClaimed
                    };
                default:
                    throw new SystemException("Unknown ManageSellOfferSuccess type");
            }
        }
    }
}