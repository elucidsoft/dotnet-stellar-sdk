namespace stellar_dotnet_sdk
{
    public class ClaimPredicateNot : ClaimPredicate
    {
        public ClaimPredicate Predicate { get; }

        public ClaimPredicateNot(ClaimPredicate predicate)
        {
            Predicate = predicate;
        }
        
        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_NOT
                },
                NotPredicate = Predicate.ToXdr(),
            };
        }
    }
}