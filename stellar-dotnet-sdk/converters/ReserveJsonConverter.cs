using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.converters
{
    public class ReserveJsonConverter : JsonConverter<Reserve>
    {
        public override Reserve ReadJson(JsonReader reader, Type objectType, Reserve existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var reserve = new Reserve();

            var jt = JToken.ReadFrom(reader);

            var assetName = jt.Value<string>("asset");
            reserve.Asset = assetName == null ? null : Asset.Create(jt.Value<string>("asset"));

            var amount = jt.Value<string>("amount");
            reserve.Amount = amount;

            return reserve;
        }

        public override void WriteJson(JsonWriter writer, Reserve value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            if (value.Asset != null)
            {
                jo.Add("asset", value.Asset.CanonicalName());
            }

            jo.Add("amount", value.Amount);
            jo.WriteTo(writer);
        }
    }
}
