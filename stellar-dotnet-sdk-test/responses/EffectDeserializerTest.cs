using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.effects;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class EffectDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeAccountCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountCreatedData(back);
        }

        public static void AssertAccountCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreatedEffectResponse);
            var effect = (AccountCreatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ");
            Assert.AreEqual(effect.StartingBalance, "30.0");
            Assert.AreEqual(effect.PagingToken, "65571265847297-1");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265847297");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265847297-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265847297-1");
        }

        [TestMethod]
        public void TestDeserializeAccountRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountRemovedData(back);
        }

        private static void AssertAccountRemovedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountRemovedEffectResponse);
            var effect = (AccountRemovedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ");

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
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCredited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountCreditedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountCreditedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCredited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountCreditedData(back);
        }

        private static void AssertAccountCreditedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreditedEffectResponse);
            var effect = (AccountCreditedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GDLGTRIBFH24364GPWPUS45GUFC2GU4ARPGWTXVCPLGTUHX3IOS3ON47");
            Assert.AreEqual(effect.Asset, new AssetTypeNative());
            Assert.AreEqual(effect.Amount, "1000.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/13563506724865");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=13563506724865-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=13563506724865-1");
        }

        [TestMethod]
        public void TestDeserializeAccountDebitedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountDebited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountDebitedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountDebitedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountDebited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountDebitedData(back);
        }

        private static void AssertAccountDebitedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountDebitedEffectResponse);
            var effect = (AccountDebitedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H");
            Assert.AreEqual(effect.Asset, new AssetTypeNative());
            Assert.AreEqual(effect.Amount, "30.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265843201");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265843201-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265843201-2");
        }

        [TestMethod]
        public void TestDeserializeAccountThresholdsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountThresholdsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountThresholdsUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountThresholdsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountThresholdsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountThresholdsUpdatedData(back);
        }

        private static void AssertAccountThresholdsUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountThresholdsUpdatedEffectResponse);
            var effect = (AccountThresholdsUpdatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.LowThreshold, 2);
            Assert.AreEqual(effect.MedThreshold, 3);
            Assert.AreEqual(effect.HighThreshold, 4);

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
        }

        [TestMethod]
        public void TestDeserializeAccountHomeDomainUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountHomeDomainUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountHomeDomainUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountHomeDomainUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountHomeDomainUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountHomeDomainUpdatedData(back);
        }

        private static void AssertAccountHomeDomainUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountHomeDomainUpdatedEffectResponse);
            var effect = (AccountHomeDomainUpdatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.HomeDomain, "stellar.org");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
        }

        [TestMethod]
        public void TestDeserializeAccountFlagsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountFlagsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountFlagsUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountFlagsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountFlagsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountFlagsUpdatedData(back);
        }

        private static void AssertAccountFlagsUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountFlagsUpdatedEffectResponse);
            var effect = (AccountFlagsUpdatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AuthRequiredFlag, false);
            Assert.AreEqual(effect.AuthRevokableFlag, true);

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
        }

        [TestMethod]
        public void TestDeserializeSignerCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSignerCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSignerCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertSignerCreatedData(back);
        }

        private static void AssertSignerCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerCreatedEffectResponse);
            var effect = (SignerCreatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GB24LPGAHYTWRYOXIDKXLI55SBRWW42T3TZKDAAW3BOJX4ADVIATFTLU");
            Assert.AreEqual(effect.Weight, 1);
            Assert.AreEqual(effect.PublicKey, "GB24LPGAHYTWRYOXIDKXLI55SBRWW42T3TZKDAAW3BOJX4ADVIATFTLU");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/65571265859585");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265859585-3");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265859585-3");
        }

        [TestMethod]
        public void TestDeserializeSignerRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSignerRemoveData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSignerRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertSignerRemoveData(back);
        }

        private static void AssertSignerRemoveData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerRemovedEffectResponse);
            var effect = (SignerRemovedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GCFKT6BN2FEASCEVDNHEC4LLFT2KLUUPEMKM4OJPEJ65H2AEZ7IH4RV6");
            Assert.AreEqual(effect.Weight, 0);
            Assert.AreEqual(effect.PublicKey, "GCFKT6BN2FEASCEVDNHEC4LLFT2KLUUPEMKM4OJPEJ65H2AEZ7IH4RV6");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/43658342567940");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=43658342567940-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=43658342567940-2");
        }

        [TestMethod]
        public void TestDeserializeSignerUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSignerUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSignerUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertSignerUpdatedData(back);
        }

        private static void AssertSignerUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerUpdatedEffectResponse);
            var effect = (SignerUpdatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Weight, 2);
            Assert.AreEqual(effect.PublicKey, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertTrustlineCreatedData(back);
        }

        private static void AssertTrustlineCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineCreatedEffectResponse);
            var effect = (TrustlineCreatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset,
                Asset.CreateNonNativeAsset("EUR",
                    KeyPair.FromAccountId("GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA")));
            Assert.AreEqual(effect.Limit, "1000.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertTrustlineRemovedData(back);
        }

        private static void AssertTrustlineRemovedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineRemovedEffectResponse);
            var effect = (TrustlineRemovedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset,
                Asset.CreateNonNativeAsset("EUR",
                    KeyPair.FromAccountId("GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA")));
            Assert.AreEqual(effect.Limit, "0.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertTrustlineUpdatedData(back);
        }

        private static void AssertTrustlineUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineUpdatedEffectResponse);
            var effect = (TrustlineUpdatedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset,
                Asset.CreateNonNativeAsset("TESTTEST",
                    KeyPair.FromAccountId("GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA")));
            Assert.AreEqual(effect.Limit, "100.0");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineAuthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineAuthorizedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineAuthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertTrustlineAuthorizedData(back);
        }

        private static void AssertTrustlineAuthorizedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineAuthorizedEffectResponse);
            var effect = (TrustlineAuthorizedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, "credit_alphanum12");
            Assert.AreEqual(effect.AssetCode, "TESTTEST");
            Assert.AreEqual(effect.Trustor.AccountId, "GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineDeauthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineDeAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTrustlineDeauthorizedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTrustlineDeauthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineDeAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertTrustlineDeauthorizedData(back);
        }

        private static void AssertTrustlineDeauthorizedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineDeauthorizedEffectResponse);
            var effect = (TrustlineDeauthorizedEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, "credit_alphanum4");
            Assert.AreEqual(effect.AssetCode, "EUR");
            Assert.AreEqual(effect.Trustor.AccountId, "GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR");

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTradeEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrade.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertTradeData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeTradeEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrade.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertTradeData(back);
        }

        private static void AssertTradeData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TradeEffectResponse);
            var effect = (TradeEffectResponse) instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Seller.AccountId, "GCVHDLN6EHZBYW2M3BQIY32C23E4GPIRZZDBNF2Q73DAZ5VJDRGSMYRB");
            Assert.AreEqual(effect.OfferId, 1);
            Assert.AreEqual(effect.SoldAmount, "1000.0");
            Assert.AreEqual(effect.SoldAsset,
                Asset.CreateNonNativeAsset("EUR",
                    KeyPair.FromAccountId("GCWVFBJ24754I5GXG4JOEB72GJCL3MKWC7VAEYWKGQHPVH3ENPNBSKWS")));
            Assert.AreEqual(effect.BoughtAmount, "60.0");
            Assert.AreEqual(effect.BoughtAsset,
                Asset.CreateNonNativeAsset("TESTTEST",
                    KeyPair.FromAccountId("GAHXPUDP3AK6F2QQM4FIRBGPNGKLRDDSTQCVKEXXKKRHJZUUQ23D5BU7")));

            Assert.AreEqual(effect.Links.Operation.Href,
                "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href,
                "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href,
                "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeAccountInflationUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountInflationUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertAccountInflationUpdated(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeAccountInflationUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountInflationUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertAccountInflationUpdated(back);
        }

        private static void AssertAccountInflationUpdated(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountInflationDestinationUpdatedEffectResponse);
            var effect = (AccountInflationDestinationUpdatedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertDataCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeDataCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertDataCreatedData(back);
        }

        private static void AssertDataCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataCreatedEffectResponse);
            var effect = (DataCreatedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertDataRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeDataRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertDataRemovedData(back);
        }

        private static void AssertDataRemovedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataRemovedEffectResponse);
            var effect = (DataRemovedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertDataUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeDataUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertDataUpdatedData(back);
        }

        private static void AssertDataUpdatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataUpdatedEffectResponse);
            var effect = (DataUpdatedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestUnknownEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectUnknown.json"));
            Assert.ThrowsException<JsonSerializationException>(() => JsonSingleton.GetInstance<EffectResponse>(json));
        }

        [TestMethod]
        public void TestDeserializeSequenceBumpedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "sequenceBumped.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertSequenceBumpedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeSequenceBumpedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "sequenceBumped.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertSequenceBumpedData(back);
        }

        private static void AssertSequenceBumpedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is SequenceBumpedEffectResponse);
            var effect = (SequenceBumpedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
            Assert.AreEqual(79473726952833048L, effect.NewSequence);
        }

        [TestMethod]
        public void TestDeserializeOfferCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectOfferCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertOfferCreatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeOfferCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectOfferCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertOfferCreatedData(back);
        }

        private static void AssertOfferCreatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is OfferCreatedEffectResponse);
            var effect = (OfferCreatedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeOfferRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectOfferRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertOfferRemovedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeOfferRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectOfferRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertOfferRemovedData(back);
        }

        private static void AssertOfferRemovedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is OfferRemovedEffectResponse);
            var effect = (OfferRemovedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeOfferUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectOfferUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            AssertOfferUpdatedData(instance);
        }

        [TestMethod]
        public void TestSerializeDeserializeOfferUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectOfferUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance, new EffectDeserializer());
            var back = JsonConvert.DeserializeObject<EffectResponse>(serialized, new EffectDeserializer());

            AssertOfferUpdatedData(back);
        }

        private static void AssertOfferUpdatedData(EffectResponse instance)
        {
            Assert.IsTrue(instance is OfferUpdatedEffectResponse);
            var effect = (OfferUpdatedEffectResponse) instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }
    }
}