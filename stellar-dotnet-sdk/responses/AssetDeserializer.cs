using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace stellar_dotnet_sdk.responses
{
    public class AssetDeserializer : JsonConverter<Asset>
    {
        public override void WriteJson(JsonWriter writer, Asset value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
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
            return Asset.CreateNonNativeAsset(code, KeyPair.FromAccountId(issuer));
        }
    }
}