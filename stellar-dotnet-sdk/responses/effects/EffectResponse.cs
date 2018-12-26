using Newtonsoft.Json;
using System;
using stellar_dotnet_sdk.responses.operations;

namespace stellar_dotnet_sdk.responses.effects
{
    [JsonConverter(typeof(EffectDeserializer))]
    public abstract class EffectResponse : Response, IPagingToken
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; protected set; }

        [JsonProperty(PropertyName = "account")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair Account { get; protected set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }

        [JsonProperty(PropertyName = "type_i")]
        public virtual int TypeId { get; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; protected set; }

        [JsonProperty(PropertyName = "_links")]
        public EffectsResponseLinks Links { get; protected set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Represents effect links.
        /// </summary>
        public class EffectsResponseLinks
        {
            [JsonProperty(PropertyName = "operation")]
            public Link<OperationResponse> Operation { get; }

            [JsonProperty(PropertyName = "precedes")]
            public Link<EffectResponse> Precedes { get; }

            [JsonProperty(PropertyName = "succeeds")]
            public Link<EffectResponse> Succeeds { get; }

            public EffectsResponseLinks(Link<OperationResponse> operation, Link<EffectResponse> precedes,
                Link<EffectResponse> succeeds)
            {
                Operation = operation;
                Precedes = precedes;
                Succeeds = succeeds;
            }
        }
    }
}