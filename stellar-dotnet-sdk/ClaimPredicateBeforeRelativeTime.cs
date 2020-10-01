using System;

namespace stellar_dotnet_sdk
{
    public class ClaimPredicateBeforeRelativeTime : ClaimPredicate
    {
        public TimeSpan Duration { get; }

        public ClaimPredicateBeforeRelativeTime(TimeSpan duration)
        {
            Duration = duration;
        }

        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_BEFORE_RELATIVE_TIME
                },
                RelBefore = new xdr.Int64(Convert.ToInt64(Duration.TotalSeconds)),
            };
        }
    }
}