namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents Inflation operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class InflationOperationResponse : OperationResponse
    {
        public override int TypeId => 9;

        public InflationOperationResponse()
        {

        }
    }
}