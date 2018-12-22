using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            var response = CreateResponse(type);
            serializer.Populate(jsonObject.CreateReader(), response);
            return response;
        }

        private static EffectResponse CreateResponse(int type)
            {
                switch (type)
                {
                    // account effects
                    case 0:
                        return new AccountCreatedEffectResponse();
                    case 1:
                        return new AccountRemovedEffectResponse();
                    case 2:
                        return new AccountCreditedEffectResponse();
                    case 3:
                        return new AccountDebitedEffectResponse();
                    case 4:
                        return new AccountThresholdsUpdatedEffectResponse();
                    case 5:
                        return new AccountHomeDomainUpdatedEffectResponse();
                    case 6:
                        return new AccountFlagsUpdatedEffectResponse();
                    case 7:
                        return new AccountInflationDestinationUpdatedEffectResponse();

                    // signer effects
                    case 10:
                        return new SignerCreatedEffectResponse();
                    case 11:
                        return new SignerRemovedEffectResponse();
                    case 12:
                        return new SignerUpdatedEffectResponse();

                    // trustline effects
                    case 20:
                        return new TrustlineCreatedEffectResponse();
                    case 21:
                        return new TrustlineRemovedEffectResponse();
                    case 22:
                        return new TrustlineUpdatedEffectResponse();
                    case 23:
                        return new TrustlineAuthorizedEffectResponse();
                    case 24:
                        return new TrustlineDeauthorizedEffectResponse();

                    // trading effects
                    case 30:
                        return new OfferCreatedEffectResponse();
                    case 31:
                        return new OfferRemovedEffectResponse();
                    case 32:
                        return new OfferUpdatedEffectResponse();
                    case 33:
                        return new TradeEffectResponse();

                    // data effects
                    case 40:
                        return new DataCreatedEffectResponse();
                    case 41:
                        return new DataRemovedEffectResponse();
                    case 42:
                        return new DataUpdatedEffectResponse();
                    case 43:
                        return new SequenceBumpedEffectResponse();

                    default:
                        throw new JsonSerializationException($"Unknown 'type_i'='{type}'");
                }
            }
        }
    }