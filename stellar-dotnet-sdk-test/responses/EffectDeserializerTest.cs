using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        public static void AssertAccountCreatedData(EffectResponse instance)
        {
            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreatedEffectResponse);
            var effect = (AccountCreatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ");
            Assert.AreEqual(effect.StartingBalance, "30.0");
            Assert.AreEqual(effect.PagingToken, "65571265847297-1");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/65571265847297");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265847297-1");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265847297-1");
        }

        [TestMethod]
        public void TestDeserializeAccountRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountRemovedEffectResponse);
            var effect = (AccountRemovedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GCBQ6JRBPF3SXQBQ6SO5MRBE7WVV4UCHYOSHQGXSZNPZLFRYVYOWBZRQ");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/65571265847297");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265847297-1");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265847297-1");
        }

        [TestMethod]
        public void TestDeserializeAccountCreditedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountCredited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountCreditedEffectResponse);
            var effect = (AccountCreditedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GDLGTRIBFH24364GPWPUS45GUFC2GU4ARPGWTXVCPLGTUHX3IOS3ON47");
            Assert.AreEqual(effect.Asset, new AssetTypeNative());
            Assert.AreEqual(effect.Amount, "1000.0");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/13563506724865");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=13563506724865-1");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=13563506724865-1");
        }

        [TestMethod]
        public void TestDeserializeAccountDebitedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountDebited.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountDebitedEffectResponse);
            var effect = (AccountDebitedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GBRPYHIL2CI3FNQ4BXLFMNDLFJUNPU2HY3ZMFSHONUCEOASW7QC7OX2H");
            Assert.AreEqual(effect.Asset, new AssetTypeNative());
            Assert.AreEqual(effect.Amount, "30.0");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/65571265843201");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265843201-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265843201-2");
        }

        [TestMethod]
        public void TestDeserializeAccountThresholdsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountThresholdsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountThresholdsUpdatedEffectResponse);
            var effect = (AccountThresholdsUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.LowThreshold, 2);
            Assert.AreEqual(effect.MedThreshold, 3);
            Assert.AreEqual(effect.HighThreshold, 4);

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
        }

        [TestMethod]
        public void TestDeserializeAccountHomeDomainUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountHomeDomainUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountHomeDomainUpdatedEffectResponse);
            var effect = (AccountHomeDomainUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.HomeDomain, "stellar.org");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
        }

        [TestMethod]
        public void TestDeserializeAccountFlagsUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountFlagsUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountFlagsUpdatedEffectResponse);
            var effect = (AccountFlagsUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AuthRequiredFlag, false);
            Assert.AreEqual(effect.AuthRevokableFlag, true);

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/18970870550529");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=18970870550529-1");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=18970870550529-1");
        }

        [TestMethod]
        public void TestDeserializeSignerCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerCreatedEffectResponse);
            var effect = (SignerCreatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GB24LPGAHYTWRYOXIDKXLI55SBRWW42T3TZKDAAW3BOJX4ADVIATFTLU");
            Assert.AreEqual(effect.Weight, 1);
            Assert.AreEqual(effect.PublicKey, "GB24LPGAHYTWRYOXIDKXLI55SBRWW42T3TZKDAAW3BOJX4ADVIATFTLU");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/65571265859585");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=65571265859585-3");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=65571265859585-3");
        }

        [TestMethod]
        public void TestDeserializeSignerRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerRemovedEffectResponse);
            var effect = (SignerRemovedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GCFKT6BN2FEASCEVDNHEC4LLFT2KLUUPEMKM4OJPEJ65H2AEZ7IH4RV6");
            Assert.AreEqual(effect.Weight, 0);
            Assert.AreEqual(effect.PublicKey, "GCFKT6BN2FEASCEVDNHEC4LLFT2KLUUPEMKM4OJPEJ65H2AEZ7IH4RV6");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/43658342567940");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=43658342567940-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=43658342567940-2");
        }

        [TestMethod]
        public void TestDeserializeSignerUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectSignerUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is SignerUpdatedEffectResponse);
            var effect = (SignerUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Weight, 2);
            Assert.AreEqual(effect.PublicKey, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineCreatedEffectResponse);
            var effect = (TrustlineCreatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset, Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA")));
            Assert.AreEqual(effect.Limit, "1000.0");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineRemovedEffectResponse);
            var effect = (TrustlineRemovedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset, Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA")));
            Assert.AreEqual(effect.Limit, "0.0");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineUpdatedEffectResponse);
            var effect = (TrustlineUpdatedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Asset, Asset.CreateNonNativeAsset("TESTTEST", KeyPair.FromAccountId("GAZN3PPIDQCSP5JD4ETQQQ2IU2RMFYQTAL4NNQZUGLLO2XJJJ3RDSDGA")));
            Assert.AreEqual(effect.Limit, "100.0");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineAuthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineAuthorizedEffectResponse);
            var effect = (TrustlineAuthorizedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, "credit_alphanum12");
            Assert.AreEqual(effect.AssetCode, "TESTTEST");
            Assert.AreEqual(effect.Trustor.AccountId, "GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTrustlineDeauthorizedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrustlineDeAuthorized.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TrustlineDeauthorizedEffectResponse);
            var effect = (TrustlineDeauthorizedEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.AssetType, "credit_alphanum4");
            Assert.AreEqual(effect.AssetCode, "EUR");
            Assert.AreEqual(effect.Trustor.AccountId, "GB3E4AB4VWXJDUVN4Z3CPBU5HTMWVEQXONZYVDFMHQD6333KHCOL3UBR");

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeTradeEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectTrade.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is TradeEffectResponse);
            var effect = (TradeEffectResponse)instance;

            Assert.AreEqual(effect.Account.AccountId, "GA6U5X6WOPNKKDKQULBR7IDHDBAQKOWPHYEC7WSXHZBFEYFD3XVZAKOO");
            Assert.AreEqual(effect.Seller.AccountId, "GCVHDLN6EHZBYW2M3BQIY32C23E4GPIRZZDBNF2Q73DAZ5VJDRGSMYRB");
            Assert.AreEqual(effect.OfferId, 1);
            Assert.AreEqual(effect.SoldAmount, "1000.0");
            Assert.AreEqual(effect.SoldAsset, Asset.CreateNonNativeAsset("EUR", KeyPair.FromAccountId("GCWVFBJ24754I5GXG4JOEB72GJCL3MKWC7VAEYWKGQHPVH3ENPNBSKWS")));
            Assert.AreEqual(effect.BoughtAmount, "60.0");
            Assert.AreEqual(effect.BoughtAsset, Asset.CreateNonNativeAsset("TESTTEST", KeyPair.FromAccountId("GAHXPUDP3AK6F2QQM4FIRBGPNGKLRDDSTQCVKEXXKKRHJZUUQ23D5BU7")));

            Assert.AreEqual(effect.Links.Operation.Href, "http://horizon-testnet.stellar.org/operations/33788507721730");
            Assert.AreEqual(effect.Links.Succeeds.Href, "http://horizon-testnet.stellar.org/effects?order=desc&cursor=33788507721730-2");
            Assert.AreEqual(effect.Links.Precedes.Href, "http://horizon-testnet.stellar.org/effects?order=asc&cursor=33788507721730-2");
        }

        [TestMethod]
        public void TestDeserializeAccountInflationUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectAccountInflationUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is AccountInflationDestinationUpdatedEffectResponse);
            var effect = (AccountInflationDestinationUpdatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataCreatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataCreated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataCreatedEffectResponse);
            var effect = (DataCreatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataRemovedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataRemoved.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataRemovedEffectResponse);
            var effect = (DataRemovedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestDeserializeDataUpdatedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectDataUpdated.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            //There is a JsonConverter called OperationDeserializer that instantiates the type based on the json type_i element...
            Assert.IsTrue(instance is DataUpdatedEffectResponse);
            var effect = (DataUpdatedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
        }

        [TestMethod]
        public void TestUnknownEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "effectUnknown.json"));
            try
            {
                var instance = JsonSingleton.GetInstance<EffectResponse>(json);
                Assert.Fail();
            }
            catch
            {
                //We want the exception to pass the test, that is what it should be doing.
            }
        }

        [TestMethod]
        public void TestDeserializeSequenceBumpedEffect()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "sequenceBumped.json"));
            var instance = JsonSingleton.GetInstance<EffectResponse>(json);

            Assert.IsTrue(instance is SequenceBumpedEffectResponse);
            var effect = (SequenceBumpedEffectResponse)instance;

            Assert.AreEqual("GDPFGP4IPE5DXG6XRXC4ZBUI43PAGRQ5VVNJ3LJTBXDBZ4ITO6HBHNSF", effect.Account.AccountId);
            Assert.AreEqual(DateTimeOffset.Parse("2018-06-06T10:23:57Z").UtcDateTime, effect.CreatedAt);
            Assert.AreEqual(79473726952833048L, effect.NewSequence);
        }
    }
}
