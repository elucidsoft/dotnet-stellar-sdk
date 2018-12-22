using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace stellar_dotnet_sdk.responses
{
    public class TransactionDeserializer : JsonConverter<TransactionResponse>
    {
        public override void WriteJson(JsonWriter writer, TransactionResponse value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override TransactionResponse ReadJson(JsonReader reader, Type objectType, TransactionResponse existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            reader.DateParseHandling = DateParseHandling.None;

            var jsonObject = JObject.Load(reader);

            TransactionResponse transaction = jsonObject.ToObject<TransactionResponse>();
            return transaction;
        }
    }
}