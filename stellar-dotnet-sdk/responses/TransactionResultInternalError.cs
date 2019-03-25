namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultInternalError : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}