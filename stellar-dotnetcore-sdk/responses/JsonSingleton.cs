using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.responses
{
    public static partial class JsonSingleton
    {
        public static T GetInstance<T>(string content)
        {
            var jsonConverters = new JsonConverter[] 
            {
                new KeyPairTypeAdapter()
            };

            return JsonConvert.DeserializeObject<T>(content, jsonConverters);
        }
    }
}
