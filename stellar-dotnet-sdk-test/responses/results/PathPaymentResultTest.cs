using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class PathPaymentResultTest
    {
        [TestMethod]
        public void TestSuccess()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAACAAAAAAAAAAEAAAAAKoNGsl81xj8D8XyekzKZXRuSU2KImhHkQj4QWhroY64AAAAAAAAE0gAAAAAAAAAAAJiWgAAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAAADDUAAAAAAAyzXIcEd0vK9XlVfmjyQE9QpJjOLzYUN5orR0N+Dz+QAAAABVVNEAAAAAAAqg0ayXzXGPwPxfJ6TMpldG5JTYoiaEeRCPhBaGuhjrgAAAAAAAw1AAAAAAA==",
                typeof(PathPaymentSuccess), true);
            var failed = (TransactionResultFailed) tx;
            var op = (PathPaymentSuccess) failed.Results[0];
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
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////wAAAAA=", typeof(PathPaymentMalformed),
                false);
        }

        [TestMethod]
        public void TestUnderfunded()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////gAAAAA=", typeof(PathPaymentUnderfunded),
                false);
        }

        [TestMethod]
        public void TestSrcNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////QAAAAA=", typeof(PathPaymentSrcNoTrust),
                false);
        }

        [TestMethod]
        public void TestSrcNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC/////AAAAAA=", typeof(PathPaymentSrcNotAuthorized),
                false);
        }

        [TestMethod]
        public void TestNoDestination()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+wAAAAA=", typeof(PathPaymentNoDestination),
                false);
        }

        [TestMethod]
        public void TestNoTrust()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+gAAAAA=", typeof(PathPaymentNoTrust), false);
        }

        [TestMethod]
        public void TestNotAuthorized()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+QAAAAA=", typeof(PathPaymentNotAuthorized),
                false);
        }

        [TestMethod]
        public void TestLineFull()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////+AAAAAA=", typeof(PathPaymentLineFull), false);
        }

        [TestMethod]
        public void TestNoIssuer()
        {
            var tx = Util.AssertResultOfType(
                "AAAAAACYloD/////AAAAAQAAAAAAAAAC////9wAAAAFVU0QAAAAAACqDRrJfNcY/A/F8npMymV0bklNiiJoR5EI+EFoa6GOuAAAAAA==",
                typeof(PathPaymentNoIssuer), false);
            var failed = (TransactionResultFailed) tx;
            var op = (PathPaymentNoIssuer) failed.Results[0];
            Assert.AreEqual(Asset.CreateNonNativeAsset("USD", "GAVIGRVSL424MPYD6F6J5EZSTFORXESTMKEJUEPEII7BAWQ25BR25DUC"), op.NoIssuer);
        }

        [TestMethod]
        public void TestTooFewOffer()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////9gAAAAA=", typeof(PathPaymentTooFewOffers),
                false);
        }

        [TestMethod]
        public void TestOfferCrossSelf()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////9QAAAAA=", typeof(PathPaymentOfferCrossSelf),
                false);
        }

        [TestMethod]
        public void TestOverSendmax()
        {
            Util.AssertResultOfType("AAAAAACYloD/////AAAAAQAAAAAAAAAC////9AAAAAA=", typeof(PathPaymentOverSendmax),
                false);
        }
    }
}