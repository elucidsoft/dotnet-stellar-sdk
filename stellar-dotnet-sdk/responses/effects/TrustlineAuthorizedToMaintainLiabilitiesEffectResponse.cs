using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
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
