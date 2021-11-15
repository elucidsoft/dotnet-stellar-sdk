using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///     Represents account flags.
    /// </summary>
    public class Flags
    {
        public Flags(bool authRequired, bool authRevocable, bool authImmutable, bool authClawback)
        {
            AuthRequired = authRequired;
            AuthRevocable = authRevocable;
            AuthImmutable = authImmutable;
            AuthClawback = authClawback;
        }

        /// <summary>
        /// This account must approve anyone who wants to hold its asset.
        /// </summary>
        [JsonProperty(PropertyName = "auth_required")]
        public bool AuthRequired { get; private set; }

        /// <summary>
        /// This account can set the authorize flag of an existing trustline to freeze the assets held by an asset holder.
        /// </summary>
        [JsonProperty(PropertyName = "auth_revocable")]
        public bool AuthRevocable { get; private set; }

        /// <summary>
        /// This account cannot change any of the authorization flags.
        /// </summary>
        [JsonProperty(PropertyName = "auth_immutable")]
        public bool AuthImmutable { get; private set; }

        /// <summary>
        /// This account can unilaterally take away any portion of its issued asset(s) from any asset holders.
        /// </summary>
        [JsonProperty(PropertyName = "auth_clawback_enabled")]
        public bool AuthClawback { get; private set; }
    }
}