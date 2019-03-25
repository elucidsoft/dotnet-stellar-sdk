namespace stellar_dotnet_sdk.responses
{
    public class TransactionResultBadSeq : TransactionResult
    {
        public override bool IsSuccess => false;
    }
}