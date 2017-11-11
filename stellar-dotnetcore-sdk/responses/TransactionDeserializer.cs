using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace stellar_dotnetcore_sdk.responses
{
    public class TransactionDeserializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.DateParseHandling = DateParseHandling.None;

            var jsonObject = JObject.Load(reader);

            TransactionResponse transaction = jsonObject.ToObject<TransactionResponse>();


            var memoType = jsonObject.GetValue("memo_type").ToObject<string>();

            Memo memo;
            if (memoType.Equals("none"))
            {
                memo = Memo.None();
            }
            else
            {
                String memoValue = transaction.MemoStr;

                if (memoType.Equals("text"))
                {
                    memo = Memo.Text(memoValue);
                }
                else if (memoType.Equals("id"))
                {
                    memo = Memo.Id(long.Parse(memoValue));
                }
                else if (memoType.Equals("hash"))
                {
                    memo = Memo.Hash(Convert.FromBase64String(memoValue));
                }
                else if (memoType.Equals("return"))
                {
                    memo = Memo.ReturnHash(Convert.FromBase64String(memoValue));
                }
                else
                {
                    throw new JsonException("Unknown memo type.");
                }
            }

            transaction.Memo = memo;
            return transaction;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TransactionResponse);
        }
    }
}
