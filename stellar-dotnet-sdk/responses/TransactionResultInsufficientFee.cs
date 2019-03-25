namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultInsufficientFee : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}