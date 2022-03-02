using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <inheritdoc />
    /// <summary>
    /// Represents SetTrustlineFlags operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="T:stellar_dotnetcore_sdk.requests.ClawbackOperationResponse" />
    /// <seealso cref="T:stellar_dotnetcore_sdk.Server" />
    /// </summary>
    public class SetTrustlineFlagsOperationResponse : OperationResponse
    {
        /// <param name="assetType">Asset type (native / alphanum4 / alphanum12)</param>
        /// <param name="assetCode">Asset code.</param>
        /// <param name="assetIssuer">Asset issuer.</param>
        /// <param name="amount">Amount</param>
        /// <param name="from">From account</param>
        public SetTrustlineFlagsOperationResponse(string assetType, string assetCode, string assetIssuer, string trustor, string[] setFlags, string[] clearFlags)
        {
            AssetType = assetType;
            AssetCode = assetCode;
            AssetIssuer = assetIssuer;
            Trustor = trustor;
            SetFlags = setFlags;
            ClearFlags = clearFlags;
        }

        public SetTrustlineFlagsOperationResponse()
        {

        }

        public override int TypeId => 21;

        /// <summary>
        /// Asset type (native / alphanum4 / alphanum12)
        /// </summary>
        [JsonProperty(PropertyName = "asset_type")]
        public string AssetType { get; private set; }

        /// <summary>
        /// Asset code.
        /// </summary>
        [JsonProperty(PropertyName = "asset_code")]
        public string AssetCode { get; private set; }

        /// <summary>
        /// Asset issuer.
        /// </summary>
        [JsonProperty(PropertyName = "asset_issuer")]
        public string AssetIssuer { get; private set; }

        /// <summary>
        /// Trustor account.
        /// </summary>
        [JsonProperty(PropertyName = "trustor")]
        public string Trustor { get; private set; }

        /// <summary>
        /// Indicates which flags to clear. For details about the flags, please refer to the accounts doc. The bit mask integer adds onto the
        /// existing flags of the account. This allows for setting specific bits without knowledge of existing flags.
        /// </summary>
        [JsonProperty(PropertyName = "clear_flags_s")]
        public string[] ClearFlags { get; private set; }

        /// <summary>
        /// Indicates which flags to set. For details about the flags, please refer to the accounts doc. The bit mask integer adds onto the
        /// existing flags of the account. This allows for setting specific bits without knowledge of existing flags.
        /// </summary>
        [JsonProperty(PropertyName = "set_flags_s")]
        public string[] SetFlags { get; private set; }

        /// <summary>
        /// Asset representation (Using the values of the other fields)
        /// </summary>
        public Asset Asset => Asset.CreateNonNativeAsset(AssetType, AssetIssuer, AssetCode);
    }
}
