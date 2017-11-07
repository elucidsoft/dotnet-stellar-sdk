using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnetcore_sdk.responses.effects;

namespace stellar_dotnetcore_sdk.responses
{
    public class EffectDeserializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject.GetValue("type_i").ToObject<int>();

            switch (type)
            {
                case 0:
                    return JsonSingleton.GetInstance<AccountCreatedEffectResponse>(jsonObject.Root.ToString());
                case 1:
                    return JsonSingleton.GetInstance<AccountRemovedEffectResponse>(jsonObject.Root.ToString());
                case 2:
                    return JsonSingleton.GetInstance<AccountCreditedEffectResponse>(jsonObject.Root.ToString());
                case 3:
                    return JsonSingleton.GetInstance<AccountDebitedEffectResponse>(jsonObject.Root.ToString());
                case 4:
                    return JsonSingleton.GetInstance<AccountThresholdsUpdatedEffectResponse>(jsonObject.Root.ToString());
                case 5:
                    return JsonSingleton.GetInstance<AccountHomeDomainUpdatedEffectResponse>(jsonObject.Root.ToString());
                case 6:
                    return JsonSingleton.GetInstance<AccountFlagsUpdatedEffectResponse>(jsonObject.Root.ToString());
                // Signer effects
                case 10:
                    return JsonSingleton.GetInstance<SignerCreatedEffectResponse>(jsonObject.Root.ToString());
                case 11:
                    return JsonSingleton.GetInstance<SignerRemovedEffectResponse>(jsonObject.Root.ToString());
                case 12:
                    return JsonSingleton.GetInstance<SignerUpdatedEffectResponse>(jsonObject.Root.ToString());
                // Trustline effects
                case 20:
                    return JsonSingleton.GetInstance<TrustlineCreatedEffectResponse>(jsonObject.Root.ToString());
                case 21:
                    return JsonSingleton.GetInstance<TrustlineRemovedEffectResponse>(jsonObject.Root.ToString());
                case 22:
                    return JsonSingleton.GetInstance<TrustlineUpdatedEffectResponse>(jsonObject.Root.ToString());
                case 23:
                    return JsonSingleton.GetInstance<TrustlineAuthorizedEffectResponse>(jsonObject.Root.ToString());
                case 24:
                    return JsonSingleton.GetInstance<TrustlineDeauthorizedEffectResponse>(jsonObject.Root.ToString());
                // Trading effects
                case 30:
                    return JsonSingleton.GetInstance<OfferCreatedEffectResponse>(jsonObject.Root.ToString());
                case 31:
                    return JsonSingleton.GetInstance<OfferRemovedEffectResponse>(jsonObject.Root.ToString());
                case 32:
                    return JsonSingleton.GetInstance<OfferUpdatedEffectResponse>(jsonObject.Root.ToString());
                case 33:
                    return JsonSingleton.GetInstance<TradeEffectResponse>(jsonObject.Root.ToString());
                default:
                    throw new Exception("Invalid operation type");
            }
}

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EffectResponse);
        }
    }
}