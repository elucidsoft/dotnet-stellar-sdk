using System;

namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// An offer is the building block of the offer book, they are automatically
    /// claimed by payments when the price set by the owner is met.
    /// For example an Offer is selling 10A where 1A is priced at 1.5B
    /// </summary>
    public class OfferEntry
    {
        public enum OfferEntryFlags
        {
            /// <summary>
            /// Issuer has authorized account to perform transactions with its credit.
            /// </summary>
            Passive = 1,
        }

        /// <summary>
        /// Offer Seller.
        /// </summary>
        public KeyPair Seller { get; set; }

        /// <summary>
        /// Unique Id of Offer.
        /// </summary>
        public long OfferId { get; set; }

        /// <summary>
        /// Selling Asset.
        /// </summary>
        public Asset Selling { get; set; }

        /// <summary>
        /// Buying Asset.
        /// </summary>
        public Asset Buying { get; set; }

        /// <summary>
        /// Amount of Selling asset.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Price for this offer, after fees.
        /// Price of Selling in terms of Buying.
        /// Price = AmountBuying / AmountSelling
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// Flags for offer.
        /// </summary>
        public OfferEntryFlags Flags { get; set; }

        public static OfferEntry FromXdr(xdr.OfferEntry entry)
        {
            return new OfferEntry
            {
                Seller = KeyPair.FromXdrPublicKey(entry.SellerID.InnerValue),
                OfferId = entry.OfferID.InnerValue,
                Selling = Asset.FromXdr(entry.Selling),
                Buying = Asset.FromXdr(entry.Buying),
                Amount = stellar_dotnet_sdk.Amount.FromXdr(entry.Amount.InnerValue),
                Price = Price.FromXdr(entry.Price),
                Flags = FlagsFromXdr(entry.Flags.InnerValue)
            };
        }

        public static OfferEntryFlags FlagsFromXdr(int flags)
        {
            switch (flags)
            {
                case 0:
                    return 0;
                case 1: // PASSIVE_FLAG
                    return OfferEntryFlags.Passive;
                default:
                    throw new SystemException("Unknown OfferEntryFlags type");
            }
        }
    }
}