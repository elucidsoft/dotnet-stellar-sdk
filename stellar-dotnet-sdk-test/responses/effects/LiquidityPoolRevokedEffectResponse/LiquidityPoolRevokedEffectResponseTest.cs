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
    public class LiquidityPoolRevokedEffectResponseTest
    {
        [TestMethod]
        public void TestCreation()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolRevokedEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var response = (LiquidityPoolRevokedEffectResponse)instance;
            var clone = new LiquidityPoolRevokedEffectResponse(response.LiquidityPool, response.ReservesRevoked, response.SharesRevoked);

            Assert.AreEqual(response.LiquidityPool, clone.LiquidityPool);
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolRevokedEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserialize()
        {
            var json = File.ReadAllText(Path.Combine("responses/effects/", "LiquidityPoolRevokedEffectResponse/Data.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertData(back);
        }

        public static void AssertData(EffectResponse instance)
        {
            Assert.IsTrue(instance is LiquidityPoolRevokedEffectResponse);
            var effect = (LiquidityPoolRevokedEffectResponse)instance;

            Assert.AreEqual(effect.AccountMuxedID, 1278881);

            Assert.AreEqual(effect.LiquidityPool.ID.ToString(), "4f7f29db33ead1a38c2edf17aa0416c369c207ca081de5c686c050c1ad320385");

            Assert.AreEqual(effect.LiquidityPool.FeeBP, 30);
            Assert.AreEqual(effect.LiquidityPool.TotalTrustlines, 1);
            Assert.AreEqual(effect.LiquidityPool.TotalShares, "0.0000000");

            Assert.AreEqual(effect.LiquidityPool.Reserves[0].Asset.CanonicalName(), "native");
            Assert.AreEqual(effect.LiquidityPool.Reserves[0].Amount, "0.0000000");

            Assert.AreEqual(effect.LiquidityPool.Reserves[1].Asset.CanonicalName(), "TEST:GC2262FQJAHVJSYWI6XEVQEH5CLPYCVSOLQHCDHNSKVWHTKYEZNAQS25");
            Assert.AreEqual(effect.LiquidityPool.Reserves[1].Amount, "0.0000000");

            Assert.AreEqual(effect.ReservesRevoked.Asset.CanonicalName(), "TEST:GC2262FQJAHVJSYWI6XEVQEH5CLPYCVSOLQHCDHNSKVWHTKYEZNAQS25");
            Assert.AreEqual(effect.ReservesRevoked.Amount, "1500.0000000");
            Assert.AreEqual(effect.ReservesRevoked.ClaimableBalanceID, "00000000836f572dd43b76853df6c88ca1b89394b547d74de0c87334ce7f9270cb342203");

            Assert.AreEqual(effect.SharesRevoked, "100.0000000");
        }
    }
}
