using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk.responses
{
    public class OperationDeserializer : JsonConverter<OperationResponse>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, OperationResponse value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override OperationResponse ReadJson(JsonReader reader, Type objectType, OperationResponse existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject.GetValue("type_i").ToObject<int>();
            var response = CreateResponse(type);
            serializer.Populate(jsonObject.CreateReader(), response);
            return response;
        }

        private static OperationResponse CreateResponse(int type)
        {
            switch (type)
            {
                case 0:
                    return new CreateAccountOperationResponse();
                case 1:
                    return new PaymentOperationResponse();
                case 2:
                    return new PathPaymentStrictReceiveOperationResponse();
                case 3:
                    return new ManageSellOfferOperationResponse();
                case 4:
                    return new CreatePassiveOfferOperationResponse();
                case 5:
                    return new SetOptionsOperationResponse();
                case 6:
                    return new ChangeTrustOperationResponse();
                case 7:
                    return new AllowTrustOperationResponse();
                case 8:
                    return new AccountMergeOperationResponse();
                case 9:
                    return new InflationOperationResponse();
                case 10:
                    return new ManageDataOperationResponse();
                case 11:
                    return new BumpSequenceOperationResponse();
                case 12:
                    return new ManageBuyOfferOperationResponse();
                case 13:
                    return new PathPaymentStrictSendOperationResponse();
                default:
                    throw new JsonSerializationException($"Invalid operation 'type_i'='{type}'");
            }
        }
    }
}