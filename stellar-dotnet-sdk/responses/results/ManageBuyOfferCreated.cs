namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// Offer created.
    /// </summary>
    public class ManageBuyOfferCreated : ManageBuyOfferSuccess
    {
        /// <summary>
        /// The offer that was created.
        /// </summary>
        public OfferEntry Offer { get; set; }
    }
}