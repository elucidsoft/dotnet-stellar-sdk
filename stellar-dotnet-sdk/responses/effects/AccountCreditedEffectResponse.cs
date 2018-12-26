using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.effects
{
    /// <summary>
    ///     Represents account_credited effect response.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/effect.html
    ///     <seealso cref="requests.EffectsRequestBuilder" />
    ///     <seealso cref="Server" />
    /// </summary>
    public class AccountCreditedEffectResponse : EffectResponse
    {
        public AccountCreditedEffectResponse()
        {

        }

        /// <inheritdoc />
        public AccountCreditedEffectResponse(string amount, string assetType, string assetCode, string assetIssuer)
        {
            Amount = amount;
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
        }

        public override int TypeId => 2;

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}