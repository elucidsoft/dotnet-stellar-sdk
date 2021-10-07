using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.converters
{
    public class LiquidityPoolIDJsonConverter : JsonConverter<LiquidityPoolID>
    {
        public override LiquidityPoolID ReadJson(JsonReader reader, Type objectType, LiquidityPoolID existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return new LiquidityPoolID((string)reader.Value);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, LiquidityPoolID value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }

}
