using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class OperationFeeStatsResponse : Response
    {
        [JsonProperty(PropertyName = "min_accepted_fee")]
        public long Min { get; }

        [JsonProperty(PropertyName = "mode_accepted_fee")]
        public long Mode { get; }

        [JsonProperty(PropertyName = "p10_accepted_fee")]
        public long P10 { get; }

        [JsonProperty(PropertyName = "p20_accepted_fee")]
        public long P20 { get; }

        [JsonProperty(PropertyName = "p30_accepted_fee")]
        public long P30 { get; }

        [JsonProperty(PropertyName = "p40_accepted_fee")]
        public long P40 { get; }

        [JsonProperty(PropertyName = "p50_accepted_fee")]
        public long P50 { get; }

        [JsonProperty(PropertyName = "p60_accepted_fee")]
        public long P60 { get; }

        [JsonProperty(PropertyName = "p70_accepted_fee")]
        public long P70 { get; }

        [JsonProperty(PropertyName = "p80_accepted_fee")]
        public long P80 { get; }

        [JsonProperty(PropertyName = "p90_accepted_fee")]
        public long P90 { get; }

        [JsonProperty(PropertyName = "p95_accepted_fee")]
        public long P95  { get; }

        [JsonProperty(PropertyName = "p99_accepted_fee")]
        public long P99 { get; }

        [JsonProperty(PropertyName = "ledger_capacity_usage")]
        public float LedgerCapacityUsage { get; }

        [JsonProperty(PropertyName = "last_ledger_base_fee")]
        public long LastLedgerBaseFee { get; }

        [JsonProperty(PropertyName = "last_ledger")]
        public long LastLedger { get; }

        public OperationFeeStatsResponse(long min, long mode, long p10, long p20, long p30, long p40, long p50, long p60, long p70, long p80, long p90, long p95, long p99, float ledgerCapacityUsage, long lastLedgerBaseFee, long lastLedger)
        {
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
            LedgerCapacityUsage = ledgerCapacityUsage;
            LastLedgerBaseFee = lastLedgerBaseFee;
            LastLedger = lastLedger;
        }
    }
}
