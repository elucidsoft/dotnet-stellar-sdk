using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_home_domain_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineSponsorshipCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 50;

        [JsonProperty(PropertyName = "asset")]
        public string Asset { get; private set; }

        [JsonProperty(PropertyName = "sponsor")]
	    public string Sponsor { get; private set; }

        public TrustlineSponsorshipCreatedEffectResponse()
        {

        }

        public TrustlineSponsorshipCreatedEffectResponse(string asset, string sponsor)
        {
            Asset = asset;
            Sponsor = sponsor;
        }
    }
}