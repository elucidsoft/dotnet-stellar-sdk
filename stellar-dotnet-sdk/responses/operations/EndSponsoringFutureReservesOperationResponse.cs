using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class EndSponsoringFutureReservesOperationResponse : OperationResponse
    {
        public override int TypeId => 17;

        [JsonProperty(PropertyName = "begin_sponsor")]
        public string BeginSponsor { get; private set; }

        public EndSponsoringFutureReservesOperationResponse()
        {

        }


        public EndSponsoringFutureReservesOperationResponse(string beginSponsor)
        {
            BeginSponsor = beginSponsor;
        }
    }
}
