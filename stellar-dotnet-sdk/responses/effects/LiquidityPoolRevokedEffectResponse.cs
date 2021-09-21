using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolRevokedEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "liquidity_pool")]
        public LiquidityPool LiquidityPool { get; }

        [JsonProperty(PropertyName = "reserves_revoked")]
        public LiquidityPoolClaimableAssetAmount ReservesRevoked { get; }

        [JsonProperty(PropertyName = "shares_revoked")]
        public string SharesRevoked { get; }

        public LiquidityPoolRevokedEffectResponse(LiquidityPool liquidityPool, LiquidityPoolClaimableAssetAmount reservesRevoked, string sharesRevoked)
        {
            LiquidityPool = liquidityPool;
            ReservesRevoked = reservesRevoked;
            SharesRevoked = sharesRevoked;
        }
    }
}
