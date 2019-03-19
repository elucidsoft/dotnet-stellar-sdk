namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultTooEarly : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}