using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stellar_dotnet_sdk.responses
{
    public class AssetDeserializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var type = jsonObject.GetValue("asset_type").ToObject<string>();

            if (type == "native")
            {
                return new AssetTypeNative();
            }

            var code = jsonObject.GetValue("asset_code").ToObject<string>();
            var issuer = jsonObject.GetValue("asset_issuer").ToObject<string>();
            return Asset.CreateNonNativeAsset(code, issuer);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Asset);
        }
    }
}