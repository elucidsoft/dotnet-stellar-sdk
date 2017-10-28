using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.responses
{
    public static class JsonSingleton
    {
        public static T GetInstance<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
