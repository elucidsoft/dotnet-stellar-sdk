namespace stellar_dotnet_sdk.responses.results
{
    public class AccountMergeSuccess : AccountMergeResult
    {
        public override bool IsSuccess => true;

        public string SourceAccountBalance { get; set; }
    }
}