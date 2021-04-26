using System;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_deauthorized effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    [Obsolete("Deprecated in favor of 'TrustlineFlagsUpdatedEffectResponse'")]
    public class TrustlineDeauthorizedEffectResponse : TrustlineAuthorizationResponse
    {
        public override int TypeId => 24;

        public TrustlineDeauthorizedEffectResponse()
        {

        }

        /// <inheritdoc />
        public TrustlineDeauthorizedEffectResponse(string trustor, string assetType, string assetCode)
            : base(trustor, assetType, assetCode)
        {
        }
    }
}