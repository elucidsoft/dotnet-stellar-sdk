using Newtonsoft.Json;
using System;

namespace stellar_dotnetcore_sdk.responses
{
    public static partial class JsonSingleton
    {
        public class KeyPairTypeAdapter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(KeyPair);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return KeyPair.FromAccountId(reader.Value.ToString());
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                // Don't need this.
            }
        }
    }
}
