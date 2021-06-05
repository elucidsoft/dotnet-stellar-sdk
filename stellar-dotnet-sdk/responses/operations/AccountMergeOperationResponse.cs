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
        public string Account { get; private set; }

        [JsonProperty(PropertyName = "account_muxed")]
        public string AccountMuxed { get; private set; }

        [JsonProperty(PropertyName = "account_muxed_id")]
        public long AccountMuxedID { get; private set; }

        [JsonProperty(PropertyName = "into")]
        public string Into { get; private set; }

        [JsonProperty(PropertyName = "into_muxed")]
        public string IntoMuxed { get; private set; }

        [JsonProperty(PropertyName = "into_muxed_id")]
        public string IntoMuxedID { get; private set; }

        public AccountMergeOperationResponse()
        {

        }


        public AccountMergeOperationResponse(string account, string into)
        {
            Account = account;
            Into = into;
        }
    }
}
