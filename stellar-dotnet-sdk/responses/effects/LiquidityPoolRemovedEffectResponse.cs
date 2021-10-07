using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolRemovedEffectResponse : EffectResponse
    {
        public override int TypeId => 94;

        [JsonProperty(PropertyName = "liquidity_pool_id")]
        public LiquidityPoolID LiquidityPoolID { get; private set; }

        public LiquidityPoolRemovedEffectResponse() { }

        public LiquidityPoolRemovedEffectResponse(LiquidityPoolID liquidityPoolID)
        {
            LiquidityPoolID = liquidityPoolID;
        }
    }
}
