using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class LiquidityPoolWithdrawOperationResponse : OperationResponse
    {
        public override int TypeId => 23;

        [JsonProperty(PropertyName = "liquidity_pool_id")]
        public string LiquidityPoolID { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; private set; }

        [JsonProperty(PropertyName = "min_amount_a")]
        public string MinAmountA { get; private set; }

        [JsonProperty(PropertyName = "min_amount_b")]
        public string MinAmountB { get; private set; }

        public LiquidityPoolWithdrawOperationResponse()
        {

        }


        public LiquidityPoolWithdrawOperationResponse(string liquidityPoolID, string amount, string minAmountA, string minAmountB)
        {
            LiquidityPoolID = liquidityPoolID;
            Amount = amount;
            MinAmountA = minAmountA;
            MinAmountB = minAmountB;
        }
    }
}
