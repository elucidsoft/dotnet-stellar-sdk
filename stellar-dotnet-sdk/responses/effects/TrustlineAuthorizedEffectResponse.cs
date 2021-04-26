using System;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_authorized effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    [Obsolete("Deprecated in favor of 'TrustlineFlagsUpdatedEffectResponse'")]
    public class TrustlineAuthorizedEffectResponse : TrustlineAuthorizationResponse
    {
        public override int TypeId => 23;

        public TrustlineAuthorizedEffectResponse()
        {

        }

        /// <inheritdoc />
        public TrustlineAuthorizedEffectResponse(string trustor, string assetType, string assetCode)
            : base(trustor, assetType, assetCode)
        {
        }
    }
}
