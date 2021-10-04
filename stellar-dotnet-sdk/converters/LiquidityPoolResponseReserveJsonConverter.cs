using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.converters
{
    public class LiquidityPoolResponseReserveJsonConverter : JsonConverter<LiquidityPoolResponse.Reserve>
    {
        public override LiquidityPoolResponse.Reserve ReadJson(JsonReader reader, Type objectType, LiquidityPoolResponse.Reserve existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jt = JToken.ReadFrom(reader);
            var asset = Asset.Create(jt.Value<string>("asset"));
            var amount = jt.Value<string>("amount");

            return new LiquidityPoolResponse.Reserve(amount, asset);
        }

        public override void WriteJson(JsonWriter writer, LiquidityPoolResponse.Reserve value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            jo.Add("asset", value.Asset.CanonicalName());
            jo.Add("amount", value.Amount);
            jo.WriteTo(writer);
        }
    }
}
