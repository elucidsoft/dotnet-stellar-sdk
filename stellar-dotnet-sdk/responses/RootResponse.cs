using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses
{
    public class RootResponse : Response
    {
        [JsonProperty(PropertyName = "horizon_version")]
        public string HorizonVersion { get; }

        [JsonProperty(PropertyName = "core_version")]
        public string StellarCoreVersion { get; }

        [JsonProperty(PropertyName = "history_latest_ledger")]
        public int HistoryLatestLedger { get; }

        [JsonProperty(PropertyName = "history_elder_ledger")]
        public int HistoryElderLedger { get; }

        [JsonProperty(PropertyName = "core_latest_ledger")]
        public int CoreLatestLedger { get; }

        [JsonProperty(PropertyName = "network_passphrase")]
        public string NetworkPassphrase { get; }

        [JsonProperty(PropertyName = "protocol_version")]
        public int ProtocolVersion { get; }

        public RootResponse(string horizonVersion, string stellarCoreVersion, int historyLatestLedger, int historyElderLedger, int coreLatestLedger, string networkPassphrase, int protocolVersion)
        {
            HorizonVersion = horizonVersion;
            StellarCoreVersion = stellarCoreVersion;
            HistoryLatestLedger = historyLatestLedger;
            HistoryElderLedger = historyElderLedger;
            CoreLatestLedger = coreLatestLedger;
            NetworkPassphrase = networkPassphrase;
            ProtocolVersion = protocolVersion;
            
        }
    }
}
