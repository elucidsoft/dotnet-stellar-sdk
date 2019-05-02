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
                typeof(ManageSellOfferCreated), true);
            var failed = (TransactionResultFailed) tx;
            var op = (ManageSellOfferCreated) failed.Results[0];
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
                typeof(ManageSellOfferUpdated), true);
            var failed = (TransactionResultFailed) tx;
            var op = (ManageSellOfferUpdated) failed.Results[0];
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
                typeof(ManageSellOfferDeleted), true);
            var failed = (TransactionResultFailed) tx;
            var op = (ManageSellOfferDeleted) failed.Results[0];
            Assert.AreEqual(1, op.OffersClaimed.Length);
        }

        [TestMethod]
        public void TestMalformed()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////wAAAAA=", typeof(ManageSellOfferMalformed),
                false);
        }

        [TestMethod]
        public void TestUnderfunded()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+QAAAAA=", typeof(ManageSellOfferUnderfunded),
                false);
        }

        [TestMethod]
        public void TestSellNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////gAAAAA=", typeof(ManageSellOfferSellNoTrust),
                false);
        }

        [TestMethod]
        public void TestBuyNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////QAAAAA=", typeof(ManageSellOfferBuyNoTrust),
                false);
        }

        [TestMethod]
        public void TestSellNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD/////AAAAAA=",
                typeof(ManageSellOfferSellNotAuthorized), false);
        }

        [TestMethod]
        public void TestBuyNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+wAAAAA=", typeof(ManageSellOfferBuyNotAuthorized),
                false);
        }

        [TestMethod]
        public void TestLineFull()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+gAAAAA=", typeof(ManageSellOfferLineFull), false);
        }

        [TestMethod]
        public void TestCrossSelf()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////+AAAAAA=", typeof(ManageSellOfferCrossSelf),
                false);
        }

        [TestMethod]
        public void TestSellNoIssuer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9wAAAAA=", typeof(ManageSellOfferSellNoIssuer),
                false);
        }

        [TestMethod]
        public void TestBuyNoIssuer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9gAAAAA=", typeof(ManageSellOfferBuyNoIssuer),
                false);
        }

        [TestMethod]
        public void TestNotFound()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9QAAAAA=", typeof(ManageSellOfferNotFound), false);
        }

        [TestMethod]
        public void TestLowReserve()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAD////9AAAAAA=", typeof(ManageSellOfferLowReserve),
                false);
        }
    }
}