using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class BeginSponsoringFutureReservesOperationResponse : OperationResponse
    {
        public override int TypeId => 16;

        [JsonProperty(PropertyName = "sponsored_id")]
        public string SponsoredID { get; private set; }

        public BeginSponsoringFutureReservesOperationResponse()
        {

        }


        public BeginSponsoringFutureReservesOperationResponse(string sponsoredID)
        {
            SponsoredID = sponsoredID;
        }
    }
}
