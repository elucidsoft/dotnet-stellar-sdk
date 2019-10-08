using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class WebAuthenticationTest
    {
        [TestMethod]
        public void TestBuildChallengeTransaction()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";
            var anchorName = "NET";
            Network.UseTestNetwork();
            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientAccountId, anchorName);

            var serializedTx = tx.ToEnvelopeXdrBase64();
            var back = Transaction.FromEnvelopeXdr(serializedTx);

            var timeout = back.TimeBounds.MaxTime - back.TimeBounds.MinTime;
            Assert.AreEqual(300, timeout);

            CheckAccounts(back, serverKeypair);
            CheckOperation(back, clientAccountId);
        }

        [TestMethod]
        public void TestBuildChallengeTransactionWithOptions()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";
            var anchorName = "NET";

            var nonce = new byte[48];
            Array.Clear(nonce, 0, nonce.Length);

            var now = new DateTimeOffset();
            var duration = TimeSpan.FromMinutes(10.0);

            var tx = WebAuthentication
                .BuildChallengeTransaction(serverKeypair, clientAccountId, anchorName, nonce, now, duration, Network.Test());

            var serializedTx = tx.ToEnvelopeXdrBase64();
            var back = Transaction.FromEnvelopeXdr(serializedTx);

            var timeout = back.TimeBounds.MaxTime - back.TimeBounds.MinTime;
            Assert.AreEqual(600, timeout);

            CheckAccounts(back, serverKeypair);
            CheckOperation(back, clientAccountId);

        }

        [TestMethod]
        public void TestVerifyChallengeTransactionReturnsTrueForValidTransaction()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Sign(clientKeypair);

            Assert.IsTrue(WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now));
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfSequenceIsNotZero()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var nonce = new byte[64];
            var tx = new Transaction
                .Builder(new Account(serverKeypair.AccountId, 0))
                .AddOperation(new ManageDataOperation.Builder("NET auth", nonce).Build())
                .Build();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfServerAccountIdIsDifferent()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, KeyPair.Random().AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfTransactionHasNoManageDataOperation()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = new Transaction
                .Builder(new Account(serverKeypair.AccountId, -1))
                .AddOperation(
                    new AccountMergeOperation.Builder(serverKeypair)
                        .SetSourceAccount(clientKeypair)
                        .Build())
                .Build();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfOperationHasNoSourceAccount()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;
            var nonce = new byte[64];
            var tx = new Transaction
                .Builder(new Account(serverKeypair.AccountId, -1))
                .AddOperation(new ManageDataOperation.Builder("NET auth", nonce).Build())
                .Build();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {

                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfOperationDataIsNotBase64Encoded()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;
            var nonce = new byte[64];
            var tx = new Transaction
                .Builder(new Account(serverKeypair.AccountId, -1))
                .AddOperation(
                    new ManageDataOperation
                    .Builder("NET auth", nonce)
                        .SetSourceAccount(clientKeypair)
                        .Build())
                .Build();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfNotSignedByServer()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Signatures.Clear();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfSignedByServerOnDifferentNetwork()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now, network: Network.Public());
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfNotSignedByClient()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfSignedByClientOnDifferentNetwork()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Sign(clientKeypair, Network.Public());

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now, network: Network.Test());
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfItsTooEarly()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
                {
                    WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now.Subtract(TimeSpan.FromDays(1.0)));
                });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfItsTooLate()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var anchorName = "NET";
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, anchorName, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
                {
                    WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, now: now.Add(TimeSpan.FromDays(1.0)));
                });
        }

        private void CheckAccounts(Transaction tx, KeyPair serverKeypair)
        {

            Assert.AreEqual(0, tx.SequenceNumber);
            Assert.AreEqual(serverKeypair.AccountId, tx.SourceAccount.AccountId);
        }

        private void CheckOperation(Transaction tx, string clientAccountId)
        {

            Assert.AreEqual(1, tx.Operations.Length);
            var operation = tx.Operations[0] as ManageDataOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual("NET auth", operation.Name);
            Assert.AreEqual(clientAccountId, operation.SourceAccount.AccountId);
            Assert.AreEqual(64, operation.Value.Length);
            var bytes = Convert.FromBase64String(Encoding.UTF8.GetString(operation.Value));
            Assert.AreEqual(48, bytes.Length);

        }
    }
}