namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineUpdatedEffectResponse : TrustlineCUDResponse
    {
        /// <inheritdoc />
        public TrustlineUpdatedEffectResponse(string limit, string assetType, string assetCode, string assetIssuer) 
            : base(limit, assetType, assetCode, assetIssuer)
        {
        }
    }
}