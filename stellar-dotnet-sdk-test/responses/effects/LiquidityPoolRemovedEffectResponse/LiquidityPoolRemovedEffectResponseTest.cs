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
    public class LiquidityPoolRemovedEffectResponseTest
    {
        [TestMethod]
        public void TestCreation()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolRemovedEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var response = (LiquidityPoolRemovedEffectResponse)instance;
            var clone = new LiquidityPoolRemovedEffectResponse(response.LiquidityPoolID);

            Assert.AreEqual(response.LiquidityPoolID, clone.LiquidityPoolID);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolRemovedEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolRemovedEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertData(back);
        }

        public static void AssertData(EffectResponse instance)
        {
            Assert.IsTrue(instance is LiquidityPoolRemovedEffectResponse);
            var effect = (LiquidityPoolRemovedEffectResponse)instance;

            Assert.AreEqual(effect.AccountMuxedID, 1278881UL);
            Assert.AreEqual(effect.LiquidityPoolID.ToString(), "4f7f29db33ead1a38c2edf17aa0416c369c207ca081de5c686c050c1ad320385");
        }
    }
}
