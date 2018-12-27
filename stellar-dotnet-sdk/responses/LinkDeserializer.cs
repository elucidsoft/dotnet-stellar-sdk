using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stellar_dotnet_sdk.responses
{
    // We have to use reflection because Newtonsoft.Json does not support generic JsonConverters
    // https://github.com/JamesNK/Newtonsoft.Json/issues/1332
    public class LinkDeserializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Link link)
            {
                var jsonObject = new JObject();
                jsonObject.Add(new JProperty("href", link.Href));
                if (link.Templated)
                {
                    jsonObject.Add(new JProperty("templated", link.Templated));
                }

                jsonObject.WriteTo(writer);
            }
            else
            {
                throw new JsonSerializationException();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;

            var jsonObject = JObject.Load(reader);
            var templated = jsonObject.GetValue("templated")?.ToObject<bool>() ?? false;
            var href = jsonObject.GetValue("href")?.ToObject<string>();

            if (href is null) throw new JsonSerializationException();

            return objectType.GetMethod("Create")?.Invoke(null, new object[] {href, templated});
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Link<>);
        }
    }
}