using System;
using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///
    /// </summary>
    public class AssetResponseFlags
    {
        /// <summary>
        /// The anchor must approve anyone who wants to hold this asset.
        /// </summary>
        [JsonProperty(PropertyName = "auth_required")]
        public bool AuthRequired { get; set; }

        /// <summary>
        /// The anchor can freeze the asset.
        /// </summary>
        [JsonProperty(PropertyName = "auth_revocable")]
        public bool AuthRevocable { get; set; }

        /// <summary>
        /// None of the authorization flags can be set and the issuing account can never be deleted.
        /// </summary>
        [JsonProperty(PropertyName = "auth_immutable")]
        public bool AuthImmutable { get; set; }


        [JsonConstructor]
        public AssetResponseFlags(bool authRequired, bool authRevocable, bool authImmutable)
        {
            AuthRequired = authRequired;
            AuthRevocable = authRevocable;
            AuthImmutable = authImmutable;
        }

        [Obsolete("This constructor has been deprecated. Use AssetResponseFlags(bool authRequired, bool authRevocable, bool authImmutable) instead.")]
        public AssetResponseFlags(bool authRequired, bool authRevocable) : this(authRequired, authRevocable, false)
        {

        }
    }
}