namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_deauthorized effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineDeauthorizedEffectResponse : TrustlineAuthorizationResponse
    {
        public override int TypeId => 24;

        /// <inheritdoc />
        public TrustlineDeauthorizedEffectResponse(KeyPair trustor, string assetType, string assetCode)
            : base(trustor, assetType, assetCode)
        {
        }
    }
}