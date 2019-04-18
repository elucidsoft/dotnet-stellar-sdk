namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// An unknown error occured.
    /// </summary>
    public class TransactionResultInternalError : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}