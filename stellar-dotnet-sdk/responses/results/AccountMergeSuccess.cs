namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// Operation successful.
    /// </summary>
    public class AccountMergeSuccess : AccountMergeResult
    {
        public override bool IsSuccess => true;

        /// <summary>
        /// How much got transferred from source account.
        /// </summary>
        public string SourceAccountBalance { get; set; }
    }
}