namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// Clawback balance doesn't exist
    /// </summary>
    public class ClawbackSuccess : ClawbackResult
    {
        public override bool IsSuccess => true;
    }
}
