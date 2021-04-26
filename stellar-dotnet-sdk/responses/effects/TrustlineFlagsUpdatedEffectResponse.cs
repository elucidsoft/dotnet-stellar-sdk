using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents trustline_updated effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class TrustlineFlagsUpdatedEffectResponse : EffectResponse
    {
        public override int TypeId => 26;

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        [JsonProperty(PropertyName = "trustor")]
        public string Trustor { get; private set; }

        [JsonProperty(PropertyName = "authorized_flag")]
        public bool AuthorizedFlag { get; private set; }

        [JsonProperty(PropertyName = "authorized_to_maintain_liabilities")]
        public bool AuthorizedToMaintainLiabilities { get; private set; }

        [JsonProperty(PropertyName = "clawback_enabled_flag")]
        public bool ClawbackEnabledFlag { get; private set; }

        public TrustlineFlagsUpdatedEffectResponse()
        {

        }

        public TrustlineFlagsUpdatedEffectResponse(string assetType, string assetCode, string assetIssuer, string trustor, bool authorizedFlag, bool authorizedToMaintainLiabilities, bool clawbackEnabledFlag)
        {
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            Trustor = trustor;
            AuthorizedFlag = authorizedFlag;
            AuthorizedToMaintainLiabilities = authorizedToMaintainLiabilities;
            ClawbackEnabledFlag = clawbackEnabledFlag;
        }
    }
}