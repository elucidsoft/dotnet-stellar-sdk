using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolEffectResponse : Response
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "fee_bp")]
        public int FeeBP { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "total_trustlines")]
        public string TotalTrustlines { get; set; }

        [JsonProperty(PropertyName = "total_shares")]
        public string TotalShares { get; set; }

        [JsonProperty(PropertyName = "reserves")]
        public AssetAmount[] Reserves { get; set; }
    }
}
