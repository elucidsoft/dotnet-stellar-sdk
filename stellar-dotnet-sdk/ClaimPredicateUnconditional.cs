namespace stellar_dotnet_sdk
{
    public class ClaimPredicateUnconditional : ClaimPredicate
    {
        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_UNCONDITIONAL
                },
            };
        }
    }
}