using Newtonsoft.Json;
using stellar_dotnet_sdk.converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolDepositedEffectResponse : EffectResponse
    {
        public override int TypeId => 90;

        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; set; }

        [JsonProperty(PropertyName = "reserves_deposited")]
        public AssetAmount[] ReservesDeposited { get; set; }

        [JsonProperty(PropertyName = "shares_received")]
        public string SharesReceived { get; set; }

        public LiquidityPoolDepositedEffectResponse() { }

        public LiquidityPoolDepositedEffectResponse(LiquidityPool liquidityPool, AssetAmount[] reservesDeposited, string sharesReceived)
        {
            LiquidityPool = liquidityPool;
            ReservesDeposited = reservesDeposited;
            SharesReceived = sharesReceived;
        }
    }
}
