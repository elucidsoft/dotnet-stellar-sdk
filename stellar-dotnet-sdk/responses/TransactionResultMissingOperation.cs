namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// No operation was specified.
    /// </summary>
    public class TransactionResultMissingOperation : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}