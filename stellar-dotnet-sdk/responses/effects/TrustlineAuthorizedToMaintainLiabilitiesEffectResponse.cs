using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    public class TrustlineAuthorizedToMaintainLiabilitiesEffectResponse : TrustlineAuthorizationResponse
    {
        public TrustlineAuthorizedToMaintainLiabilitiesEffectResponse()
        {
        }

        public TrustlineAuthorizedToMaintainLiabilitiesEffectResponse(string trustor, string assetType, string assetCode)
            : base(trustor, assetType, assetCode)
        {
        }
    }
}
