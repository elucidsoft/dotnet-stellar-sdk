using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <inheritdoc />
    /// <summary>
    ///     This operation sets the options for an account.
    ///     See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    ///     <seealso cref="T:stellar_dotnetcore_sdk.requests.OperationsRequestBuilder" />
    ///     <seealso cref="T:stellar_dotnetcore_sdk.Server" />
    /// </summary>
    public class SetOptionsOperationResponse : OperationResponse
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowThreshold">A number from 0-255 representing the threshold this account sets on all operations it performs that have a low threshold.</param>
        /// <param name="medThreshold">A number from 0-255 representing the threshold this account sets on all operations it performs that have a medium threshold.</param>
        /// <param name="highThreshold">A number from 0-255 representing the threshold this account sets on all operations it performs that have a high threshold.</param>
        /// <param name="inflationDestination">Account of the inflation destination.</param>
        /// <param name="homeDomain">Sets the home domain of an account. </param>
        /// <param name="signerKey">Add, update, or remove a signer from an account.</param>
        /// <param name="signerWeight">Weight of the signer.</param>
        /// <param name="masterKeyWeight">Weight of the master key.</param>
        /// <param name="clearFlags">Indicates which flags to clear. For details about the flags, please refer to the accounts doc. The bit mask integer subtracts 
        /// from the existing flags of the account. This allows for setting specific bits without knowledge of existing flags.</param>
        /// <param name="setFlags">Indicates which flags to set. For details about the flags, please refer to the accounts doc. The bit mask integer adds onto the
        /// existing flags of the account. This allows for setting specific bits without knowledge of existing flags.</param>
        public SetOptionsOperationResponse(int lowThreshold, int medThreshold, int highThreshold, KeyPair inflationDestination,
            string homeDomain, string signerKey, int signerWeight, int masterKeyWeight, string[] clearFlags, string[] setFlags)
        {
            LowThreshold = lowThreshold;
            MedThreshold = medThreshold;
            HighThreshold = highThreshold;
            InflationDestination = inflationDestination;
            HomeDomain = homeDomain;
            SignerKey = signerKey;
            SignerWeight = signerWeight;
            MasterKeyWeight = masterKeyWeight;
            ClearFlags = clearFlags;
            SetFlags = setFlags;
        }

        /// <summary>
        /// A number from 0-255 representing the threshold this account sets on all operations it performs that have a low threshold.
        /// </summary>
        [JsonProperty(PropertyName = "low_threshold")]
        public int LowThreshold { get; }

        /// <summary>
        /// A number from 0-255 representing the threshold this account sets on all operations it performs that have a medium threshold.
        /// </summary>
        [JsonProperty(PropertyName = "med_threshold")]
        public int MedThreshold { get; }

        /// <summary>
        /// A number from 0-255 representing the threshold this account sets on all operations it performs that have a high threshold.
        /// </summary>
        [JsonProperty(PropertyName = "high_threshold")]
        public int HighThreshold { get; }

        /// <summary>
        /// Account of the inflation destination.
        /// </summary>
        [JsonProperty(PropertyName = "inflation_dest")]
        public KeyPair InflationDestination { get; }

        /// <summary>
        /// Gets the home domain of an account.
        /// </summary>
        [JsonProperty(PropertyName = "home_domain")]
        public string HomeDomain { get; }

        /// <summary>
        /// A signer from an account.
        /// </summary>
        [JsonProperty(PropertyName = "signer_key")]
        public string SignerKey { get; }

        /// <summary>
        /// Weight of the signer.
        /// </summary>
        [JsonProperty(PropertyName = "signer_weight")]
        public int SignerWeight { get; }

        /// <summary>
        /// Weight of the master key.
        /// </summary>
        [JsonProperty(PropertyName = "master_key_weight")]
        public int MasterKeyWeight { get; }

        /// <summary>
        /// Indicates which flags to clear. For details about the flags, please refer to the accounts doc. The bit mask integer subtracts 
        /// from the existing flags of the account. This allows for setting specific bits without knowledge of existing flags.
        /// </summary>
        [JsonProperty(PropertyName = "clear_flags_s")]
        public string[] ClearFlags { get; }

        /// <summary>
        /// Indicates which flags to set. For details about the flags, please refer to the accounts doc. The bit mask integer adds onto the
        /// existing flags of the account. This allows for setting specific bits without knowledge of existing flags.
        /// </summary>
        [JsonProperty(PropertyName = "set_flags_s")]
        public string[] SetFlags { get; }
    }
}