using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolDepositedEffectResponse : EffectResponse
    {
        public override int TypeId => 81;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPoolEffectResponse LiquidityPool { get; set; }

        [JsonProperty(PropertyName = "reserves_deposited")]
        public AssetAmount[] ReservesDeposited { get; set; }

        [JsonProperty(PropertyName = "shares_received")]
        public string SharesReceived { get; private set; }

        public LiquidityPoolDepositedEffectResponse()
        {

        }

        public LiquidityPoolDepositedEffectResponse(LiquidityPoolEffectResponse liquidityPool, AssetAmount[] reservesDeposited, string sharesReceived)
        {
            LiquidityPool = liquidityPool;
            ReservesDeposited = reservesDeposited;
            SharesReceived = sharesReceived;
        }
    }
}
