using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace stellar_dotnet_sdk.responses
{
    public class AssetDeserializer : JsonConverter<Asset>
    {
        public override void WriteJson(JsonWriter writer, Asset value, JsonSerializer serializer)
        {
            var jsonObject = new JObject();
            var assetType = new JProperty("asset_type", value.GetType());
            jsonObject.Add(assetType);
            if (value is AssetTypeCreditAlphaNum credit)
            {
                var code = new JProperty("asset_code", credit.Code);
                jsonObject.Add(code);
                var issuer = new JProperty("asset_issuer", credit.Issuer);
                jsonObject.Add(issuer);
            }
            jsonObject.WriteTo(writer);
        }

        public override Asset ReadJson(JsonReader reader, Type objectType, Asset existingValue, bool hasExistingValue,
            JsonSerializer serializer)
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
    }
}