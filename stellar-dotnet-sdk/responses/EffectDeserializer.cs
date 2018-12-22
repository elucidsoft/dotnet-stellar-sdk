using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses.effects;

namespace stellar_dotnet_sdk.responses
{
    public class EffectDeserializer : JsonConverter<EffectResponse>
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, EffectResponse value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override EffectResponse ReadJson(JsonReader reader, Type objectType, EffectResponse existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject.GetValue("type_i").ToObject<int>();
            var root = jsonObject.Root.ToString();
            switch (type)
            {
                // account effects
                case 0:
                    return JsonConvert.DeserializeObject<AccountCreatedEffectResponse>(root);
                case 1:
                    return JsonConvert.DeserializeObject<AccountRemovedEffectResponse>(root);
                case 2:
                    return JsonConvert.DeserializeObject<AccountCreditedEffectResponse>(root);
                case 3:
                    return JsonConvert.DeserializeObject<AccountDebitedEffectResponse>(root);
                case 4:
                    return JsonConvert.DeserializeObject<AccountThresholdsUpdatedEffectResponse>(root);
                case 5:
                    return JsonConvert.DeserializeObject<AccountHomeDomainUpdatedEffectResponse>(root);
                case 6:
                    return JsonConvert.DeserializeObject<AccountFlagsUpdatedEffectResponse>(root);
                case 7:
                    return JsonConvert.DeserializeObject<AccountInflationDestinationUpdatedEffectResponse>(root);

                // signer effects
                case 10:
                    return JsonConvert.DeserializeObject<SignerCreatedEffectResponse>(root);
                case 11:
                    return JsonConvert.DeserializeObject<SignerRemovedEffectResponse>(root);
                case 12:
                    return JsonConvert.DeserializeObject<SignerUpdatedEffectResponse>(root);

                // trustline effects
                case 20:
                    return JsonConvert.DeserializeObject<TrustlineCreatedEffectResponse>(root);
                case 21:
                    return JsonConvert.DeserializeObject<TrustlineRemovedEffectResponse>(root);
                case 22:
                    return JsonConvert.DeserializeObject<TrustlineUpdatedEffectResponse>(root);
                case 23:
                    return JsonConvert.DeserializeObject<TrustlineAuthorizedEffectResponse>(root);
                case 24:
                    return JsonConvert.DeserializeObject<TrustlineDeauthorizedEffectResponse>(root);

                 // trading effects
                case 30:
                    return JsonConvert.DeserializeObject<OfferCreatedEffectResponse>(root);
                case 31:
                    return JsonConvert.DeserializeObject<OfferRemovedEffectResponse>(root);
                case 32:
                    return JsonConvert.DeserializeObject<OfferUpdatedEffectResponse>(root);
                case 33:
                    return JsonConvert.DeserializeObject<TradeEffectResponse>(root);

                 // data effects
                case 40:
                    return JsonConvert.DeserializeObject<DataCreatedEffectResponse>(root);
                case 41:
                    return JsonConvert.DeserializeObject<DataRemovedEffectResponse>(root);
                case 42:
                    return JsonConvert.DeserializeObject<DataUpdatedEffectResponse>(root);
                case 43:
                    return JsonConvert.DeserializeObject<SequenceBumpedEffectResponse>(root);

                default:
                    throw new JsonSerializationException($"Unknown 'type_i'='{type}'");
            }
        }
    }
}