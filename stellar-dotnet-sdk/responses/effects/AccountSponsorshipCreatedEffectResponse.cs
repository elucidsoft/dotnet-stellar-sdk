using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_sponsorship_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountSponsorshipCreatedEffectResponse : EffectResponse
    {
        public override int TypeId => 60;

        [JsonProperty(PropertyName = "sponsor")]
        public string Sponsor { get; private set; }

        public AccountSponsorshipCreatedEffectResponse()
        {

        }

        public AccountSponsorshipCreatedEffectResponse(string sponsor)
        {
            Sponsor = sponsor;
        }
    }
}