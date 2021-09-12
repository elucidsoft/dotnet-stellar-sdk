using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolClaimableAssetAmount
    {
        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        [JsonProperty(PropertyName ="claimable_balance_id")]
        public string ClaimableBalanceID { get; set; }

        public LiquidityPoolClaimableAssetAmount(string asset, string amount, string claimableBalanceID)
        {
            Asset = asset;
            Amount = amount;
            ClaimableBalanceID = claimableBalanceID;
        }
    }
}
