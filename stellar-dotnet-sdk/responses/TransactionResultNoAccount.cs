namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultNoAccount : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}