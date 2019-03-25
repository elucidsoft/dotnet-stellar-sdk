namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultMissingOperation : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}