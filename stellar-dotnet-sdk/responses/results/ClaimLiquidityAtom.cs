namespace stellar_dotnet_sdk.responses.results
{
    public class ClaimLiquidityAtom
    {
        /// <summary>
        /// The Liquidity Pool ID
        /// </summary>
        public LiquidityPoolID LiquidityPoolID { get; set; }

        /// <summary>
        /// Asset sold
        /// </summary>
        public Asset AssetSold { get; set; }

        /// <summary>
        /// Amount sold
        /// </summary>
        public string AmountSold { get; set; }

        /// <summary>
        /// Asset Bought
        /// </summary>
        public Asset AssetBought { get; set; }

        /// <summary>
        /// Amount Bought
        /// </summary>
        public string AmountBought { get; set; }

        /// <summary>
        /// Get new ClaimLiquityAtom object parsed from an XDR ClaimLiquidityAtom.
        /// </summary>
        /// <param name="claimLiquidityAtomXdr"></param>
        /// <returns></returns>
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