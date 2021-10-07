using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.converters
{
    public class AssetAmountJsonConverter : JsonConverter<AssetAmount>
    {
        public override AssetAmount ReadJson(JsonReader reader, Type objectType, AssetAmount existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jt = JToken.ReadFrom(reader);
            var asset = Asset.Create(jt.Value<string>("asset"));
            var amount = jt.Value<string>("amount");

            return new AssetAmount(asset, amount);
        }

        public override void WriteJson(JsonWriter writer, AssetAmount value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            jo.Add("asset", value.Asset.CanonicalName());
            jo.Add("amount", value.Amount);
            jo.WriteTo(writer);
        }
    }
}
