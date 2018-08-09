using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using stellar_dotnet_sdk.responses.effects;

namespace stellar_dotnet_sdk.responses
{
    public class EffectDeserializer : JsonConverter
    {
        private static readonly IDictionary<int, Func<string, EffectResponse>> Effects = new ConcurrentDictionary<int, Func<string, EffectResponse>>();

        static EffectDeserializer()
        {
            if (Effects.Count > 0)
                return;

            Effects.Add(0, JsonSingleton.GetInstance<AccountCreatedEffectResponse>);
            Effects.Add(1, JsonSingleton.GetInstance<AccountRemovedEffectResponse>);
            Effects.Add(2, JsonSingleton.GetInstance<AccountCreditedEffectResponse>);
            Effects.Add(3, JsonSingleton.GetInstance<AccountDebitedEffectResponse>);
            Effects.Add(4, JsonSingleton.GetInstance<AccountThresholdsUpdatedEffectResponse>);
            Effects.Add(5, JsonSingleton.GetInstance<AccountHomeDomainUpdatedEffectResponse>);
            Effects.Add(6, JsonSingleton.GetInstance<AccountFlagsUpdatedEffectResponse>);
            Effects.Add(7, JsonSingleton.GetInstance<AccountInflationDestinationUpdatedEffectResponse>);

            // Signer effects
            Effects.Add(10, JsonSingleton.GetInstance<SignerCreatedEffectResponse>);
            Effects.Add(11, JsonSingleton.GetInstance<SignerRemovedEffectResponse>);
            Effects.Add(12, JsonSingleton.GetInstance<SignerUpdatedEffectResponse>);

            // Trustline effects
            Effects.Add(20, JsonSingleton.GetInstance<TrustlineCreatedEffectResponse>);
            Effects.Add(21, JsonSingleton.GetInstance<TrustlineRemovedEffectResponse>);
            Effects.Add(22, JsonSingleton.GetInstance<TrustlineUpdatedEffectResponse>);
            Effects.Add(23, JsonSingleton.GetInstance<TrustlineAuthorizedEffectResponse>);
            Effects.Add(24, JsonSingleton.GetInstance<TrustlineDeauthorizedEffectResponse>);

            // Trading effects
            Effects.Add(30, JsonSingleton.GetInstance<OfferCreatedEffectResponse>);
            Effects.Add(31, JsonSingleton.GetInstance<OfferRemovedEffectResponse>);
            Effects.Add(32, JsonSingleton.GetInstance<OfferUpdatedEffectResponse>);
            Effects.Add(33, JsonSingleton.GetInstance<TradeEffectResponse>);

            // Data effects
            Effects.Add(40, JsonSingleton.GetInstance<DataCreatedEffectResponse>);
            Effects.Add(41, JsonSingleton.GetInstance<DataRemovedEffectResponse>);
            Effects.Add(42, JsonSingleton.GetInstance<DataUpdatedEffectResponse>);
            Effects.Add(43, JsonSingleton.GetInstance<SequenceBumpedEffectResponse>);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject.GetValue("type_i").ToObject<int>();

            return Effects[type].Invoke(jsonObject.Root.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(EffectResponse);
        }
    }
}