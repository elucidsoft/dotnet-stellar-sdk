using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses
{
    public class LiquidityPoolResponse : Response
    {
        public LiquidityPoolID ID { get; set; }

        public string PagingToken { get; set; }

        public int FeeBP { get; set; }

        public xdr.LiquidityPoolType.LiquidityPoolTypeEnum Type { get; set; }

        public string TotalTrustlines { get; set; }
        public string TotalShares { get; set; }
        public Reserve[] Reserves { get; set; }
        public Links Links { get; set; }

        public class Reserve
        {
            [JsonProperty(PropertyName = "amount")]
            public string Amount { get; set; }

            [JsonProperty(PropertyName = "asset")]
            public Asset Asset { get; set; }

            /*
            public Reserve(string amount, string asset)
            {
                Amount = amount;

                if(string.IsNullOrEmpty(asset))
                {
                    throw new ArgumentNullException(nameof(asset), "asset cannot be null");
                }
                else
                {
                    Asset = Asset.(asset);
                }
            }*/

            public Reserve(string amount, Asset asset)
            {
                Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                Asset = asset ?? throw new ArgumentNullException(nameof(amount), "asset cannot be null");
            }

            public override bool Equals(object obj)
            {
                if(!(obj is LiquidityPoolResponse.Reserve))
                {
                    return false;
                }

                LiquidityPoolResponse.Reserve other = (LiquidityPoolResponse.Reserve)obj;
                return Object.Equals(Asset, other.Asset) && Object.Equals(Amount, other.Amount);
            }
        }

        public class Links
        {
            [JsonProperty(PropertyName = "effects")]
            public Link Effects { get; set; }

            [JsonProperty(PropertyName = "operations")]
            public Link Operations { get; set; }

            [JsonProperty(PropertyName = "self")]
            public Link Self { get; set; }

            [JsonProperty(PropertyName = "transactions")]
            public Link Transactions { get; set; }

            public Links (Link effects, Link operations, Link self, Link transactions)
            {
                Effects = effects;
                Operations = operations;
                Self = self;
                Transactions = transactions;
            }
        }
    }
}
