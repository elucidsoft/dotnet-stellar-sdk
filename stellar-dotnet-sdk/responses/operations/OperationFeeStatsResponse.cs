using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.operations
{
    public class OperationFeeStatsResponse : OperationResponse
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
