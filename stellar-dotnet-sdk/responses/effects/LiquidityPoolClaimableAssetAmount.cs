using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class LiquidityPoolClaimableAssetAmount
    {
        [JsonProperty(PropertyName = "asset")]
        public Asset Asset { get; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; }

        [JsonProperty(PropertyName = "claimable_balance_id")]
        public string ClaimableBalanceID { get; }

        public LiquidityPoolClaimableAssetAmount(Asset asset, string amount, string claimableBalanceID)
        {
            Asset = asset;
            Amount = amount;
            ClaimableBalanceID = claimableBalanceID;
        }
    }
}
