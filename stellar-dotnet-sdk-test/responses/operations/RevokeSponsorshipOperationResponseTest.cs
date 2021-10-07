using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stellar_dotnet_sdk_test.responses.operations
{
    [TestClass]
    public class RevokeSponsorshipOperationResponseTest
    {
        //Revoke Sponsorship Account ID
        [TestMethod]
        public void TestSerializationRevokeSponsorshipAccountIDOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/revokeSponsorship", "revokeSponsorshipAccountID.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipAccountIDData(back);
        }

        private static void AssertRevokeSponsorshipAccountIDData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286156491067394, operation.Id);
            Assert.AreEqual("GCLHBHJAYWFT6JA27KEPUQCCGIHUB33HURYAKNWIY4FB7IY3K24PRXET", operation.AccountID);
        }

        //Revoke Sponsorship Claimable Balance
        [TestMethod]
        public void TestSerializationRevokeSponsorshipClaimableBalanceOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/revokeSponsorship", "revokeSponsorshipClaimableBalance.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipClaimableBalanceData(back);
        }

        private static void AssertRevokeSponsorshipClaimableBalanceData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(287054139232258, operation.Id);
            Assert.AreEqual("00000000c582697b67cbec7f9ce64f4dc67bfb2bfd26318bb9f964f4d70e3f41f650b1e6", operation.ClaimableBalanceID);
        }

        //Revoke Sponsorship Data
        [TestMethod]
        public void TestSerializationRevokeSponsorshipDataOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/revokeSponsorship", "revokeSponsorshipData.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipDataData(back);
        }

        private static void AssertRevokeSponsorshipDataData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286800736161794, operation.Id);
            Assert.AreEqual("GDHSYF7V3DZRM7Q2HS5J6FHAHNWETMBFMG7DOSWU3GA7OM4KGOPZM3FB", operation.DataAccountID);
            Assert.AreEqual("hello", operation.DataName);
        }

        //Revoke Sponsorship Offer
        [TestMethod]
        public void TestSerializationRevokeSponsorshipOfferOperation()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/revokeSponsorship", "revokeSponsorshipOffer.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipOfferData(back);
        }

        private static void AssertRevokeSponsorshipOfferData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286800736161794, operation.Id);
            Assert.IsNull(operation.OfferID);
        }

        //Revoke Sponsorship Signer Key
        [TestMethod]
        public void TestSerializationRevokeSponsorshipSignerKey()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/revokeSponsorship", "revokeSponsorshipSignerKey.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipSignerKeyData(back);
        }

        private static void AssertRevokeSponsorshipSignerKeyData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(287363376877570, operation.Id);
            Assert.AreEqual("GAXHU2XHSMTZYAKFCVTULAYUL34BFPPLRVJYZMEOHP7IWPZJKSVY67RJ", operation.SignerAccountID);
            Assert.AreEqual("XAMF7DNTEJY74JPVMGTPZE4LFYTEGBXMGBHNUUMAA7IXMSBGHAMWSND6", operation.SignerKey);
        }

        //Revoke Sponsorship Signer Key
        [TestMethod]
        public void TestSerializationRevokeSponsorshipTrustline()
        {
            var json = File.ReadAllText(Path.Combine("testdata/operations/revokeSponsorship", "revokeSponsorshipTrustline.json"));
            var instance = JsonSingleton.GetInstance<OperationResponse>(json);
            var serialized = JsonConvert.SerializeObject(instance);
            var back = JsonConvert.DeserializeObject<OperationResponse>(serialized);

            AssertRevokeSponsorshipTrustlineData(back);
        }

        private static void AssertRevokeSponsorshipTrustlineData(OperationResponse instance)
        {
            Assert.IsTrue(instance is RevokeSponsorshipOperationResponse);
            var operation = (RevokeSponsorshipOperationResponse)instance;

            Assert.AreEqual(286500088451074, operation.Id);
            Assert.AreEqual("GDHSYF7V3DZRM7Q2HS5J6FHAHNWETMBFMG7DOSWU3GA7OM4KGOPZM3FB", operation.TrustlineAccountID);
            Assert.AreEqual("XYZ:GD2I2F7SWUHBAD7XBIZTF7MBMWQYWJVEFMWTXK76NSYVOY52OJRYNTIY", operation.TrustlineAsset);
        }
    }
}
