using System;
using Newtonsoft.Json;

namespace stellar_dotnetcore_sdk.responses.effects
{
    public class EffectResponse : Response
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
        public Links LinksValue { get; protected set; }


        /// <summary>
        /// Represents effect links.
        /// </summary>
        public class Links
        {
            [JsonProperty(PropertyName = "operation")]
            public Link Operation { get; }


            [JsonProperty(PropertyName = "precedes")]
            public Link Precedes { get; }

            [JsonProperty(PropertyName = "succeeds")]
            public Link Succeeds { get; }

            public Links(Link operation, Link precedes, Link succeeds)
            {
                Operation = operation;
                Precedes = precedes;
                this.Succeeds = succeeds;
            }
        }
    }
}
