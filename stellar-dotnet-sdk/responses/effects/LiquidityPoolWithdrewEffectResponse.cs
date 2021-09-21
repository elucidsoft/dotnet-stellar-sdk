using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolWithdrewEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; }

        [JsonProperty(PropertyName = "reserves_received")]
        public AssetAmount[] ReservesReceived { get; }

        [JsonProperty(PropertyName = "shares_redeemed")]
        public string SharesRedeemed { get; }

        public LiquidityPoolWithdrewEffectResponse(LiquidityPool liquidityPool, AssetAmount[] reservesReceived, string sharesRedeemed)
        {
            LiquidityPool = liquidityPool;
            ReservesReceived = reservesReceived;
            SharesRedeemed = sharesRedeemed;
        }
    }
}
