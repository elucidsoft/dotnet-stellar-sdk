using System;

namespace stellar_dotnet_sdk
{
    public class ClaimPredicateBeforeAbsoluteTime : ClaimPredicate
    {
        public DateTimeOffset DateTime { get => DateTimeOffset.FromUnixTimeSeconds(UnixTimeSeconds); }
        public long UnixTimeSeconds { get; }

        public ClaimPredicateBeforeAbsoluteTime(DateTimeOffset dateTime)
        {
            UnixTimeSeconds = dateTime.ToUnixTimeSeconds();
        }

        public ClaimPredicateBeforeAbsoluteTime(long unixTimeSeconds)
        {
            UnixTimeSeconds = unixTimeSeconds;
        }

        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_BEFORE_ABSOLUTE_TIME
                },
                AbsBefore = new xdr.Int64(UnixTimeSeconds),
            };
        }
    }
}