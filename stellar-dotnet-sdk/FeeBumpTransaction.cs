using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class FeeBumpTransaction
    {
        public FeeBumpTransaction(KeyPair feeSource, uint fee)
        {
            FeeSource = feeSource;
            Fee = fee;
        }

        [JsonProperty(PropertyName = "fee_source")]
        public KeyPair FeeSource { get; }

        [JsonProperty(PropertyName = "fee_source")]
        public long Fee { get; }
    }
}
