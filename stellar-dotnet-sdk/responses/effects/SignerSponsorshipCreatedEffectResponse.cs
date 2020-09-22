using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents signer_sponsorship_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class SignerSponsorshipCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 69;

        [JsonProperty(PropertyName = "signer")]
        public string Signer { get; private set; }

        [JsonProperty(PropertyName = "sponsor")]
        public string Sponsor { get; private set; }

        public SignerSponsorshipCreatedEffectResponse()
        {

        }

        public SignerSponsorshipCreatedEffectResponse(string signer, string sponsor)
        {
            Signer = signer;
            Sponsor = sponsor;
        }
    }
}