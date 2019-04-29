namespace stellar_dotnet_sdk.responses.results
{
    /// <summary>
    /// Offer updated.
    /// </summary>
    public class ManageSellOfferUpdated : ManageSellOfferSuccess
    {
        /// <summary>
        /// The offer that was updated.
        /// </summary>
        public OfferEntry Offer { get; set; }
    }
}