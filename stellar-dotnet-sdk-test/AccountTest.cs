using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestNullArguments()
        {
            try
            {
                var unused = new Account((string)null, 10L);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                KeyPair random = KeyPair.Random();
                var unused = new Account(random.AccountId, null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void TestWithStringArgumentIsKeyPair()
        {
            var keypair = KeyPair.Random();
            var account = new Account(keypair.Address, 7);
            Assert.AreEqual(account.AccountId, keypair.AccountId);
            Assert.AreEqual(account.KeyPair.AccountId, keypair.AccountId);
        }

        [TestMethod]
        public void TestWithMuxedAccount()
        {
            var keypair = KeyPair.Random();
            var muxed = new MuxedAccountMed25519(keypair, 10);
            var account = new Account(muxed, 7);
            Assert.AreNotEqual(account.AccountId, keypair.AccountId);
            Assert.AreEqual(account.AccountId, muxed.AccountId);
            Assert.AreEqual(account.KeyPair.AccountId, keypair.AccountId);
        }

        [TestMethod]
        public void TestGetIncrementedSequenceNumber()
        {
            KeyPair random = KeyPair.Random();
            var account = new Account(random.AccountId, 100L);
            long incremented;
            incremented = account.IncrementedSequenceNumber;
            Assert.AreEqual(100L, account.SequenceNumber);
            Assert.AreEqual(101L, incremented);
            incremented = account.IncrementedSequenceNumber;
            Assert.AreEqual(100L, account.SequenceNumber);
            Assert.AreEqual(101L, incremented);
        }

        [TestMethod]
        public void TestIncrementSequenceNumber()
        {
            KeyPair random = KeyPair.Random();
            var account = new Account(random.AccountId, 100L);
            account.IncrementSequenceNumber();
            Assert.AreEqual(account.SequenceNumber, 101L);
        }

        [TestMethod]
        public void TestGetters()
        {
            var keypair = KeyPair.Random();
            var account = new Account(keypair.AccountId, 100L);
            Assert.AreEqual(account.KeyPair.AccountId, keypair.AccountId);
            Assert.AreEqual(account.AccountId, keypair.AccountId);
            Assert.AreEqual(account.SequenceNumber, 100L);
        }
    }
}