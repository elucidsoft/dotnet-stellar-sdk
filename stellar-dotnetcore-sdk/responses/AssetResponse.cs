using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk.responses
{
    /// <summary>
    /// 
    /// </summary>
    public class AssetResponse : Response
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "_links")]
        public AssetResponseLinks Links { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "paging_token")]
        public string PagingToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "ammount")]
        public string Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "num_accounts")]
        public long NumAccounts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "flags")]
        public AssetResponseFlags Flags { get; set; }


        /// <summary>
        /// 
        /// </summary>
        protected AssetResponse()
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AssetResponseFlags
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "auth_required")]
        public bool AuthRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "auth_revocable")]
        public bool AuthRevocable { get; set; }
    }

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
    }
}
