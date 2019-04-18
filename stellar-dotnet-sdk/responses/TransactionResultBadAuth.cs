namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Too few valid signatures or invalid network.
    /// </summary>
    public class TransactionResultBadAuth : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}