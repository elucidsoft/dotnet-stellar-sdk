using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolResponseLinks
    {
        [JsonProperty(PropertyName = "effects")]
        public Link Effects { get; set; }

        [JsonProperty(PropertyName = "operations")]
        public Link Operations { get; set; }

        [JsonProperty(PropertyName = "self")]
        public Link Self { get; set; }

        [JsonProperty(PropertyName = "transactions")]
        public Link Transactions { get; set; }

        public LiquidityPoolResponseLinks(Link effects, Link operations, Link self, Link transactions)
        {
            Effects = effects;
            Operations = operations;
            Self = self;
            Transactions = transactions;
        }
    }
}
