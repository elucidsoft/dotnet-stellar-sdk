namespace stellar_dotnet_sdk
{
    public class ClaimPredicateOr : ClaimPredicate
    {
        public ClaimPredicate LeftPredicate { get; }
        public ClaimPredicate RightPredicate { get; }

        public ClaimPredicateOr(ClaimPredicate leftPredicate, ClaimPredicate rightPredicate)
        {
            LeftPredicate = leftPredicate;
            RightPredicate = rightPredicate;
        }

        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_OR,
                },
                OrPredicates = new[] { LeftPredicate.ToXdr(), RightPredicate.ToXdr() }
            };
        }
    }
}