namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Fee would bring account below reserve.
    /// </summary>
    public class TransactionResultInsufficientBalance : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}