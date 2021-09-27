using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolDepositedEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; }

        [JsonProperty(PropertyName = "reserves_deposited")]
        public AssetAmount[] ReservesDeposited { get; }

        [JsonProperty(PropertyName = "shares_received")]
        public string SharesReceived { get; }

        public LiquidityPoolDepositedEffectResponse() { }

        public LiquidityPoolDepositedEffectResponse(LiquidityPool liquidityPool, AssetAmount[] reservesDeposited, string sharesReceived)
        {
            LiquidityPool = liquidityPool;
            ReservesDeposited = reservesDeposited;
            SharesReceived = sharesReceived;
        }
    }
}
