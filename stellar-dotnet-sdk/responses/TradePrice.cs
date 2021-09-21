using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses
{
    public class TradePrice
    {
        [JsonProperty(PropertyName = "n")]
        public string N { get; set; }

        [JsonProperty(PropertyName = "d")]
        public string D { get; set; }

        public TradePrice(string n, string d)
        {
            n = N;
            d = D;
        }

        public override string ToString()
        {
            return decimal.Divide(decimal.Parse(N), decimal.Parse(D)).ToString();
        }
    }
}
