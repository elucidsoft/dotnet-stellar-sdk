using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolTradeEffectResponse : EffectResponse
    {
        public override int TypeId => 81;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPoolEffectResponse LiquidityPool { get; set; }

        [JsonProperty(PropertyName = "sold")]
        public AssetAmount Sold { get; set; }

        [JsonProperty(PropertyName = "bought")]
        public AssetAmount Bought { get; private set; }

        public LiquidityPoolTradeEffectResponse()
        {

        }

        public LiquidityPoolTradeEffectResponse(LiquidityPoolEffectResponse liquidityPool, AssetAmount[] reservesDeposited, string sharesReceived)
        {
            LiquidityPool = liquidityPool;
            ReservesDeposited = reservesDeposited;
            SharesReceived = sharesReceived;
        }
    }
}
