using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class OperationFeeStatsResponse : Response
    {
        [JsonProperty(PropertyName = "min_accepted_fee")]
        public long Min { get; }

        [JsonProperty(PropertyName = "mode_accepted_fee")]
        public long Mode { get; }

        [JsonProperty(PropertyName = "last_ledger_base_fee")]
        public long LastLedgerBaseFee { get; }

        [JsonProperty(PropertyName = "last_ledger")]
        public long LastLedger { get; }

        public OperationFeeStatsResponse(long min, long mode, long lastLedgerBaseFee, long lastLedger)
        {
            Min = min;
            Mode = mode;
            LastLedgerBaseFee = lastLedgerBaseFee;
            LastLedger = lastLedger;
        }
    }
}
