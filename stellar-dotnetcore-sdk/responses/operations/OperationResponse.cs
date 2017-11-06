using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using stellar_dotnetcore_sdk;

namespace stellar_dotnetcore_sdk.responses.operations
{ 
    /// <summary>
    /// Abstract class for operation responses.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="OperationRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public abstract class OperationResponse
    {

        [JsonProperty(PropertyName = "id")]
        public long Id { get; private set; }

        [JsonProperty(PropertyName = "source_account")]
        public KeyPair SourceAccount { get; private set; }

        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; private set; }
        
        /// <summary>
        /// Returns operation type. Possible types: 
        /// crete_account
        /// payment
        /// allow_trust
        /// change_trust
        /// set_options
        /// account_merge
        /// manage_offer
        /// path_payments
        /// create_passive_offer
        /// inflation
        /// manage_data
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; private set; }

        [JsonProperty(PropertyName = "_links")]
        public OperationResponseLinks Links { get; private set; }

    }

    public class OperationResponseLinks
    {
        [JsonProperty(PropertyName = "effects")]
        public Link Effects { get; private set; }

        [JsonProperty(PropertyName = "precedes")]
        public Link Precedes { get; private set; }

        [JsonProperty(PropertyName = "self")]
        public Link Self { get; private set; }

        [JsonProperty(PropertyName = "succeeds")]
        public Link Succeeds { get; private set; }

        [JsonProperty(PropertyName = "transaction")]
        public Link Transaction { get; private set; }

        public OperationResponseLinks(Link effects, Link precedes, Link self, Link succeeds, Link transaction)
        {
            Effects = effects;
            Precedes = precedes;
            Self = self;
            Succeeds = succeeds;
            Transaction = transaction;
        }
    }
}
