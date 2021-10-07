using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses.effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.converters
{
    public class LiquidityPoolClaimableAssetAmountJsonConverter : JsonConverter<LiquidityPoolClaimableAssetAmount>
    {
        public override LiquidityPoolClaimableAssetAmount ReadJson(JsonReader reader, Type objectType, LiquidityPoolClaimableAssetAmount existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jt = JToken.ReadFrom(reader);
            var asset = Asset.Create(jt.Value<string>("asset"));
            var amount = jt.Value<string>("amount");
            var claimableBalanceID = jt.Value<string>("claimable_balance_id");

            return new LiquidityPoolClaimableAssetAmount(asset, amount, claimableBalanceID);
        }

        public override void WriteJson(JsonWriter writer, LiquidityPoolClaimableAssetAmount value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            jo.Add("asset", value.Asset.CanonicalName());
            jo.Add("amount", value.Amount);
            jo.Add("claimable_balance_id", value.ClaimableBalanceID);
            jo.WriteTo(writer);
        }
    }
}
