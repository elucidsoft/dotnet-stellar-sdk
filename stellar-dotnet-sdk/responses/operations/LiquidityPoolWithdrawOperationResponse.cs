using Newtonsoft.Json;
using System.Collections.Generic;

namespace stellar_dotnet_sdk.responses.operations
{
    public class LiquidityPoolWithdrawOperationResponse : OperationResponse
    {
        public override int TypeId => 23;

        [JsonProperty("liquidity_pool_id")]
        public LiquidityPoolID LiquidityPoolID { get; set; }

        [JsonProperty("reserves_min")]
        public List<Reserve> ReservesMin { get; set; }

        [JsonProperty("reserves_received")]
        public List<Reserve> ReservesReceived { get; set; }

        [JsonProperty("shares")]
        public string Shares { get; set; }

        public LiquidityPoolWithdrawOperationResponse()
        {

        }
    }
}
