using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_sponsorship_removed effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountSponsorshipdRemovedEffectResponse : EffectResponse
    {
        public override int TypeId => 62;

        [JsonProperty(PropertyName = "former_sponsor")]
        public string FormerSponsor { get; private set; }

        public AccountSponsorshipdRemovedEffectResponse()
        {

        }

        public AccountSponsorshipdRemovedEffectResponse(string formerSponsor)
        {
            FormerSponsor = formerSponsor;
        }
    }
}