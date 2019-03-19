namespace stellar_dotnet_sdk.responses.results
{
    public class InflationSuccess : InflationResult
    {
        public override bool IsSuccess => true;

        public InflationPayout[] Payouts { get; set; }

        public class InflationPayout
        {
            public KeyPair Destination { get; set; }
            public string Amount { get; set; }

            public static InflationPayout FromXdr(xdr.InflationPayout payout)
            {
                return new InflationPayout
                {
                    Amount = stellar_dotnet_sdk.Amount.FromXdr(payout.Amount.InnerValue),
                    Destination = KeyPair.FromXdrPublicKey(payout.Destination.InnerValue)
                };
            }
        }
    }
}