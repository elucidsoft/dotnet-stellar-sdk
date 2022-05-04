using System;
using xdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class ClaimPredicateBeforeAbsoluteTime : ClaimPredicate
    {
        public DateTimeOffset DateTime { get => DateTimeOffset.FromUnixTimeSeconds((long)TimePoint.InnerValue.InnerValue); }
        public xdr.TimePoint TimePoint { get; } = new xdr.TimePoint();

        public ClaimPredicateBeforeAbsoluteTime(DateTimeOffset dateTime)
        {
            TimePoint.InnerValue = new xdr.Uint64((ulong)dateTime.ToUnixTimeSeconds());
        }

        public ClaimPredicateBeforeAbsoluteTime(xdr.TimePoint timePoint)
        {
            TimePoint = timePoint;
        }

        public override xdr.ClaimPredicate ToXdr()
        {
            return new xdr.ClaimPredicate
            {
                Discriminant = new xdr.ClaimPredicateType
                {
                    InnerValue = xdr.ClaimPredicateType.ClaimPredicateTypeEnum.CLAIM_PREDICATE_BEFORE_ABSOLUTE_TIME
                },
                AbsBefore = new xdr.Int64((long)TimePoint.InnerValue.InnerValue),
            };
        }
    }
}