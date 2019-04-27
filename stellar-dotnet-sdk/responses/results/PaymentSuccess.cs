namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// Operation successful.
    /// </summary>
    public class PaymentSuccess : PaymentResult
    {
        public override bool IsSuccess => true;
    }
}