namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// This result is used when offers are taken during an operation.
    /// </summary>
    public class ClaimOfferAtomV0
    {
        public KeyPair Seller { get; set; }
        public long OfferID { get; set; }
        public Asset AssetSold { get; set; }
        public string AmountSold { get; set; }
        public Asset AssetBought { get; set; }
        public string AmountBought { get; set; }

        public static ClaimOfferAtomV0 FromXdr(xdr.ClaimOfferAtomV0 claimOfferAtomV0Xdr)
        {
            var claimOfferAtomV0 = new ClaimOfferAtomV0();

            claimOfferAtomV0.Seller = KeyPair.FromPublicKey(claimOfferAtomV0Xdr.SellerEd25519.InnerValue);
            claimOfferAtomV0.OfferID = claimOfferAtomV0Xdr.OfferID.InnerValue;
            claimOfferAtomV0.AssetSold = Asset.FromXdr(claimOfferAtomV0Xdr.AssetSold);
            claimOfferAtomV0.AmountSold = Amount.FromXdr(claimOfferAtomV0Xdr.AmountSold.InnerValue);
            claimOfferAtomV0.AssetBought = Asset.FromXdr(claimOfferAtomV0Xdr.AssetBought);
            claimOfferAtomV0.AmountBought = Amount.FromXdr(claimOfferAtomV0Xdr.AmountBought.InnerValue);

            return claimOfferAtomV0;
        }
    }
}