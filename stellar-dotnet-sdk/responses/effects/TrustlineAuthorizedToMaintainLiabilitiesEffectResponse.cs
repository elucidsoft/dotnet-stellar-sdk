using System;

namespace stellar_dotnet_sdk.responses.effects
{
    [Obsolete("Deprecated in favor of 'TrustlineFlagsUpdatedEffectResponse'")]
    public class TrustlineAuthorizedToMaintainLiabilitiesEffectResponse : TrustlineAuthorizationResponse
    {
        public override int TypeId => 25;

        public TrustlineAuthorizedToMaintainLiabilitiesEffectResponse()
        {
        }

        public TrustlineAuthorizedToMaintainLiabilitiesEffectResponse(string trustor, string assetType, string assetCode)
            : base(trustor, assetType, assetCode)
        {
        }
    }
}
