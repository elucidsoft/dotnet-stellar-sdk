using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class TimeBounds
    {
        ///<summary>
        ///Timebounds constructor.
        ///</summary>
        ///<param name="minTime"> 64bit Unix timestamp</param>
        public TimeBounds(long minTime, long maxTime)
        {
            if (minTime >= maxTime)
                throw new ArgumentException("minTime must be >= maxTime");

            MinTime = minTime;
            MaxTime = maxTime;
        }

        public long MinTime { get; }

        public long MaxTime { get; }

        public static TimeBounds FromXdr(xdr.TimeBounds timeBounds)
        {
            if (timeBounds == null)
            {
                return null;
            }

            return new TimeBounds(
                timeBounds.MinTime.InnerValue,
                timeBounds.MaxTime.InnerValue
            );
        }

        public xdr.TimeBounds ToXdr()
        {
            var timeBounds = new xdr.TimeBounds();
            var minTime = new Uint64();
            var maxTime = new Uint64();
            minTime.InnerValue = MinTime;
            maxTime.InnerValue = MaxTime;
            timeBounds.MinTime = minTime;
            timeBounds.MaxTime = maxTime;
            return timeBounds;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            TimeBounds that = (TimeBounds) o;

            if (MinTime != that.MinTime) return false;
            return MaxTime == that.MaxTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Start
                .Hash(MinTime)
                .Hash(MaxTime);
        }
    }
}