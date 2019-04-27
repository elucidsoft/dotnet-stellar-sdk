namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// This result is used when offers are taken during an operation.
    /// </summary>
    public class ClaimOfferAtom
    {
        /// <summary>
        /// Account that owns the offer.
        /// </summary>
        public KeyPair Seller { get; set; }

        /// <summary>
        /// Emitted to identify the offer.
        /// </summary>
        public long OfferId { get; set; }

        /// <summary>
        /// Asset taken from the owner.
        /// </summary>
        public Asset AssetSold { get; set; }

        /// <summary>
        /// Amount taken from the owner.
        /// </summary>
        public string AmountSold { get; set; }

        /// <summary>
        /// Asset sent to the owner.
        /// </summary>
        public Asset AssetBought { get; set; }

        /// <summary>
        /// Amount sent to the owner.
        /// </summary>
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