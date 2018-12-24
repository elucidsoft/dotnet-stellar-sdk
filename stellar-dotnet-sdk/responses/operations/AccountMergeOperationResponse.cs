using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class AccountMergeOperationResponse : OperationResponse
    {
        public override int TypeId => 8;

        [JsonProperty(PropertyName = "account")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair Account { get; private set; }

        [JsonProperty(PropertyName = "into")]
        [JsonConverter(typeof(KeyPairTypeAdapter))]
        public KeyPair Into { get; private set; }

        public AccountMergeOperationResponse()
        {

        }

        public AccountMergeOperationResponse(KeyPair account, KeyPair into)
        {
            Account = account;
            Into = into;
        }
    }
}