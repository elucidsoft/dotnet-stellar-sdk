using Newtonsoft.Json;
using stellar_dotnet_sdk.converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPool
    {
        [JsonProperty(PropertyName = "id")]
        public LiquidityPoolID ID { get; }

        [JsonProperty(PropertyName = "fee_bp")]
        public int FeeBP { get; }

        [JsonConverter(typeof(LiquidityPoolTypeEnumJsonConverter))]
        [JsonProperty(PropertyName = "type")]
        public xdr.LiquidityPoolType.LiquidityPoolTypeEnum Type { get; }

        [JsonProperty(PropertyName = "total_trustlines")]
        public int TotalTrustlines { get; }

        [JsonProperty(PropertyName = "total_shares")]
        public string TotalShares { get; }

        [JsonProperty(PropertyName = "reserves")]
        public AssetAmount[] Reserves { get; }

        public LiquidityPool(LiquidityPoolID id, int feeBP, xdr.LiquidityPoolType.LiquidityPoolTypeEnum type, int totalTrustlines, string totalShares, AssetAmount[] reserves)
        {
            ID = id;
            FeeBP = feeBP;
            Type = type;
            TotalTrustlines = totalTrustlines;
            TotalShares = totalShares;
            Reserves = reserves;
        }
    }
}
