using Newtonsoft.Json;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountCreatedEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "starting_balance")]
        public string StartingBalance { get; }

        public AccountCreatedEffectResponse(string startingBalance)
        {
            StartingBalance = startingBalance;
        }
    }
}