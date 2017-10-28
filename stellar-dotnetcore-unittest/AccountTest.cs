using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_unittest
{
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
            catch (NullReferenceException) { }

            try
            {
                new Account(KeyPair.Random(), null);
                Assert.Fail();
            }
            catch (NullReferenceException) { }
        }

        [TestMethod]
        public void TestGetIncrementedSequenceNumber()
        {
            Account account = new Account(KeyPair.Random(), 100L);
            long incremented;
            incremented = account.GetIncrementedSequenceNumber();
            Assert.AreEqual(100L, account.GetSequenceNumber());
            Assert.AreEqual(101L, incremented);
            incremented = account.GetIncrementedSequenceNumber();
            Assert.AreEqual(100L, account.GetSequenceNumber());
            Assert.AreEqual(101L, incremented);
        }

        [TestMethod]
        public void TestIncrementSequenceNumber()
        {
            Account account = new Account(KeyPair.Random(), 100L);
            account.IncrementSequenceNumber();
            Assert.AreEqual(account.GetSequenceNumber(), 101L);
        }

        [TestMethod]
        public void TestGetters()
        {
            KeyPair keypair = KeyPair.Random();
            Account account = new Account(keypair, 100L);
            Assert.AreEqual(account.GetKeypair().AccountId, keypair.AccountId);
            Assert.AreEqual(account.GetSequenceNumber(), 100L);
        }
    }
}
