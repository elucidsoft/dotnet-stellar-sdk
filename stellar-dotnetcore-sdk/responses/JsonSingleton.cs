using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnetcore_sdk.responses.accountResponse;
using stellar_dotnetcore_sdk.responses.page;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

namespace stellar_dotnetcore_sdk.responses
{
    public static partial class JsonSingleton
    {
        public static T GetInstance<T>(string content)
        {
            var pageResponseConvertersions = new Type[]
            {
                typeof(Page<AccountResponse>) //TODO: ,
                //TODO: typeof(Page<EffectResponse>),
                //TODO: typeof(Page<LedgerResponse>),
                //TODO: typeof(Page<OfferResponse>),
                //TODO: typeof(Page<OperationResponse>),
                //TODO: typeof(Page<PathResponse>),
                //TODO: typeof(Page<TradeResponse>),
                //TODO: typeof(Page<TransactionResponse>)
            };

            var jsonConverters = new JsonConverter[]
            {
                new KeyPairTypeAdapter()
            };

            if(pageResponseConvertersions.Contains(typeof(T)))
                content = PageAccountResponseConverter(content);

            return JsonConvert.DeserializeObject<T>(content, jsonConverters);
        }

        private static string PageAccountResponseConverter(string content)
        {
            JObject json = JObject.Parse(content);
            JObject newJson = new JObject();
            newJson.Add("records", json.SelectToken("$._embedded.records"));
            newJson.Add("links", json.SelectToken("$.._links"));

            return newJson.Root.ToString();
        }
    }
}
