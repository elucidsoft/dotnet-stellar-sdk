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
    public class LiquidityPoolDepositOperationResponseTest
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
            var json = File.ReadAllText(Path.Combine("responses/operations/LiquidityPoolDepositOperationResponse", "Data.json"));
            var instance = (LiquidityPoolDepositOperationResponse)JsonSingleton.GetInstance<OperationResponse>(json);

            AssertData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/operations/LiquidityPoolDepositOperationResponse", "Data.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = (LiquidityPoolDepositOperationResponse)JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertData(back);
        }

        public void AssertData(LiquidityPoolDepositOperationResponse instance)
        {
            Assert.AreEqual(new LiquidityPoolID("b26c0d6545349ad7f44ba758b7c705459537201583f2e524635be04aff84bc69"), instance.LiquidityPoolID);
            Assert.AreEqual("1508315204960257", instance.PagingToken);

            Assert.AreEqual("1.0000000", instance.MinPrice);
            Assert.AreEqual("100000000.0000000", instance.MaxPrice);

            Assert.AreEqual("1000.0000000", instance.ReservesMax[0].Amount);
            Assert.AreEqual(null, instance.ReservesMax[0].Asset);

            Assert.AreEqual("1.0000000", instance.ReservesMax[1].Amount);
            Assert.AreEqual(null, instance.ReservesMax[1].Asset);

            Assert.AreEqual("0.0000000", instance.ReservesDeposited[0].Amount);
            Assert.AreEqual("0.0000000", instance.ReservesDeposited[1].Amount);

            Assert.AreEqual("0.0000000", instance.SharesReceived);
        }
    }
}
