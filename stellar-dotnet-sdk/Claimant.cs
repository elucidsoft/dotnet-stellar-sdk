namespace stellar_dotnet_sdk
{
    public class Claimant
    {
        public KeyPair Destination { get; set; }
        public ClaimPredicate Predicate { get; set; }

        public xdr.Claimant ToXdr()
        {
            return new xdr.Claimant
            {
                Discriminant = new xdr.ClaimantType {InnerValue = xdr.ClaimantType.ClaimantTypeEnum.CLAIMANT_TYPE_V0},
                V0 = new xdr.Claimant.ClaimantV0
                {
                    Destination = new xdr.AccountID(Destination.XdrPublicKey),
                    Predicate = Predicate.ToXdr(),
                }
            };
        }
        
        public static Claimant FromXdr(xdr.Claimant xdr)
        {
            var destination = KeyPair.FromXdrPublicKey(xdr.V0.Destination.InnerValue);
            var predicate = ClaimPredicate.FromXdr(xdr.V0.Predicate);
            return new Claimant
            {
                Destination = destination,
                Predicate = predicate
            };
        }
    }
}