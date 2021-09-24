using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class LedgerKeyTest
    {
        [TestMethod]
        public void TestLedgerKeyAccount()
        {
            var keypair = KeyPair.FromAccountId("GCFRHRU5YRI3IN3IMRMYGWWEG2PX2B6MYH2RJW7NEDE2PTYPISPT3RU7");
            var ledgerKey = LedgerKey.Account(keypair);
            var xdr = ledgerKey.ToXdr();
            var back = LedgerKey.FromXdr(xdr);

            var account = (LedgerKeyAccount)back;

            Assert.IsNotNull(account);
            Assert.AreEqual(keypair.AccountId, account.Account.AccountId);
        }

        [TestMethod]
        public void TestLedgerKeyData()
        {
            var keypair = KeyPair.FromAccountId("GCFRHRU5YRI3IN3IMRMYGWWEG2PX2B6MYH2RJW7NEDE2PTYPISPT3RU7");
            var ledgerKey = LedgerKey.Data(keypair, "Test Data");
            var xdr = ledgerKey.ToXdr();
            var back = LedgerKey.FromXdr(xdr);

            var data = (LedgerKeyData)back;

            Assert.IsNotNull(data);
            Assert.AreEqual("Test Data", data.DataName);
            Assert.AreEqual(keypair.AccountId, data.Account.AccountId);
        }

        [TestMethod]
        public void TestLedgerKeyOffer()
        {
            var keypair = KeyPair.FromAccountId("GCFRHRU5YRI3IN3IMRMYGWWEG2PX2B6MYH2RJW7NEDE2PTYPISPT3RU7");
            var ledgerKey = LedgerKey.Offer(keypair, 1234);
            var xdr = ledgerKey.ToXdr();
            var back = LedgerKey.FromXdr(xdr);

            var offer = (LedgerKeyOffer)back;

            Assert.IsNotNull(offer);
            Assert.AreEqual(1234, offer.OfferId);
            Assert.AreEqual(keypair.AccountId, offer.Seller.AccountId);
        }

        [TestMethod]
        public void TestLedgerKeyTrustline()
        {
            var keypair = KeyPair.FromAccountId("GCFRHRU5YRI3IN3IMRMYGWWEG2PX2B6MYH2RJW7NEDE2PTYPISPT3RU7");
            var issuer = KeyPair.FromAccountId("GB24C27VKWCBG7NTCT4J2L4MXJGYC3K3SQ4JOTCSPOVVEN7EZEB43XNE");
            var asset = TrustlineAsset.CreateNonNativeAsset("ABCD", issuer.AccountId);
            var ledgerKey = LedgerKey.Trustline(keypair, asset);
            var xdr = ledgerKey.ToXdr();
            var back = LedgerKey.FromXdr(xdr);

            var trustline = (LedgerKeyTrustline)back;

            Assert.IsNotNull(trustline);
            Assert.AreEqual("ABCD:GB24C27VKWCBG7NTCT4J2L4MXJGYC3K3SQ4JOTCSPOVVEN7EZEB43XNE", "trustline.Asset.CanonicalName()");
            Assert.AreEqual(keypair.AccountId, trustline.Account.AccountId);
        }


        [TestMethod]
        public void TestLedgerKeyClaimableBalance()
        {
            var balanceId = Util.HexToBytes("c582697b67cbec7f9ce64f4dc67bfb2bfd26318bb9f964f4d70e3f41f650b1e6");
            var ledgerKey = LedgerKey.ClaimableBalance(balanceId);
            var xdr = ledgerKey.ToXdr();
            var back = LedgerKey.FromXdr(xdr);

            var claimableBalance = (LedgerKeyClaimableBalance)back;

            Assert.IsNotNull(claimableBalance);
            Assert.AreEqual(balanceId, claimableBalance.BalanceId);
        }
    }
}