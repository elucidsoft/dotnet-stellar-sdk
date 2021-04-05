using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <inheritdoc />
    /// <summary>
    /// Represents Clawback operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="T:stellar_dotnetcore_sdk.requests.ClawbackOperationResponse" />
    /// <seealso cref="T:stellar_dotnetcore_sdk.Server" />
    /// </summary>
    public class ClawbackClaimableBalanceOperationResponse : OperationResponse
    {
        /// <summary>
        /// Updates the “authorized” flag of an existing trust line this is called by the issuer of the asset.
        /// </summary>
        /// <param name="balanceID">Asset type (native / alphanum4 / alphanum12)</param>
        public ClawbackClaimableBalanceOperationResponse(string balanceID)
        {
            BalanceID = balanceID;
        }

        public ClawbackClaimableBalanceOperationResponse()
        {

        }

        public override int TypeId => 20;

        /// <summary>
        /// Amount
        /// </summary>
        [JsonProperty(PropertyName = "balance_id")]
        public string BalanceID { get; private set; }
    }
}
