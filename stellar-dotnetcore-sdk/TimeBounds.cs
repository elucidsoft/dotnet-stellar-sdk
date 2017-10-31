using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class TimeBounds
    {
        private readonly long _MinTime;
        private readonly long _MaxTime;

        public long MinTime { get { return _MinTime; } }
        public long MaxTime { get { return _MaxTime; } }
        /**
         * @param minTime 64bit Unix timestamp
         * @param maxTime 64bit Unix timestamp
         */
        public TimeBounds(long minTime, long maxTime)
        {
            if (minTime >= maxTime)
            {
                throw new ArgumentException("minTime must be >= maxTime");
            }

            _MinTime = minTime;
            _MaxTime = maxTime;
        }

        public xdr.TimeBounds ToXdr()
        {
            xdr.TimeBounds timeBounds = new xdr.TimeBounds();
            Uint64 minTime = new Uint64();
            Uint64 maxTime = new Uint64();
            minTime.InnerValue = _MinTime;
            maxTime.InnerValue = _MaxTime;
            timeBounds.MinTime = minTime;
            timeBounds.MaxTime = maxTime;
            return timeBounds;
        }
    }
}