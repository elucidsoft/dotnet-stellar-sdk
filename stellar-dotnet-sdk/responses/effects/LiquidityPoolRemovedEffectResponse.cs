using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolRemovedEffectResponse : EffectResponse
    {
        public override int TypeId => 85;

        [JsonProperty(PropertyName = "liquidity_pool_id")]
        public string LiquidityPoolID { get; set; }

        public LiquidityPoolRemovedEffectResponse()
        {

        }

        public LiquidityPoolRemovedEffectResponse(string liquidityPoolID)
        {
            LiquidityPoolID = liquidityPoolID;
        }
    }
}
