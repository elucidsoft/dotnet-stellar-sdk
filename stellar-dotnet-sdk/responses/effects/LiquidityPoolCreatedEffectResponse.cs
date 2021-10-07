using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 93;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; private set; }

        public LiquidityPoolCreatedEffectResponse() { }

        public LiquidityPoolCreatedEffectResponse(LiquidityPool liquidityPool)
        {
            LiquidityPool = liquidityPool;
        }
    }
}
