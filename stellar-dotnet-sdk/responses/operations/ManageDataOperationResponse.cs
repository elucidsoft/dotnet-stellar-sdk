using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents ManageData operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class ManageDataOperationResponse : OperationResponse
    {
        public override int TypeId => 10;

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public string Value { get; }

        public ManageDataOperationResponse(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}