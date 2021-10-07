using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.effects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.effects
{
    [TestClass]
    public class LiquidityPoolTradeEffectResponseTest
    {
        [TestMethod]
        public void TestCreation()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolTradeEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var response = (LiquidityPoolTradeEffectResponse)instance;
            var clone = new LiquidityPoolTradeEffectResponse(response.LiquidityPool, response.Sold, response.Bought);

            Assert.AreEqual(response.LiquidityPool, clone.LiquidityPool);
            Assert.AreEqual(response.Sold, clone.Sold);
            Assert.AreEqual(response.Bought, clone.Bought);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolTradeEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolTradeEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertData(back);
        }

        public static void AssertData(EffectResponse instance)
        {
            Assert.IsTrue(instance is LiquidityPoolTradeEffectResponse);
            var effect = (LiquidityPoolTradeEffectResponse)instance;

            Assert.AreEqual(effect.LiquidityPool.ID.ToString(), "4f7f29db33ead1a38c2edf17aa0416c369c207ca081de5c686c050c1ad320385");

            Assert.AreEqual(effect.Sold.Asset.CanonicalName(), "TEST:GC2262FQJAHVJSYWI6XEVQEH5CLPYCVSOLQHCDHNSKVWHTKYEZNAQS25");
            Assert.AreEqual(effect.Sold.Amount, "93.1375850");

            Assert.AreEqual(effect.Bought.Asset.CanonicalName(), "TEST2:GDQ4273UBKSHIE73RJB5KLBBM7W3ESHWA74YG7ZBXKZLKT5KZGPKKB7E");
            Assert.AreEqual(effect.Bought.Amount, "100.0000000");
        }
    }
}
