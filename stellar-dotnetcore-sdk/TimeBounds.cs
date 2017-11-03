using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class TimeBounds
    {
        /**
         * @param minTime 64bit Unix timestamp
         * @param maxTime 64bit Unix timestamp
         */
        public TimeBounds(long minTime, long maxTime)
        {
            if (minTime >= maxTime)
                throw new ArgumentException("minTime must be >= maxTime");

            MinTime = minTime;
            MaxTime = maxTime;
        }

        public long MinTime { get; }

        public long MaxTime { get; }

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
    }
}