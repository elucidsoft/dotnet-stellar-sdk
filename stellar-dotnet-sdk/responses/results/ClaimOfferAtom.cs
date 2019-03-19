namespace stellar_dotnet_sdk.responses.results
{
    public class ClaimOfferAtom
    {
        public KeyPair Seller { get; set; }

        public long OfferId { get; set; }

        public Asset AssetSold { get; set; }

        public string AmountSold { get; set; }

        public Asset AssetBought { get; set; }

        public string AmountBought { get; set; }

        public static ClaimOfferAtom FromXdr(xdr.ClaimOfferAtom offer)
        {
            return new ClaimOfferAtom
            {
                Seller = KeyPair.FromXdrPublicKey(offer.SellerID.InnerValue),
                OfferId = offer.OfferID.InnerValue,
                AssetSold = Asset.FromXdr(offer.AssetSold),
                AmountSold = Amount.FromXdr(offer.AmountSold.InnerValue),
                AssetBought = Asset.FromXdr(offer.AssetBought),
                AmountBought = Amount.FromXdr(offer.AmountBought.InnerValue)
            };
        }
    }
}