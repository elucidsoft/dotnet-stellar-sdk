using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///     Represents account flags.
    /// </summary>
    public class Flags
    {
        public Flags(bool authRequired, bool authRevocable, bool authImmutable)
        {
            AuthRequired = authRequired;
            AuthRevocable = authRevocable;
            AuthImmutable = authImmutable;
        }

        [JsonProperty(PropertyName = "auth_required")]
        public bool AuthRequired { get; private set; }

        [JsonProperty(PropertyName = "auth_revocable")]
        public bool AuthRevocable { get; private set; }

        [JsonProperty(PropertyName = "auth_immutable")]
        public bool AuthImmutable { get; private set; }
    }
}