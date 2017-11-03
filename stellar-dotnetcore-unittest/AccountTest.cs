using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestNullArguments()
        {
            try
            {
                new Account(null, 10L);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                new Account(KeyPair.Random(), null);
                Assert.Fail();
            }
            catch (ArgumentNullException)
            {
            }
        }

        [TestMethod]
        public void TestGetIncrementedSequenceNumber()
        {
            var account = new Account(KeyPair.Random(), 100L);
            long incremented;
            incremented = account.GetIncrementedSequenceNumber();
            Assert.AreEqual(100L, account.SequenceNumber);
            Assert.AreEqual(101L, incremented);
            incremented = account.GetIncrementedSequenceNumber();
            Assert.AreEqual(100L, account.SequenceNumber);
            Assert.AreEqual(101L, incremented);
        }

        [TestMethod]
        public void TestIncrementSequenceNumber()
        {
            var account = new Account(KeyPair.Random(), 100L);
            account.IncrementSequenceNumber();
            Assert.AreEqual(account.SequenceNumber, 101L);
        }

        [TestMethod]
        public void TestGetters()
        {
            var keypair = KeyPair.Random();
            var account = new Account(keypair, 100L);
            Assert.AreEqual(account.KeyPair.AccountId, keypair.AccountId);
            Assert.AreEqual(account.SequenceNumber, 100L);
        }
    }
}