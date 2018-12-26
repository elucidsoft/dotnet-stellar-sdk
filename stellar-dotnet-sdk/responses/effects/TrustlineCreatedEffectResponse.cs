namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineCreatedEffectResponse : TrustlineCUDResponse
    {
        public override int TypeId => 20;

        public TrustlineCreatedEffectResponse()
        {

        }

        /// <inheritdoc />
        public TrustlineCreatedEffectResponse(string limit, string assetType, string assetCode, string assetIssuer)
            : base(limit, assetType, assetCode, assetIssuer)
        {
        }
    }
}