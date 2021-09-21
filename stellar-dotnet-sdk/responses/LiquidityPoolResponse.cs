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
        public LiquidityPoolResponseLinks Links { get; set; }

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
                if(!(obj is Reserve))
                {
                    return false;
                }

                Reserve other = (Reserve)obj;
                return Equals(Asset, other.Asset) && Equals(Amount, other.Amount);
            }
        }    
    }
}
