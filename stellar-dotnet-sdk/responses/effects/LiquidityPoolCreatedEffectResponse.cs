using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolCreatedEffectResponse
    {
        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; }

        public LiquidityPoolCreatedEffectResponse(LiquidityPool liquidityPool)
        {
            LiquidityPool = liquidityPool;
        }
    }
}
