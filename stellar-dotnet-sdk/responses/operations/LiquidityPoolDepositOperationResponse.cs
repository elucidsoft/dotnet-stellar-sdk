using Newtonsoft.Json;
using System.Collections.Generic;

namespace stellar_dotnet_sdk.responses.operations
{
    public class LiquidityPoolDepositOperationResponse : OperationResponse
    {
        public override int TypeId => 22;

        [JsonProperty("liquidity_pool_id")]
        public LiquidityPoolID LiquidityPoolID { get; set; }

        [JsonProperty("reserves_max")]
        public List<Reserve> ReservesMax { get; set; }

        [JsonProperty("min_price")]
        public string MinPrice { get; set; }

        [JsonProperty("max_price")]
        public string MaxPrice { get; set; }

        [JsonProperty("reserves_deposited")]
        public List<Reserve> ReservesDeposited { get; set; }

        [JsonProperty("shares_received")]
        public string SharesReceived { get; set; }

        public LiquidityPoolDepositOperationResponse()
        {

        }
    }
}
