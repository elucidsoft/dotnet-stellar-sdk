using Newtonsoft.Json;
using System;

namespace stellar_dotnet_sdk.responses.effects
{
    public abstract class EffectResponse : Response, IPagingToken
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; protected set; }

        [JsonProperty(PropertyName = "account")]
        public KeyPair Account { get; protected set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; protected set; }

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
            public Link Operation { get; }


            [JsonProperty(PropertyName = "precedes")]
            public Link Precedes { get; }

            [JsonProperty(PropertyName = "succeeds")]
            public Link Succeeds { get; }

            public EffectsResponseLinks(Link operation, Link precedes, Link succeeds)
            {
                Operation = operation;
                Precedes = precedes;
                Succeeds = succeeds;
            }
        }
    }
}
