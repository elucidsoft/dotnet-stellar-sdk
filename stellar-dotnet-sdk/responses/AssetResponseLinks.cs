using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses
{
    /// <summary>
    ///
    /// </summary>
    public class AssetResponseLinks
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "toml")]
        public Link Toml { get; set; }

        public AssetResponseLinks(Link<AssetResponse> toml)
        {
            Toml = toml;
        }
    }
}