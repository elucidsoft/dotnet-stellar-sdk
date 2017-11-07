using System;
using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses
{
    public class MemoDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Memo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}