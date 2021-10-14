using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.operations
{
    [TestClass]
    public class LiquidityPoolWithdrawOperationResponseTest
    {
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

        //Allow Trust
        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/operations/LiquidityPoolWithdrawOperationResponse", "Data.json"));
            var instance = (LiquidityPoolWithdrawOperationResponse)JsonSingleton.GetInstance<OperationResponse>(json);

            AssertData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/operations/LiquidityPoolWithdrawOperationResponse", "Data.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = (LiquidityPoolWithdrawOperationResponse)JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertData(back);
        }

        public void AssertData(LiquidityPoolWithdrawOperationResponse instance)
        {
            Assert.AreEqual(new LiquidityPoolID("b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69"), instance.LiquidityPoolID);
            Assert.AreEqual("1508641622462465", instance.PagingToken);

            Assert.AreEqual("0.0000000", instance.ReservesMin[0].Amount);
            Assert.AreEqual("native", instance.ReservesMin[0].Asset.CanonicalName());

            Assert.AreEqual("0.0000000", instance.ReservesMin[1].Amount);
            Assert.AreEqual("USDC:GAKMOAANQHJKF5735OYVSQZL6KC3VMFL4LP4ZYY2LWK256TSUG45IEFB", instance.ReservesMin[1].Asset.CanonicalName());

            Assert.AreEqual("1000.0000000", instance.Shares);

            Assert.AreEqual("1000.0000000", instance.ReservesReceived[0].Amount);
            Assert.AreEqual("native", instance.ReservesReceived[0].Asset.CanonicalName());

            Assert.AreEqual("1000.0000000", instance.ReservesReceived[1].Amount);
            Assert.AreEqual("USDC:GAKMOAANQHJKF5735OYVSQZL6KC3VMFL4LP4ZYY2LWK256TSUG45IEFB", instance.ReservesReceived[1].Asset.CanonicalName());
        }
    }
}
