namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultInsufficientBalance : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}