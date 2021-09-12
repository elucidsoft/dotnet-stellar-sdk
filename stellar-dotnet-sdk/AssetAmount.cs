using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class AssetAmount
    {
        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }

        public AssetAmount (string asset, string amount)
        {
            Asset = asset;
            Amount = amount;
        }
    }
}
