using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolTradeEffectResponse : EffectResponse
    {
        public override int TypeId => 92;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; set; }

        [JsonProperty(PropertyName = "sold")]
        public AssetAmount Sold { get; set; }

        [JsonProperty(PropertyName = "bought")]
        public AssetAmount Bought { get; set; }

        public LiquidityPoolTradeEffectResponse() { }

        public LiquidityPoolTradeEffectResponse(LiquidityPool liquidityPool, AssetAmount sold, AssetAmount bought)
        {
            LiquidityPool = liquidityPool;
            Sold = sold;
            Bought = bought;
        }
    }
}
