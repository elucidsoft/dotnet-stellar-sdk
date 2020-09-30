using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.effects;
using System;
using System.IO;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class EffectDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeAccountCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountCreatedData(back);
        }

        public static void AssertAccountCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreatedEffectResponse);
            var effect = (AccountCreatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ");
            Assert.AreEqual(effect.StartingBalance, "30.0");
            Assert.AreEqual(effect.PagingToken, "65571265847297-1");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265847297");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265847297-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265847297-1");
            
            var back = new AccountCreatedEffectResponse(effect.StartingBalance);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeAccountRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountRemovedData(back);
        }

        private static void AssertAccountRemovedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountRemovedEffectResponse);
            var effect = (AccountRemovedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265847297");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265847297-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265847297-1");
        }

        [TestMethod]
        public void TestDeserializeAccountCreditedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountCredited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountCreditedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountCreditedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountCredited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountCreditedData(back);
        }

        private static void AssertAccountCreditedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreditedEffectResponse);
            var effect = (AccountCreditedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GDLGTRIBFH24364GPWPUS45GUFC2GU4ARPGWTXVCPLGTUHX3IOS3ON47");
            Assert.AreEqual(effect.Asset, new AssetTypeNative());
            Assert.AreEqual(effect.Amount, "1000.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/13563506724865");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=13563506724865-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=13563506724865-1");
            
            var back = new AccountCreditedEffectResponse(effect.Amount, effect.AssetType, effect.AssetCode, effect.AssetIssuer);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeAccountDebitedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountDebited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountDebitedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountDebitedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountDebited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountDebitedData(back);
        }

        private static void AssertAccountDebitedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountDebitedEffectResponse);
            var effect = (AccountDebitedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H");
            Assert.AreEqual(effect.Asset, new AssetTypeNative());
            Assert.AreEqual(effect.Amount, "30.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265843201");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265843201-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265843201-2");
            
            var back = new AccountDebitedEffectResponse(effect.Amount, effect.AssetType, effect.AssetCode, effect.AssetIssuer);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeAccountThresholdsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountThresholdsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountThresholdsUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountThresholdsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountThresholdsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountThresholdsUpdatedData(back);
        }

        private static void AssertAccountThresholdsUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountThresholdsUpdatedEffectResponse);
            var effect = (AccountThresholdsUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.LowThreshold, 2);
            Assert.AreEqual(effect.MedThreshold, 3);
            Assert.AreEqual(effect.HighThreshold, 4);

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
            
            var back = new AccountThresholdsUpdatedEffectResponse(effect.LowThreshold, effect.MedThreshold, effect.HighThreshold);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeAccountHomeDomainUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountHomeDomainUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountHomeDomainUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountHomeDomainUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountHomeDomainUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountHomeDomainUpdatedData(back);
        }

        private static void AssertAccountHomeDomainUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountHomeDomainUpdatedEffectResponse);
            var effect = (AccountHomeDomainUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.HomeDomain, "stellar.org");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
            
            var back = new AccountHomeDomainUpdatedEffectResponse(effect.HomeDomain);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeAccountFlagsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountFlagsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountFlagsUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountFlagsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountFlagsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountFlagsUpdatedData(back);
        }

        private static void AssertAccountFlagsUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountFlagsUpdatedEffectResponse);
            var effect = (AccountFlagsUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AuthRequiredFlag, false);
            Assert.AreEqual(effect.AuthRevokableFlag, true);

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
            
            var back = new AccountFlagsUpdatedEffectResponse(effect.AuthRequiredFlag, effect.AuthRevokableFlag);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeSignerCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSignerCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSignerCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSignerCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSignerCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSignerCreatedData(back);
        }

        private static void AssertSignerCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerCreatedEffectResponse);
            var effect = (SignerCreatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GB24LPGAHYTWRYOXIDKXLI55SBRWW42T3TZKDAAW3BOJX4ADVIATFTLU");
            Assert.AreEqual(effect.Weight, 1);
            Assert.AreEqual(effect.PublicKey, "GB24LPGAHYTWRYOXIDKXLI55SBRWW42T3TZKDAAW3BOJX4ADVIATFTLU");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265859585");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265859585-3");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265859585-3");
            
            var back = new SignerCreatedEffectResponse(effect.Weight, effect.PublicKey);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeSignerRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSignerRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSignerRemoveData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSignerRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSignerRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSignerRemoveData(back);
        }

        private static void AssertSignerRemoveData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerRemovedEffectResponse);
            var effect = (SignerRemovedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GCFKT6BN2FEASCEVDNHEC4LLFT2KLUUPEMKM4OJPEJ65H2AEZ7IH4RV6");
            Assert.AreEqual(effect.Weight, 0);
            Assert.AreEqual(effect.PublicKey, "GCFKT6BN2FEASCEVDNHEC4LLFT2KLUUPEMKM4OJPEJ65H2AEZ7IH4RV6");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/43658342567940");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=43658342567940-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=43658342567940-2");
            
            var back = new SignerRemovedEffectResponse(effect.Weight, effect.PublicKey);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeSignerUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSignerUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSignerUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSignerUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSignerUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSignerUpdatedData(back);
        }

        private static void AssertSignerUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerUpdatedEffectResponse);
            var effect = (SignerUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Weight, 2);
            Assert.AreEqual(effect.PublicKey, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
            
            var back = new SignerUpdatedEffectResponse(effect.Weight, effect.PublicKey);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeTrustlineCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineCreatedData(back);
        }

        private static void AssertTrustlineCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineCreatedEffectResponse);
            var effect = (TrustlineCreatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset, Asset.CreateNonNativeAsset("EUR", "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA"));
            Assert.AreEqual(effect.Limit, "1000.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");

            var back = new TrustlineCreatedEffectResponse(effect.Limit, effect.AssetType, effect.AssetCode,
                effect.AssetIssuer);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeTrustlineRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineRemovedData(back);
        }

        private static void AssertTrustlineRemovedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineRemovedEffectResponse);
            var effect = (TrustlineRemovedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset, Asset.CreateNonNativeAsset("EUR", "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA"));
            Assert.AreEqual(effect.Limit, "0.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
            
            var back = new TrustlineRemovedEffectResponse(effect.Limit, effect.AssetType, effect.AssetCode, effect.AssetIssuer);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeTrustlineUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineUpdatedData(back);
        }

        private static void AssertTrustlineUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineUpdatedEffectResponse);
            var effect = (TrustlineUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset, Asset.CreateNonNativeAsset("TESTTEST", "GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA"));
            Assert.AreEqual(effect.Limit, "100.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
            
            var back = new TrustlineUpdatedEffectResponse(effect.Limit, effect.AssetType, effect.AssetCode, effect.AssetIssuer);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeTrustlineAuthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineAuthorizedData(instance);
        }

        [TestMethod]
        public void TestDeserializeTrustlineAuthorizedToMaintainLiabilitiesEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineAuthorizedToMaintainLiabilitiesEffect.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineAuthorizedToMaintainLiabilitiesEffect(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineAuthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineAuthorizedData(back);
        }

        private static void AssertTrustlineAuthorizedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineAuthorizedEffectResponse);
            var effect = (TrustlineAuthorizedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, "credit_alphanum12");
            Assert.AreEqual(effect.AssetCode, "TESTTEST");
            Assert.AreEqual(effect.Trustor, "GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
            
            var back = new TrustlineAuthorizedEffectResponse(effect.Trustor, effect.AssetType, effect.AssetCode);
            Assert.IsNotNull(back);
        }

        private static void AssertTrustlineAuthorizedToMaintainLiabilitiesEffect(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineAuthorizedToMaintainLiabilitiesEffectResponse);
            var effect = (TrustlineAuthorizedToMaintainLiabilitiesEffectResponse)instance;

            TrustlineAuthorizationResponse trustline = new TrustlineAuthorizationResponse("GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR", "credit_alphanum12", "TESTTEST");

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, trustline.AssetType);
            Assert.AreEqual(effect.AssetCode, trustline.AssetCode);
            Assert.AreEqual(effect.Trustor, trustline.Trustor);

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
            
            var back = new TrustlineAuthorizedToMaintainLiabilitiesEffectResponse(effect.Trustor, effect.AssetType, effect.AssetCode);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeTrustlineDeauthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineDeAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineDeauthorizedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineDeauthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrustlineDeAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineDeauthorizedData(back);
        }

        private static void AssertTrustlineDeauthorizedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineDeauthorizedEffectResponse);
            var effect = (TrustlineDeauthorizedEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, "credit_alphanum4");
            Assert.AreEqual(effect.AssetCode, "EUR");
            Assert.AreEqual(effect.Trustor, "GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
            
            var back = new TrustlineDeauthorizedEffectResponse(effect.Trustor, effect.AssetType, effect.AssetCode);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeTradeEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrade.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTradeData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTradeEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTrade.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTradeData(back);
        }

        //Before Horizon 1.0.0 the OfferID in the json was a long.
        [TestMethod]
        public void TestDeserializeTradeEffectPre100()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTradePre100.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTradeData(instance);
        }

        //Before Horizon 1.0.0 the OfferID in the json was a long.
        [TestMethod]
        public void TestSerializeDeserializeTradeEffectPre100()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectTradePre100.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTradeData(back);
        }

        private static void AssertTradeData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TradeEffectResponse);
            var effect = (TradeEffectResponse)instance;

            Assert.AreEqual(effect.Account, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Seller, "GCVHDLN6EHZBYW2M3BQIY32C23E4GPIRZZDBNF2Q73DAZ5VJDRGSMYRB");
            Assert.AreEqual(effect.OfferId, "1");
            Assert.AreEqual(effect.SoldAmount, "1000.0");
            Assert.AreEqual(effect.SoldAsset, Asset.CreateNonNativeAsset("EUR", "GCWVFBJ24754I5GXG4JOEB72GJCL3MKWC7VAEYWKGQHPVH3ENPNBSKWS"));
            Assert.AreEqual(effect.BoughtAmount, "60.0");
            Assert.AreEqual(effect.BoughtAsset, Asset.CreateNonNativeAsset("TESTTEST", "GAHXPUDP3AK6F2QQM4FIRBGPNGKLRDDSTQCVKEXXKKRHJZUUQ23D5BU7"));

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");

            var back = new TradeEffectResponse(effect.Seller, effect.OfferId, effect.SoldAmount, effect.SoldAssetType,
                effect.SoldAssetCode, effect.SoldAssetIssuer, effect.BoughtAmount, effect.BoughtAssetType,
                effect.BoughtAssetCode, effect.BoughtAssetType);
            Assert.IsNotNull(back);
        }

        [TestMethod]
        public void TestDeserializeAccountInflationUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountInflationUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountInflationUpdated(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountInflationUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectAccountInflationUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountInflationUpdated(back);
        }

        private static void AssertAccountInflationUpdated(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountInflationDestinationUpdatedEffectResponse);
            var effect = (AccountInflationDestinationUpdatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectDataCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertDataCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeDataCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectDataCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertDataCreatedData(back);
        }

        private static void AssertDataCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataCreatedEffectResponse);
            var effect = (DataCreatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectDataRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertDataRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeDataRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectDataRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertDataRemovedData(back);
        }

        private static void AssertDataRemovedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataRemovedEffectResponse);
            var effect = (DataRemovedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectDataUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertDataUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeDataUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectDataUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertDataUpdatedData(back);
        }

        private static void AssertDataUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataUpdatedEffectResponse);
            var effect = (DataUpdatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestUnknownEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectUnknown.json"));
            Assert.ThrowsException<JsonSerializationException>(() => JsonSingleton.GetInstance<EffectResponse>(json));
        }

        [TestMethod]
        public void TestDeserializeSequenceBumpedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSequenceBumped.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSequenceBumpedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSequenceBumpedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectSequenceBumped.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSequenceBumpedData(back);
        }

        private static void AssertSequenceBumpedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is SequenceBumpedEffectResponse);
            var effect = (SequenceBumpedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
            Assert.AreEqual(79473726952833048L, effect.NewSequence);
        }

        [TestMethod]
        public void TestDeserializeOfferCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectOfferCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertOfferCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeOfferCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectOfferCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertOfferCreatedData(back);
        }

        private static void AssertOfferCreatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is OfferCreatedEffectResponse);
            var effect = (OfferCreatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeOfferRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectOfferRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertOfferRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeOfferRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectOfferRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertOfferRemovedData(back);
        }

        private static void AssertOfferRemovedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is OfferRemovedEffectResponse);
            var effect = (OfferRemovedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeOfferUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectOfferUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertOfferUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeOfferUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects", "effectOfferUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertOfferUpdatedData(back);
        }

        private static void AssertOfferUpdatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is OfferUpdatedEffectResponse);
            var effect = (OfferUpdatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        //Account Sponsorship Created
        [TestMethod]
        public void TestSerializationAccountSponsorshipCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/accountSponsorship", "accountSponsorshipCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountSponsorshipCreatedData(back);
        }

        private static void AssertAccountSponsorshipCreatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is AccountSponsorshipCreatedEffectResponse);
            var effect = (AccountSponsorshipCreatedEffectResponse)instance;

            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.Sponsor);
            
            var back = new AccountSponsorshipCreatedEffectResponse(effect.Sponsor);
            Assert.IsNotNull(back);
        }

        //Account Sponsorship Removed
        [TestMethod]
        public void TestSerializationAccountSponsorshipRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/accountSponsorship", "accountSponsorshipRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountSponsorshipRemovedData(back);
        }

        private static void AssertAccountSponsorshipRemovedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is AccountSponsorshipRemovedEffectResponse);
            var effect = (AccountSponsorshipRemovedEffectResponse)instance;

            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.FormerSponsor);
            
            var back = new AccountSponsorshipRemovedEffectResponse(effect.FormerSponsor);
            Assert.IsNotNull(back);
        }


        //Account Sponsorship Updated
        [TestMethod]
        public void TestSerializationAccountSponsorshipUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/accountSponsorship", "accountSponsorshipUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertAccountSponsorshipUpdatedData(back);
        }

        private static void AssertAccountSponsorshipUpdatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is AccountSponsorshipUpdatedEffectResponse);
            var effect = (AccountSponsorshipUpdatedEffectResponse)instance;

            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.FormerSponsor);
            Assert.AreEqual("GBVFLWXYCIGPO3455XVFIKHS66FCT5AI64ZARKS7QJN4NF7K5FOXTJNL", effect.NewSponsor);
            
            var back = new AccountSponsorshipUpdatedEffectResponse(effect.FormerSponsor, effect.NewSponsor);
            Assert.IsNotNull(back);
        }

        //Claimable Balance Claimant Created
        [TestMethod]
        public void TestSerializationClaimableBalanceClaimantCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/claimableBalance", "claimableBalanceClaimantCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertClaimableBalanceClaimantCreatedEffect(back);
        }

        private static void AssertClaimableBalanceClaimantCreatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is ClaimableBalanceClaimantCreatedEffectResponse);
            var effect = (ClaimableBalanceClaimantCreatedEffectResponse)instance;

            Assert.AreEqual("native", effect.Asset);
            Assert.AreEqual("00000000be7e37b24927c095e2292d5d0e6db8b0f2dbeb1355847c7fccb458cbdd61bfd0", effect.BalanceID);
            Assert.AreEqual("1.0000000", effect.Amount);
            Assert.IsNotNull(effect.Predicate.ToClaimPredicate());
            
            var back = new ClaimableBalanceClaimantCreatedEffectResponse(effect.Asset, effect.BalanceID, effect.Amount, effect.Predicate);
            Assert.IsNotNull(back);
        }


        // Claimable Balance Claimed
        [TestMethod]
        public void TestSerializationClaimableBalanceClaimedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/claimableBalance", "claimableBalanceClaimed.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertClaimableBalanceClaimedEffect(back);
        }

        private static void AssertClaimableBalanceClaimedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is ClaimableBalanceClaimedEffectResponse);
            var effect = (ClaimableBalanceClaimedEffectResponse)instance;

            Assert.AreEqual("native", effect.Asset);
            Assert.AreEqual("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7", effect.BalanceID);
            Assert.AreEqual("1.0000000", effect.Amount);
            
            var back = new ClaimableBalanceClaimedEffectResponse(effect.Asset, effect.BalanceID, effect.Amount);
            Assert.IsNotNull(back);
        }

        // Claimable Balance Created
        [TestMethod]
        public void TestSerializationClaimableBalanceCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/claimableBalance", "claimableBalanceCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertClaimableBalanceCreatedEffect(back);
        }

        private static void AssertClaimableBalanceCreatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is ClaimableBalanceCreatedEffectResponse);
            var effect = (ClaimableBalanceCreatedEffectResponse)instance;

            Assert.AreEqual("native", effect.Asset);
            Assert.AreEqual("00000000be7e37b24927c095e2292d5d0e6db8b0f2dbeb1355847c7fccb458cbdd61bfd0", effect.BalanceID);
            Assert.AreEqual("1.0000000", effect.Amount);
            
            var back = new ClaimableBalanceCreatedEffectResponse(effect.Asset, effect.BalanceID, effect.Amount);
            Assert.IsNotNull(back);
        }

        // Claimable Balance Sponsorship Created
        [TestMethod]
        public void TestSerializationClaimableBalanceSponsorshipCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/claimableBalance", "claimableBalanceSponsorshipCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertClaimableBalanceSponsorshipCreatedEffect(back);
        }

        private static void AssertClaimableBalanceSponsorshipCreatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is ClaimableBalanceSponsorshipCreatedEffectResponse);
            var effect = (ClaimableBalanceSponsorshipCreatedEffectResponse)instance;

            Assert.AreEqual("00000000be7e37b24927c095e2292d5d0e6db8b0f2dbeb1355847c7fccb458cbdd61bfd0", effect.BalanceID);
            Assert.AreEqual("GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", effect.Sponsor);
            
            var back = new ClaimableBalanceSponsorshipCreatedEffectResponse(effect.BalanceID, effect.Sponsor);
            Assert.IsNotNull(back);
        }

        // Claimable Balance Sponsorship Removed
        [TestMethod]
        public void TestSerializationClaimableBalanceSponsorshipRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/claimableBalance", "claimableBalanceSponsorshipRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertClaimableBalanceSponsorshipRemovedEffect(back);
        }

        private static void AssertClaimableBalanceSponsorshipRemovedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is ClaimableBalanceSponsorshipRemovedEffectResponse);
            var effect = (ClaimableBalanceSponsorshipRemovedEffectResponse)instance;

            Assert.AreEqual("00000000be7e37b24927c095e2292d5d0e6db8b0f2dbeb1355847c7fccb458cbdd61bfd0", effect.BalanceID);
            Assert.AreEqual("GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", effect.FormerSponsor);
            
            var back = new ClaimableBalanceSponsorshipRemovedEffectResponse(effect.BalanceID, effect.FormerSponsor);
            Assert.IsNotNull(back);
        }

        // Claimable Balance Sponsorship Updated
        [TestMethod]
        public void TestSerializationClaimableBalanceSponsorshipUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/claimableBalance", "claimableBalanceSponsorshipUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertClaimableBalanceSponsorshipUpdatedEffect(back);
        }

        private static void AssertClaimableBalanceSponsorshipUpdatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is ClaimableBalanceSponsorshipUpdatedEffectResponse);
            var effect = (ClaimableBalanceSponsorshipUpdatedEffectResponse)instance;

            Assert.AreEqual("00000000526674017c3cf392614b3f2f500230affd58c7c364625c350c61058fbeacbdf7", effect.BalanceID);
            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.FormerSponsor);
            Assert.AreEqual("GBVFLWXYCIGPO3455XVFIKHS66FCT5AI64ZARKS7QJN4NF7K5FOXTJNL", effect.NewSponsor);
            
            var back = new ClaimableBalanceSponsorshipUpdatedEffectResponse(effect.BalanceID, effect.FormerSponsor, effect.NewSponsor);
            Assert.IsNotNull(back);
        }

        //Signer Sponsorship Created
        [TestMethod]
        public void TestSerializationSignerSponsorshipCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/signerSponsorship", "signerSponsorshipCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSignerSponsorshipCreatedEffect(back);
        }

        private static void AssertSignerSponsorshipCreatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is SignerSponsorshipCreatedEffectResponse);
            var effect = (SignerSponsorshipCreatedEffectResponse)instance;

            Assert.AreEqual("XAMF7DNTEJY74JPVMGTPZE4LFYTEGBXMGBHNUUMAA7IXMSBGHAMWSND6", effect.Signer);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", effect.Sponsor);
            
            var back = new SignerSponsorshipCreatedEffectResponse(effect.Signer, effect.Sponsor);
            Assert.IsNotNull(back);
        }

        //Signer Sponsorship Removed
        [TestMethod]
        public void TestSerializationSignerSponsorshipRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/signerSponsorship", "signerSponsorshipRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSignerSponsorshipRemovedEffect(back);
        }

        private static void AssertSignerSponsorshipRemovedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is SignerSponsorshipRemovedEffectResponse);
            var effect = (SignerSponsorshipRemovedEffectResponse)instance;

            Assert.AreEqual("XAMF7DNTEJY74JPVMGTPZE4LFYTEGBXMGBHNUUMAA7IXMSBGHAMWSND6", effect.Signer);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", effect.FormerSponsor);
            
            var back = new SignerSponsorshipRemovedEffectResponse(effect.Signer, effect.FormerSponsor);
            Assert.IsNotNull(back);
        }

        //Signer Sponsorship Updated
        [TestMethod]
        public void TestSerializationSignerSponsorshipUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/signerSponsorship", "signerSponsorshipUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertSignerSponsorshipUpdatedEffect(back);
        }

        private static void AssertSignerSponsorshipUpdatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is SignerSponsorshipUpdatedEffectResponse);
            var effect = (SignerSponsorshipUpdatedEffectResponse)instance;

            Assert.AreEqual("XAMF7DNTEJY74JPVMGTPZE4LFYTEGBXMGBHNUUMAA7IXMSBGHAMWSND6", effect.Signer);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", effect.FormerSponsor);
            Assert.AreEqual("GB5N4275ETC6A77K4DTDL3EFAQMN66PC7UITDUZUBM7Y6LDJP7EYSGOB", effect.NewSponsor);
            
            var back = new SignerSponsorshipUpdatedEffectResponse(effect.Signer, effect.FormerSponsor, effect.NewSponsor);
            Assert.IsNotNull(back);
        }

        //Trustline Sponsorship Created
        [TestMethod]
        public void TestSerializationTrustlineSponsorshipCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/trustlineSponsorship", "trustlineSponsorshipCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineSponsorshipCreatedEffect(back);
        }

        private static void AssertTrustlineSponsorshipCreatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is TrustlineSponsorshipCreatedEffectResponse);
            var effect = (TrustlineSponsorshipCreatedEffectResponse)instance;

            Assert.AreEqual("ABC:GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", effect.Asset);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", effect.Sponsor);
            
            var back = new TrustlineSponsorshipCreatedEffectResponse(effect.Asset, effect.Sponsor);
            Assert.IsNotNull(back);
        }

        //Trustline Sponsorship Removed
        [TestMethod]
        public void TestSerializationTrustlineSponsorshipRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/trustlineSponsorship", "trustlineSponsorshipRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineSponsorshipRemovedEffect(back);
        }

        private static void AssertTrustlineSponsorshipRemovedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is TrustlineSponsorshipRemovedEffectResponse);
            var effect = (TrustlineSponsorshipRemovedEffectResponse)instance;

            Assert.AreEqual("ABC:GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", effect.Asset);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", effect.FormerSponsor);
            
            var back = new TrustlineSponsorshipRemovedEffectResponse(effect.Asset, effect.FormerSponsor);
            Assert.IsNotNull(back);
        }

        //Trustline Sponsorship Updated
        [TestMethod]
        public void TestSerializationTrustlineSponsorshipUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/trustlineSponsorship", "trustlineSponsorshipUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertTrustlineSponsorshipUpdatedEffect(back);
        }

        private static void AssertTrustlineSponsorshipUpdatedEffect(EffectResponse instance)
        {
            Assert.IsTrue(instance is TrustlineSponsorshipUpdatedEffectResponse);
            var effect = (TrustlineSponsorshipUpdatedEffectResponse)instance;

            Assert.AreEqual("XYZ:GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", effect.Asset);
            Assert.AreEqual("GAEJ2UF46PKAPJYED6SQ45CKEHSXV63UQEYHVUZSVJU6PK5Y4ZVA4ELU", effect.FormerSponsor);
            Assert.AreEqual("GB5N4275ETC6A77K4DTDL3EFAQMN66PC7UITDUZUBM7Y6LDJP7EYSGOB", effect.NewSponsor);
            
            var back = new TrustlineSponsorshipUpdatedEffectResponse(effect.Asset, effect.FormerSponsor, effect.NewSponsor);
            Assert.IsNotNull(back);
        }

        //Data Sponsorship Created
        [TestMethod]
        public void TestSerializationDataSponsorshipCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/dataSponsorship", "dataSponsorshipCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertDataSponsorshipCreatedData(back);
        }

        private static void AssertDataSponsorshipCreatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is DataSponsorshipCreatedEffectResponse);
            var effect = (DataSponsorshipCreatedEffectResponse)instance;

            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.Sponsor);
            Assert.AreEqual("welcome-friend", effect.DataName);
            
            var back = new DataSponsorshipCreatedEffectResponse(effect.Sponsor, effect.DataName);
            Assert.IsNotNull(back);
        }

        //Data Sponsorship Removed
        [TestMethod]
        public void TestSerializationDataSponsorshipRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/dataSponsorship", "dataSponsorshipRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertDataSponsorshipRemovedData(back);
        }

        private static void AssertDataSponsorshipRemovedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is DataSponsorshipRemovedEffectResponse);
            var effect = (DataSponsorshipRemovedEffectResponse)instance;

            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.FormerSponsor);
            Assert.AreEqual("welcome-friend", effect.DataName);
            
            var back = new DataSponsorshipRemovedEffectResponse(effect.FormerSponsor, effect.DataName);
            Assert.IsNotNull(back);
        }


        //Data Sponsorship Updated
        [TestMethod]
        public void TestSerializationDataSponsorshipUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata/effects/dataSponsorship", "dataSponsorshipUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized);

            AssertDataSponsorshipUpdatedData(back);
        }

        private static void AssertDataSponsorshipUpdatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is DataSponsorshipUpdatedEffectResponse);
            var effect = (DataSponsorshipUpdatedEffectResponse)instance;

            Assert.AreEqual("GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ", effect.FormerSponsor);
            Assert.AreEqual("GBVFLWXYCIGPO3455XVFIKHS66FCT5AI64ZARKS7QJN4NF7K5FOXTJNL", effect.NewSponsor);
            Assert.AreEqual("welcome-friend", effect.DataName);
            
            var back = new DataSponsorshipUpdatedEffectResponse(effect.FormerSponsor, effect.NewSponsor, effect.DataName);
            Assert.IsNotNull(back);
        }

    }
}
