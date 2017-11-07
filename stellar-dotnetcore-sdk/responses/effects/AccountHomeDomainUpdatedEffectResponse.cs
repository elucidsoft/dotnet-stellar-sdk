using Newtonsoft.Json;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_home_domain_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountHomeDomainUpdatedEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "home_domain")]
        public string HomeDomain { get; }

        public AccountHomeDomainUpdatedEffectResponse(string homeDomain)
        {
            HomeDomain = homeDomain;
        }
    }
}