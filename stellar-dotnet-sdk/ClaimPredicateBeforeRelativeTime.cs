using System;
using xdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class ClaimPredicateBeforeRelativeTime : ClaimPredicate
    {
        public xdr.Duration Duration { get; }

        public ClaimPredicateBeforeRelativeTime(xdr.Duration duration)
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
                RelBefore = new xdr.Int64((long)Duration.InnerValue.InnerValue)
            };
        }
    }
}