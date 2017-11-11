namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_removed effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineRemovedEffectResponse : TrustlineCUDResponse
    {
        /// <inheritdoc />
        public TrustlineRemovedEffectResponse(string limit, string assetType, string assetCode, string assetIssuer) 
            : base(limit, assetType, assetCode, assetIssuer)
        {
        }
    }
}