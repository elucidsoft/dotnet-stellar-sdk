namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// This result is used when offers are taken during an operation.
    /// </summary>
    public class ClaimAtom
    {
        public xdr.ClaimAtomType.ClaimAtomTypeEnum Type { get; set; }
        public ClaimOfferAtomV0 V0 { get; set; }
        public ClaimOfferAtom OrderBook { get; set; }
        public ClaimLiquidityAtom LiquidityPool { get; set; }

        public static ClaimAtom FromXdr(xdr.ClaimAtom offer)
        {
            var claimAtom = new ClaimAtom();
            claimAtom.Type = offer.Discriminant.InnerValue;
            claimAtom.OrderBook = ClaimOfferAtom.FromXdr(offer.OrderBook);
            claimAtom.V0 = ClaimOfferAtomV0.FromXdr(offer.V0);
            claimAtom.LiquidityPool = ClaimLiquidityAtom.FromXdr(offer.LiquidityPool);
            return claimAtom;
        }
    }
}