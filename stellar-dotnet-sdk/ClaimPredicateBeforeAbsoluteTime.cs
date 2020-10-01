using System;

namespace stellar_dotnet_sdk
{
    public class ClaimPredicateBeforeAbsoluteTime : ClaimPredicate
    {
        public DateTimeOffset DateTime { get; }

        public ClaimPredicateBeforeAbsoluteTime(DateTimeOffset dateTime)
        {
            DateTime = dateTime;
        }

        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_BEFORE_ABSOLUTE_TIME
                },
                AbsBefore = new xdr.Int64(DateTime.ToUnixTimeSeconds()),
            };
        }
    }
}