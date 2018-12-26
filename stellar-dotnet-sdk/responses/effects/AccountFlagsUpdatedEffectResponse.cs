using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_flags_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountFlagsUpdatedEffectResponse : EffectResponse
    {
        public AccountFlagsUpdatedEffectResponse()
        {

        }

        /// <inheritdoc />
        public AccountFlagsUpdatedEffectResponse(bool authRequiredFlag, bool authRevokableFlag)
        {
            AuthRequiredFlag = authRequiredFlag;
            AuthRevokableFlag = authRevokableFlag;
        }

        public override int TypeId => 6;

        [JsonProperty(PropertyName = "auth_required_flag")]
        public bool AuthRequiredFlag { get; private set; }

        [JsonProperty(PropertyName = "auth_revokable_flag")]
        public bool AuthRevokableFlag { get; private set; }
    }
}