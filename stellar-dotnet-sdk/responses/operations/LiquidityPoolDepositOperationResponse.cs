using Newtonsoft.Json;

namespace stellar_dotnet_sdk.responses.operations
{
    /// <summary>
    /// Represents AccountMerge operation response.
    /// See: https://www.stellar.org/developers/horizon/reference/resources/operation.html
    /// <seealso cref="requests.OperationsRequestBuilder"/>
    /// <seealso cref="Server"/>
    /// </summary>
    public class LiquidityPoolDepositOperationResponse : OperationResponse
    {
        public override int TypeId => 22;

        [JsonProperty(PropertyName = "liquidity_pool_id")]
        public string LiquidityPoolID { get; private set; }

        [JsonProperty(PropertyName = "max_amount_a")]
        public string MaxAmountA { get; private set; }

        [JsonProperty(PropertyName = "max_amount_b")]
        public string MaxAmountB { get; private set; }

        [JsonProperty(PropertyName = "min_price")]
        public string MinPrice { get; private set; }

        [JsonProperty(PropertyName = "max_price")]
        public string MaxPrice { get; private set; }

        public LiquidityPoolDepositOperationResponse()
        {

        }


        public LiquidityPoolDepositOperationResponse(string liquidityPoolID, string maxAmountA, string maxAmountB, string minPrice, string maxPrice)
        {
            LiquidityPoolID = liquidityPoolID;
            MaxAmountA = maxAmountA;
            MaxAmountB = maxAmountB;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
    }
}
