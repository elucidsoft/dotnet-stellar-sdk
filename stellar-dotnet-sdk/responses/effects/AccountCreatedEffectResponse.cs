using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_created effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountCreatedEffectResponse : EffectResponse
    {
        [JsonProperty(PropertyName = "starting_balance")]
        public string StartingBalance { get; }

        /// <inheritdoc />
        public AccountCreatedEffectResponse(string startingBalance)
        {
            StartingBalance = startingBalance;
        }
    }
}