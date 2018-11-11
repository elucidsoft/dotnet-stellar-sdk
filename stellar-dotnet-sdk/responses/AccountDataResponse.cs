using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses
{
    public class AccountDataResponse
    {

        [JsonProperty(PropertyName = "value")]
        public string Value { get; private set; }

        public string ValueDecoded
        {
            get
            {
                var data = Convert.FromBase64String(Value);
                return Encoding.UTF8.GetString(data);
            }
        }

        public AccountDataResponse(string value)
        {
            Value = value;
        }
    }
}
