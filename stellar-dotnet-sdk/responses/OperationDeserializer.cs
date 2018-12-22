using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk.responses
{
    public class OperationDeserializer : JsonConverter<OperationResponse>
    {
        public override void WriteJson(JsonWriter writer, OperationResponse value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override OperationResponse ReadJson(JsonReader reader, Type objectType, OperationResponse existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject.GetValue("type_i").ToObject<int>();
            var root = jsonObject.Root.ToString();

            switch (type)
            {
                case 0:
                    return JsonConvert.DeserializeObject<CreateAccountOperationResponse>(root);
                case 1:
                    return JsonConvert.DeserializeObject<PaymentOperationResponse>(root);
                case 2:
                    return JsonConvert.DeserializeObject<PathPaymentOperationResponse>(root);
                case 3:
                    return JsonConvert.DeserializeObject<ManageOfferOperationResponse>(root);
                case 4:
                    return JsonConvert.DeserializeObject<CreatePassiveOfferOperationResponse>(root);
                case 5:
                    return JsonConvert.DeserializeObject<SetOptionsOperationResponse>(root);
                case 6:
                    return JsonConvert.DeserializeObject<ChangeTrustOperationResponse>(root);
                case 7:
                    return JsonConvert.DeserializeObject<AllowTrustOperationResponse>(root);
                case 8:
                    return JsonConvert.DeserializeObject<AccountMergeOperationResponse>(root);
                case 9:
                    return JsonConvert.DeserializeObject<InflationOperationResponse>(root);
                case 10:
                    return JsonConvert.DeserializeObject<ManageDataOperationResponse>(root);
                case 11:
                    return JsonConvert.DeserializeObject<BumpSequenceOperationResponse>(root);
                default:
                    throw new Exception($"Invalid operation 'type_i'='{type}'");
            }
        }
    }
}