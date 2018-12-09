using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class AccountDeserializerTest
    {
        [TestMethod]
        public void TestDeserializeAccountResponse()
        {
            var json = File.ReadAllText(Path.Combine("testdata", "account.json"));
            var account = JsonSingleton.GetInstance<AccountResponse>(json);

            AssertTestData(account);
        }

        public static void AssertTestData(AccountResponse account)
        {
            Assert.AreEqual(account.KeyPair.AccountId, "GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7");
            Assert.AreEqual(account.PagingToken, "1");
            Assert.AreEqual(account.SequenceNumber, 2319149195853854);
            Assert.AreEqual(account.SubentryCount, 0);
            Assert.AreEqual(account.InflationDestination, "GAGRSA6QNQJN2OQYCBNQGMFLO4QLZFNEHIFXOMTQVSUTWVTWT66TOFSC");
            Assert.AreEqual(account.HomeDomain, "stellar.org");

            Assert.AreEqual(account.Thresholds.LowThreshold, 10);
            Assert.AreEqual(account.Thresholds.MedThreshold, 20);
            Assert.AreEqual(account.Thresholds.HighThreshold, 30);

            Assert.AreEqual(account.Flags.AuthRequired, false);
            Assert.AreEqual(account.Flags.AuthRevocable, true);
            Assert.AreEqual(account.Flags.AuthImmutable, true);

            Assert.AreEqual(account.Balances[0].AssetType, "credit_alphanum4");
            Assert.AreEqual(account.Balances[0].AssetCode, "ABC");
            Assert.AreEqual(account.Balances[0].AssetIssuer.AccountId, "GCRA6COW27CY5MTKIA7POQ2326C5ABYCXODBN4TFF5VL4FMBRHOT3YHU");
            var asset = (AssetTypeCreditAlphaNum) account.Balances[0].Asset;
            Assert.IsInstanceOfType(asset, typeof(AssetTypeCreditAlphaNum));
            Assert.AreEqual(asset.Code, "ABC");
            Assert.AreEqual(asset.Issuer.AccountId, "GCRA6COW27CY5MTKIA7POQ2326C5ABYCXODBN4TFF5VL4FMBRHOT3YHU");
            Assert.AreEqual(account.Balances[0].BalanceString, "1001.0000000");
            Assert.AreEqual(account.Balances[0].Limit, "12000.4775807");
            Assert.AreEqual(account.Balances[0].BuyingLiabilities, "100.1234567");
            Assert.AreEqual(account.Balances[0].SellingLiabilities, "100.7654321");

            Assert.AreEqual(account.Balances[1].AssetType, "native");
            Assert.IsInstanceOfType(account.Balances[1].Asset, typeof(AssetTypeNative));
            Assert.AreEqual(account.Balances[1].BalanceString, "20.0000300");
            Assert.AreEqual(account.Balances[1].BuyingLiabilities, "5.1234567");
            Assert.AreEqual(account.Balances[1].SellingLiabilities, "1.7654321");
            Assert.AreEqual(account.Balances[1].Limit, null);

            Assert.AreEqual(account.Signers[0].AccountId, "GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7");
            Assert.AreEqual(account.Signers[0].Weight, 0);
            Assert.AreEqual(account.Signers[0].Type, "ed25519_public_key");
            Assert.AreEqual(account.Signers[1].Key, "GCR2KBCIU6KQXSQY5F5GZYC4WLNHCHCKW4NEGXNEZRYWLTNZIRJJY7D2");
            Assert.AreEqual(account.Signers[1].Weight, 1);
            Assert.AreEqual(account.Signers[1].Type, "ed25519_public_key");

            Assert.AreEqual(account.Data["TestKey"], "VGVzdFZhbHVl");

            Assert.AreEqual(account.Links.Effects.Href, "/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/effects{?cursor,limit,order}");
            Assert.AreEqual(account.Links.Offers.Href, "/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/offers{?cursor,limit,order}");
            Assert.AreEqual(account.Links.Operations.Href, "/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/operations{?cursor,limit,order}");
            Assert.AreEqual(account.Links.Self.Href, "/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7");
            Assert.AreEqual(account.Links.Transactions.Href, "/accounts/GAAZI4TCR3TY5OJHCTJC2A4QSY6CJWJH5IAJTGKIN2ER7LBNVKOCCWN7/transactions{?cursor,limit,order}");
        }
    }
}