namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Ledger closeTime before minTime.
    /// </summary>
    public class TransactionResultTooEarly : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}