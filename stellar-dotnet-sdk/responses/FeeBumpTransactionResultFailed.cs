namespace stellar_dotnet_sdk.responses;

/// <summary>
/// One of the operations in the inner transaction failed (none were applied).
/// </summary>
public class FeeBumpTransactionResultFailed : TransactionResult
{
    public override bool IsSuccess => false;

    public InnerTransactionResultPair InnerResultPair { get; set; }
}