using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnetcore_sdk.responses.effects;
using stellar_dotnetcore_sdk.responses.operations;
using stellar_dotnetcore_sdk.responses.page;

namespace stellar_dotnetcore_sdk.responses
{
    public static class JsonSingleton
    {
        public static T GetInstance<T>(string content)
        {
            var pageResponseConversions = new[]
            {
                typeof(Page<AccountResponse>),
                typeof(Page<EffectResponse>),
                typeof(Page<LedgerResponse>),
                typeof(Page<OfferResponse>),
                typeof(Page<OperationResponse>),
                typeof(Page<PathResponse>),
                typeof(Page<TransactionResponse>),
                typeof(Page<TradeResponse>),
                typeof(Page<TransactionResponse>)
            };

            var jsonConverters = new JsonConverter[]
            {
                new AssetDeserializer(),
                new KeyPairTypeAdapter(),
                new OperationDeserializer(),
                new EffectDeserializer(),
                new TransactionDeserializer()
            };

            var pageJsonConverters = new JsonConverter[]
            {
                new AssetDeserializer(),
                new KeyPairTypeAdapter(),
                new OperationDeserializer(),
                new EffectDeserializer()
            };

            if (pageResponseConversions.Contains(typeof(T)))
            {
                content = PageAccountResponseConverter(content);
                return JsonConvert.DeserializeObject<T>(content, pageJsonConverters);
            }

            return JsonConvert.DeserializeObject<T>(content, jsonConverters);
        }

        private static string PageAccountResponseConverter(string content)
        {
            var json = JObject.Parse(content);
            var newJson = new JObject();
            newJson.Add("records", json.SelectToken("$._embedded.records"));
            newJson.Add("links", json.SelectToken("$._links"));

            return newJson.Root.ToString();
        }
    }
}