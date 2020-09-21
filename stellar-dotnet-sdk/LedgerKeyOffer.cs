namespace stellar_dotnet_sdk
{
    public class LedgerKeyOffer : LedgerKey
    {
        public KeyPair Seller { get; }
        public long OfferId { get; }

        public LedgerKeyOffer(KeyPair seller, long offerId)
        {
            Seller = seller;
            OfferId = offerId;
        }
        
        public override xdr.LedgerKey ToXdr()
        {
            return new xdr.LedgerKey
            {
                Discriminant =
                    new xdr.LedgerEntryType {InnerValue = xdr.LedgerEntryType.LedgerEntryTypeEnum.OFFER},
                Offer = new xdr.LedgerKey.LedgerKeyOffer
                {
                    SellerID = new xdr.AccountID(Seller.XdrPublicKey),
                    OfferID = new xdr.Int64(OfferId),
                }
            };
        }

        public static LedgerKeyOffer FromXdr(xdr.LedgerKey.LedgerKeyOffer xdr)
        {
            var seller = KeyPair.FromXdrPublicKey(xdr.SellerID.InnerValue);
            var offerId = xdr.OfferID.InnerValue;
            return new LedgerKeyOffer(seller, offerId);
        }
    }
}