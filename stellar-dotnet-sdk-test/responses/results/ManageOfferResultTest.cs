using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class ManageOfferResultTest
    {
        [TestMethod]
        public void TestSuccessCreated()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAADAAAAAAAAAAEAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAAAE0gAAAAAAAAAAAJiWgAAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAADDUAAAAAAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAAABNIAAAAAAAAAAVVTRAAAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAJiWgAAAA+gAABEYAAAAAQAAAAAAAAAA",
                typeof(ManageOfferCreated), true);
            var failed = (TransactionResultFailed) tx;
            var op = (ManageOfferCreated) failed.Results[0];
            var offer = op.Offer;
            Assert.AreEqual("GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC", offer.Seller.AccountId);
            Assert.AreEqual(1234, offer.OfferId);
            Assert.AreEqual(new AssetTypeNative(), offer.Selling);
            Assert.AreEqual(
                Asset.CreateNonNativeAsset("USD", "GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC"),
                offer.Buying);
            Assert.AreEqual("1", offer.Amount);
            Assert.AreEqual(new Price(1000, 4376), offer.Price);

            Assert.AreEqual(1, op.OffersClaimed.Length);
        }

        [TestMethod]
        public void TestSuccessUpdated()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAADAAAAAAAAAAEAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAAAE0gAAAAAAAAAAAJiWgAAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAADDUAAAAABAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAAABNIAAAAAAAAAAVVTRAAAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAJiWgAAAA+gAABEYAAAAAQAAAAAAAAAA",
                typeof(ManageOfferUpdated), true);
            var failed = (TransactionResultFailed) tx;
            var op = (ManageOfferUpdated) failed.Results[0];
            var offer = op.Offer;
            Assert.AreEqual("GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC", offer.Seller.AccountId);
            Assert.AreEqual(1234, offer.OfferId);
            Assert.AreEqual(new AssetTypeNative(), offer.Selling);
            Assert.AreEqual(
                Asset.CreateNonNativeAsset("USD", "GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC"),
                offer.Buying);
            Assert.AreEqual("1", offer.Amount);
            Assert.AreEqual(new Price(1000, 4376), offer.Price);

            Assert.AreEqual(1, op.OffersClaimed.Length);
        }

        [TestMethod]
        public void TestSuccessDeleted()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAADAAAAAAAAAAEAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAAAE0gAAAAAAAAAAAJiWgAAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAADDUAAAAACAAAAAA==",
                typeof(ManageOfferDeleted), true);
            var failed = (TransactionResultFailed) tx;
            var op = (ManageOfferDeleted) failed.Results[0];
            Assert.AreEqual(1, op.OffersClaimed.Length);
        }

        [TestMethod]
        public void TestMalformed()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////wAAAAA=", typeof(ManageOfferMalformed),
                false);
        }

        [TestMethod]
        public void TestUnderfunded()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+QAAAAA=", typeof(ManageOfferUnderfunded),
                false);
        }

        [TestMethod]
        public void TestSellNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////gAAAAA=", typeof(ManageOfferSellNoTrust),
                false);
        }

        [TestMethod]
        public void TestBuyNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////QAAAAA=", typeof(ManageOfferBuyNoTrust),
                false);
        }

        [TestMethod]
        public void TestSellNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////AAAAAA=",
                typeof(ManageOfferSellNotAuthorized), false);
        }

        [TestMethod]
        public void TestBuyNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+wAAAAA=", typeof(ManageOfferBuyNotAuthorized),
                false);
        }

        [TestMethod]
        public void TestLineFull()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+gAAAAA=", typeof(ManageOfferLineFull), false);
        }

        [TestMethod]
        public void TestCrossSelf()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+AAAAAA=", typeof(ManageOfferCrossSelf),
                false);
        }

        [TestMethod]
        public void TestSellNoIssuer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9wAAAAA=", typeof(ManageOfferSellNoIssuer),
                false);
        }

        [TestMethod]
        public void TestBuyNoIssuer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9gAAAAA=", typeof(ManageOfferBuyNoIssuer),
                false);
        }

        [TestMethod]
        public void TestNotFound()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9QAAAAA=", typeof(ManageOfferNotFound), false);
        }

        [TestMethod]
        public void TestLowReserve()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9AAAAAA=", typeof(ManageOfferLowReserve),
                false);
        }
    }
}