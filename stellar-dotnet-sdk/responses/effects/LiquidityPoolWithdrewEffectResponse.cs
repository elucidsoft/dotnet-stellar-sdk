using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolWithdrewEffectResponse : EffectResponse
    {
        public override int TypeId => 91;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; private set; }

        [JsonProperty(PropertyName = "reserves_received")]
        public AssetAmount[] ReservesReceived { get; private set; }

        [JsonProperty(PropertyName = "shares_redeemed")]
        public string SharesRedeemed { get; private set; }

        public LiquidityPoolWithdrewEffectResponse() { }

        public LiquidityPoolWithdrewEffectResponse(LiquidityPool liquidityPool, AssetAmount[] reservesReceived, string sharesRedeemed)
        {
            LiquidityPool = liquidityPool;
            ReservesReceived = reservesReceived;
            SharesRedeemed = sharesRedeemed;
        }
    }
}
