namespace stellar_dotnet_sdk
{
    public class ClaimPredicateAnd : ClaimPredicate
    {
        public ClaimPredicate LeftPredicate { get; }
        public ClaimPredicate RightPredicate { get; }

        public ClaimPredicateAnd(ClaimPredicate leftPredicate, ClaimPredicate rightPredicate)
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
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_AND,
                },
                AndPredicates = new[] {LeftPredicate.ToXdr(), RightPredicate.ToXdr()}
            };
        }
    }
}