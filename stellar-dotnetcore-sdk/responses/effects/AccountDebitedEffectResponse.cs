using Newtonsoft.Json;
using stellar_dotnetcore_sdk.requests;

namespace stellar_dotnetcore_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_debited effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountDebitedEffectResponse : EffectResponse
    {
        /// <inheritdoc />
        public AccountDebitedEffectResponse(string amount, string assetType, string assetCode, string assetIssuer)
        {
            Amount = amount;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
        }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}