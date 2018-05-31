using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses
{
    public class TradeAggregationResponse : Response
    {
        [JsonProperty(PropertyName = "timestamp")]
        public long Timestamp { get; }

        [JsonProperty(PropertyName = "trade_count")]
        public int TradeCount { get; }

        [JsonProperty(PropertyName = "base_volume")]
        public String BaseVolume { get; }

        [JsonProperty(PropertyName = "counter_volume")]
        public String CounterVolume { get; }

        [JsonProperty(PropertyName = "avg")]
        public String Avg { get; }

        [JsonProperty(PropertyName = "high")]
        public String High { get; }

        [JsonProperty(PropertyName = "low")]
        public String Low { get; }

        [JsonProperty(PropertyName = "open")]
        public String Open { get; }

        [JsonProperty(PropertyName = "close")]
        public String Close { get; }

        public TradeAggregationResponse(long timestamp, int tradeCount, String baseVolume, String counterVolume, String avg, String high, String low, String open, String close)
        {
            this.Timestamp = timestamp;
            this.TradeCount = tradeCount;
            this.BaseVolume = baseVolume;
            this.CounterVolume = counterVolume;
            this.Avg = avg;
            this.High = high;
            this.Low = low;
            this.Open = open;
            this.Close = close;
        }
    }
}
