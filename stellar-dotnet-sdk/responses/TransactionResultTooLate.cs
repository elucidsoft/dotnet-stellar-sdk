namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultTooLate : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}