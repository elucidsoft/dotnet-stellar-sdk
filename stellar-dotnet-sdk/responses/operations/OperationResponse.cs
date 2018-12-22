using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Abstract class for operation responses.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public abstract class OperationResponse : IPagingToken
    {
        /// <summary>
        /// Id of the operation
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long Id { get; private set; }

        /// <summary>
        /// Source Account of Operation
        /// </summary>
        [JsonProperty(PropertyName = "source_account")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair SourceAccount { get; private set; }

        /// <summary>
        /// Paging Token of Paging
        /// </summary>
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

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; private set; }

        /// <summary>
        /// Returns transaction hash of transaction this operation belongs to.
        /// </summary>
        [JsonProperty(PropertyName = "transaction_hash")]
        public string TransactionHash { get; private set; }

        /// <summary>
        /// Links of Paging
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public OperationResponseLinks Links { get; private set; }
    }
}