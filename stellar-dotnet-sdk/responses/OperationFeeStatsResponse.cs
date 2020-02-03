using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class OperationFeeStatsResponse : Response
    {
        [JsonProperty(PropertyName = "ledger_capacity_usage")]
        public decimal LedgerCapacityUsage { get; }

        [JsonProperty(PropertyName = "last_ledger_base_fee")]
        public long LastLedgerBaseFee { get; }

        [JsonProperty(PropertyName = "last_ledger")]
        public uint LastLedger { get; }

        [JsonProperty(PropertyName = "fee_charged")]
        public FeeStatsResponseData FeeCharged { get; private set; }

        [JsonProperty(PropertyName = "max_fee")]
        public FeeStatsResponseData MaxFee { get; private set; }
       
        public OperationFeeStatsResponse(decimal ledgerCapacityUsage, long lastLedgerBaseFee, uint lastLedger, FeeStatsResponseData feeCharged, FeeStatsResponseData maxFee)
        {
            LedgerCapacityUsage = ledgerCapacityUsage;
            LastLedgerBaseFee = lastLedgerBaseFee;
            LastLedger = lastLedger;
            FeeCharged = feeCharged;
            MaxFee = maxFee;
        }

        public class FeeStatsResponseData
        {
            [JsonProperty(PropertyName = "max")]
            public long Max { get; }

            [JsonProperty(PropertyName = "min")]
            public long Min { get; }

            [JsonProperty(PropertyName = "mode")]
            public long Mode { get; }

            [JsonProperty(PropertyName = "p10")]
            public long P10 { get; }

            [JsonProperty(PropertyName = "p20")]
            public long P20 { get; }

            [JsonProperty(PropertyName = "p30")]
            public long P30 { get; }

            [JsonProperty(PropertyName = "p40")]
            public long P40 { get; }

            [JsonProperty(PropertyName = "p50")]
            public long P50 { get; }

            [JsonProperty(PropertyName = "p60")]
            public long P60 { get; }

            [JsonProperty(PropertyName = "p70")]
            public long P70 { get; }

            [JsonProperty(PropertyName = "p80")]
            public long P80 { get; }

            [JsonProperty(PropertyName = "p90")]
            public long P90 { get; }

            [JsonProperty(PropertyName = "p95")]
            public long P95 { get; }

            [JsonProperty(PropertyName = "p99")]
            public long P99 { get; }

            public FeeStatsResponseData(long max, long min, long mode, long p10, long p20, long p30, long p40, long p50, long p60,
                long p70, long p80, long p90, long p95, long p99)
            {
                Max = max;
                Min = min;
                Mode = mode;
                P10 = p10;
                P20 = p20;
                P30 = p30;
                P40 = p40;
                P50 = p50;
                P60 = p60;
                P70 = p70;
                P80 = p80;
                P90 = p90;
                P95 = p95;
                P99 = p99;
            }
        }
    }
}
