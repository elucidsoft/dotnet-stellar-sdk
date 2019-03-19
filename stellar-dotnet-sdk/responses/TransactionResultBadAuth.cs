namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultBadAuth : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}