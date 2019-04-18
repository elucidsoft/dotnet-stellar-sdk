namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Sequence number does not match source account.
    /// </summary>
    public class TransactionResultBadSeq : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}