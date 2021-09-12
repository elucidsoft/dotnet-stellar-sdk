using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 84;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPoolEffectResponse LiquidityPool { get; set; }

        public LiquidityPoolCreatedEffectResponse()
        {

        }

        public LiquidityPoolCreatedEffectResponse(LiquidityPoolEffectResponse liquidityPool)
        {
            LiquidityPool = liquidityPool;
        }
    }
}
