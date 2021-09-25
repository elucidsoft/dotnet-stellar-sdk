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

            switch(claimAtom.Type)
            {
                case xdr.ClaimAtomType.ClaimAtomTypeEnum.CLAIM_ATOM_TYPE_V0:
                    claimAtom.V0 = ClaimOfferAtomV0.FromXdr(offer.V0);
                    break;

                case xdr.ClaimAtomType.ClaimAtomTypeEnum.CLAIM_ATOM_TYPE_ORDER_BOOK:
                    claimAtom.OrderBook = ClaimOfferAtom.FromXdr(offer.OrderBook);
                    break;

                case xdr.ClaimAtomType.ClaimAtomTypeEnum.CLAIM_ATOM_TYPE_LIQUIDITY_POOL:
                    claimAtom.LiquidityPool = ClaimLiquidityAtom.FromXdr(offer.LiquidityPool);
                    break;
            }
            
            return claimAtom;
        }
    }
}