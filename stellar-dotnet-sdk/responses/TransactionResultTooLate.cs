namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Ledger closeTime after maxTime.
    /// </summary>
    public class TransactionResultTooLate : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}