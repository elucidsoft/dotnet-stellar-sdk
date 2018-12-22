using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    public class KeyPairTypeAdapter : JsonConverter<KeyPair>
    {
        public override void WriteJson(JsonWriter writer, KeyPair value, JsonSerializer serializer)
        {
            writer.WriteValue(value.AccountId);
        }

        public override KeyPair ReadJson(JsonReader reader, Type objectType, KeyPair existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            return KeyPair.FromAccountId(reader.Value.ToString());
        }
    }
}