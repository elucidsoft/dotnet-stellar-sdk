using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class PathPaymentStrictReceiveResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAACAAAAAAAAAAEAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAAAE0gAAAAAAAAAAAJiWgAAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAADDUAAAAAAAyzXIcEd0vK9XlVfmjyQE9QpJjOLzYUN5orR0N+Dz+QAAAABVVNEAAAAAAAqg0ayXzXGPwPxfJ6TMpldG5JTYoiaEeRCPhBaGuhjrgAAAAAAAw1AAAAAAA==",
                typeof(PathPaymentStrictReceiveSuccess), true);
            var failed = (TransactionResultFailed)tx;
            var op = (PathPaymentStrictReceiveSuccess)failed.Results[0];
            Assert.AreEqual("GABSZVZBYEO5F4V5LZKV7GR4SAJ5IKJGGOF43BIN42FNDUG7QPH6IMRQ", op.Last.Destination.AccountId);
            Assert.AreEqual(Asset.CreateNonNativeAsset("USD", "GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC"), op.Last.Asset);
            Assert.AreEqual("0.02", op.Last.Amount);
            Assert.AreEqual(1, op.Offers.Length);
            var offer = op.Offers[0];
            Assert.AreEqual("GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC", offer.Seller.AccountId);
            Assert.AreEqual(1234, offer.OfferId);
            Assert.AreEqual(new AssetTypeNative(), offer.AssetSold);
            Assert.AreEqual("1", offer.AmountSold);
            Assert.AreEqual(Asset.CreateNonNativeAsset("USD", "GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC"), offer.AssetBought);
            Assert.AreEqual("0.02", offer.AmountBought);
        }

        [TestMethod]
        public void TestMalformed()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////wAAAAA=", typeof(PathPaymentStrictReceiveMalformed),
                false);
        }

        [TestMethod]
        public void TestUnderfunded()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////gAAAAA=", typeof(PathPaymentStrictReceiveUnderfunded),
                false);
        }

        [TestMethod]
        public void TestSrcNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////QAAAAA=", typeof(PathPaymentStrictReceiveSrcNoTrust),
                false);
        }

        [TestMethod]
        public void TestSrcNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////AAAAAA=", typeof(PathPaymentStrictReceiveSrcNotAuthorized),
                false);
        }

        [TestMethod]
        public void TestNoDestination()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+wAAAAA=", typeof(PathPaymentStrictReceiveNoDestination),
                false);
        }

        [TestMethod]
        public void TestNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+gAAAAA=", typeof(PathPaymentStrictReceiveNoTrust), false);
        }

        [TestMethod]
        public void TestNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+QAAAAA=", typeof(PathPaymentStrictReceiveNotAuthorized),
                false);
        }

        [TestMethod]
        public void TestLineFull()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+AAAAAA=", typeof(PathPaymentStrictReceiveLineFull), false);
        }

        [TestMethod]
        public void TestNoIssuer()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAAC////9wAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAA==",
                typeof(PathPaymentStrictReceiveNoIssuer), false);
            var failed = (TransactionResultFailed)tx;
            var op = (PathPaymentStrictReceiveNoIssuer)failed.Results[0];
            Assert.AreEqual(Asset.CreateNonNativeAsset("USD", "GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC"), op.NoIssuer);
        }

        [TestMethod]
        public void TestTooFewOffer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////9gAAAAA=", typeof(PathPaymentStrictReceiveTooFewOffers),
                false);
        }

        [TestMethod]
        public void TestOfferCrossSelf()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////9QAAAAA=", typeof(PathPaymentStrictReceiveOfferCrossSelf),
                false);
        }

        [TestMethod]
        public void TestOverSendmax()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////9AAAAAA=", typeof(PathPaymentStrictReceiveOverSendmax),
                false);
        }
    }
}