namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    /// Fee is too small.
    /// </summary>
    public class TransactionResultInsufficientFee : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}