using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class TradeAggregationResponse : Response
    {
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; }

        [JsonProperty(PropertyName = "trade_count")]
        public string TradeCount { get; }

        [JsonProperty(PropertyName = "base_volume")]
        public String BaseVolume { get; }

        [JsonProperty(PropertyName = "counter_volume")]
        public String CounterVolume { get; }

        [JsonProperty(PropertyName = "avg")] public String Avg { get; }

        [JsonProperty(PropertyName = "high")] public String High { get; }

        [JsonProperty(PropertyName = "low")] public String Low { get; }

        [JsonProperty(PropertyName = "open")] public String Open { get; }

        [JsonProperty(PropertyName = "close")] public String Close { get; }

        public TradeAggregationResponse(string timestamp, string tradeCount, String baseVolume, String counterVolume, String avg, String high, String low, String open, String close)
        {
            Timestamp = timestamp;
            TradeCount = tradeCount;
            BaseVolume = baseVolume;
            CounterVolume = counterVolume;
            Avg = avg;
            High = high;
            Low = low;
            Open = open;
            Close = close;
        }
    }
}