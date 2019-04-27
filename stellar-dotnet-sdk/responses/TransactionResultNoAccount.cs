namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Source account not found.
    /// </summary>
    public class TransactionResultNoAccount : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}