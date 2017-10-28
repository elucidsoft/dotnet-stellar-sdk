using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Text;

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
            catch (ArgumentNullException) { }

            try
            {
                new Account(KeyPair.Random(), null);
                Assert.Fail();
            }
            catch (ArgumentNullException) { }
        }

        [TestMethod]
        public void TestGetIncrementedSequenceNumber()
        {
            Account account = new Account(KeyPair.Random(), 100L);
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
            Account account = new Account(KeyPair.Random(), 100L);
            account.IncrementSequenceNumber();
            Assert.AreEqual(account.SequenceNumber, 101L);
        }

        [TestMethod]
        public void TestGetters()
        {
            KeyPair keypair = KeyPair.Random();
            Account account = new Account(keypair, 100L);
            Assert.AreEqual(account.KeyPair.AccountId, keypair.AccountId);
            Assert.AreEqual(account.SequenceNumber, 100L);
        }
    }
}
