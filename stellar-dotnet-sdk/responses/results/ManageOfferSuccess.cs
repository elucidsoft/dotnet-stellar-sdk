using System;
using System.Linq;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk.responses.results
{
    public class ManageOfferSuccess : ManageOfferResult
    {
        public override bool IsSuccess => true;

        public ClaimOfferAtom[] OffersClaimed { get; set; }

        public static ManageOfferSuccess FromXdr(xdr.ManageOfferSuccessResult result)
        {
            var offersClaimed = result.OffersClaimed.Select(ClaimOfferAtom.FromXdr).ToArray();

            switch (result.Offer.Discriminant.InnerValue)
            {
                case ManageOfferEffect.ManageOfferEffectEnum.MANAGE_OFFER_CREATED:
                    var createdOffer = OfferEntry.FromXdr(result.Offer.Offer);
                    return new ManageOfferCreated
                    {
                        OffersClaimed = offersClaimed,
                        Offer = createdOffer,
                    };
                case ManageOfferEffect.ManageOfferEffectEnum.MANAGE_OFFER_UPDATED:
                    var updatedOffer = OfferEntry.FromXdr(result.Offer.Offer);
                    return new ManageOfferUpdated
                    {
                        OffersClaimed = offersClaimed,
                        Offer = updatedOffer,
                    };
                case ManageOfferEffect.ManageOfferEffectEnum.MANAGE_OFFER_DELETED:
                    return new ManageOfferDeleted
                    {
                        OffersClaimed = offersClaimed
                    };
                default:
                    throw new SystemException("Unknown ManageOfferSuccess type");
            }
        }
    }
}