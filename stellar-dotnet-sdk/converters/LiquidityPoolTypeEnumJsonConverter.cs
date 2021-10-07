using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.converters
{
    public class LiquidityPoolTypeEnumJsonConverter : JsonConverter<xdr.LiquidityPoolType.LiquidityPoolTypeEnum>
    {
        public override LiquidityPoolType.LiquidityPoolTypeEnum ReadJson(JsonReader reader, Type objectType, LiquidityPoolType.LiquidityPoolTypeEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            switch (reader.Value)
            {
                case "constant_product":
                    return LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT;

                default:
                    throw new Exception("type is not readable");
            }
        }

        public override void WriteJson(JsonWriter writer, LiquidityPoolType.LiquidityPoolTypeEnum value, JsonSerializer serializer)
        {
            switch (value)
            {
                case LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT:
                    writer.WriteValue("constant_product");
                    break;

                default:
                    throw new Exception("type is not readable");
            }
        }
    }
}
