using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_home_domain_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountInflationDestinationUpdatedEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "home_domain")]
        public string HomeDomain { get; }

        /// <inheritdoc />
        public AccountInflationDestinationUpdatedEffectResponse(string homeDomain)
        {
            HomeDomain = homeDomain;
        }
    }
}