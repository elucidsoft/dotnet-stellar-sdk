using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolTradeEffectResponse : EffectResponse
    {
        public override int TypeId => 92;

        [JsonProperty(PropertyName = "liquidity_pool_id")]
        public LiquidityPoolID LiquidityPoolID { get; }

        [JsonProperty(PropertyName = "sold")]
        public AssetAmount Sold { get; }

        [JsonProperty(PropertyName = "bought")]
        public AssetAmount Bought { get; }

        public LiquidityPoolTradeEffectResponse() { }

        public LiquidityPoolTradeEffectResponse(LiquidityPoolID liquidityPoolID, AssetAmount sold, AssetAmount bought)
        {
            LiquidityPoolID = liquidityPoolID;
            Sold = sold;
            Bought = bought;
        }
    }
}
