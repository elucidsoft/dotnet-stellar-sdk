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
        [JsonProperty(PropertyName = "account")]
        public string Account { get; }

        [JsonProperty(PropertyName = "into")]
        public string Into { get; }

        public AccountMergeOperationResponse(string account, string into)
        {
            Account = account;
            Into = into;
        }
    }
}