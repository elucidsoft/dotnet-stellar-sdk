using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class InnerTransaction
    {
        public InnerTransaction(long maxFee)
        {
            MaxFee = maxFee;
        }

        [JsonProperty("max_fee")]
        public long MaxFee { get; }
    }
}
