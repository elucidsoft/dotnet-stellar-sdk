namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// This result is used when offers are taken during an operation.
    /// </summary>
    public class ClaimLiquidityAtom
    {
        public LiquidityPoolID LiquidityPoolID { get; set; }
        public Asset AssetSold { get; set; }
        public string AmountSold { get; set; }
        public Asset AssetBought { get; set; }
        public string AmountBought { get; set; }

        public static ClaimLiquidityAtom FromXdr(xdr.ClaimLiquidityAtom claimLiquidityAtomXdr)
        {
            var claimLiquidityAtom = new ClaimLiquidityAtom();
            claimLiquidityAtom.LiquidityPoolID = LiquidityPoolID.FromXdr(claimLiquidityAtomXdr.LiquidityPoolID);
            claimLiquidityAtom.AssetSold = Asset.FromXdr(claimLiquidityAtomXdr.AssetSold);
            claimLiquidityAtom.AmountSold = Amount.FromXdr(claimLiquidityAtomXdr.AmountSold.InnerValue);
            claimLiquidityAtom.AssetBought = Asset.FromXdr(claimLiquidityAtomXdr.AssetBought);
            claimLiquidityAtom.AmountBought = Amount.FromXdr(claimLiquidityAtomXdr.AmountBought.InnerValue);
            return claimLiquidityAtom;
        }
    }
}