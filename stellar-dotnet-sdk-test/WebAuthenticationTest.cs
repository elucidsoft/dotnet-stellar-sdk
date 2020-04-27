using System;
using System.Collections.Generic;
using System.Linq;
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
            var tx = new TransactionBuilder(new Account(serverKeypair.AccountId, 0))
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

            var tx = new TransactionBuilder(new Account(serverKeypair.AccountId, -1))
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
            var tx = new TransactionBuilder(new Account(serverKeypair.AccountId, -1))
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
            var tx = new TransactionBuilder(new Account(serverKeypair.AccountId, -1))
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

        [TestMethod]
        public void TestReadChallengeTransactionValidSignedByServerAndClient()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());

            Assert.AreEqual(clientKeypair.AccountId, readTransactionID);
        }

        [TestMethod]
        public void TestReadChallengeTransactionValidSignedByServer()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);


            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());

            Assert.AreEqual(clientKeypair.AccountId, readTransactionID);
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidNotSignedByServer()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());

            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by server"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidServerAccountIDMismatch()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(KeyPair.Random().Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction source must be serverAccountId"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidSequenceNoNotZero()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, 1234);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction sequence number must be 0"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidTooManyOperations()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation).AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction must contain one operation"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidOperationWrongType()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var operation = new BumpSequenceOperation.Builder(100).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction operation must be of type ManageDataOperation"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidOperationNoSourceAccount()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).Build();
            var transaction = new TransactionBuilder(txSource).AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction operation must have source account"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionInvalidDataValueWrongEncodedLength()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA?AAAAAAAAAAAAAAAAAAAAAAAAAA");
            var base64Data = plainTextBytes;

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction operation data must be base64 encoded"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdInvalidServer()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(clientKeypair);

            var threshold = 1;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 1 }
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by server"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdValidServerAndClientKeyMeetingThreshold()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var threshold = 1;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 1 }
            };

            var wantSigners = new string[1]
            {
                clientKeypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTxThresholdValidServerAndMultipleClientKeyMeetingThreshold()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);

            var threshold = 3;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 1 },
                { client2Keypair.Address, 2 }
            };

            var wantSigners = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdValidServerAndMultipleClientKeyMeetingThresholdSomeUnused()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();
            var client3Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);

            var threshold = 3;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 1 },
                { client2Keypair.Address, 2 },
                { client3Keypair.Address, 2 }
            };

            var wantSigners = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdInvalidServerAndMultipleClientKeyNotMeetingThreshold()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();
            var client3Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);

            var threshold = 10;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 1 },
                { client2Keypair.Address, 2 },
                { client3Keypair.Address, 2 }
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();
            }
            catch(Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Signers with weight 3 do not meet threshold 10"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdInvalidClientKeyUnrecognized()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();
            var client3Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);
            transaction.Sign(client3Keypair);

            var threshold = 3;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 1 },
                { client2Keypair.Address, 2 },
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction has unrecognized signatures"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdInvalidNoSigners()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();
            var client3Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);
            transaction.Sign(client3Keypair);

            var threshold = 3;
            var signerSummary = new Dictionary<string, int>()
            {
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("signers must be non-empty"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThresholdWeightsAddToMoreThan8Bits()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);

            var threshold = 1;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 255 },
                { client2Keypair.Address, 1 },
            };

            var wantSigners = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidServer()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            //transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var threshold = 1;
            var signerSummary = new Dictionary<string, int>()
            {
                { clientKeypair.Address, 255 },
            };

            var wantSigners = new string[1]
            {
                clientKeypair.Address,
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by server"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersValidServerAndClientMasterKey()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var signers = new string[1]
            {
                clientKeypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test());

            Assert.AreEqual(clientKeypair.Address, signersFound[0]);
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidServerAndNoClient()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            var signers = new string[1]
            {
                clientKeypair.Address
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by client"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidServerAndUnrecognizedClient()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var unrecognizedKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(unrecognizedKeypair);

            var signers = new string[1]
            {
                clientKeypair.Address
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by client"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersValidServerAndMultipleClientSigners()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(client2Keypair);

            var signers = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var wantSigners = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersValidServerAndMultipleClientSignersReverseOrder()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(client2Keypair);
            transaction.Sign(clientKeypair);

            var signers = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var wantSigners = new string[2]
            {
                clientKeypair.Address,
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersValidServerAndClientSignersNotMasterKey()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(client2Keypair);

            var signers = new string[1]
            {
                client2Keypair.Address
            };

            var wantSigners = new string[1]
            {
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersValidServerAndClientSignersIgnoresServerSigner()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(client2Keypair);

            var signers = new string[2]
            {
                serverKeypair.Address,
                client2Keypair.Address
            };

            var wantSigners = new string[1]
            {
                client2Keypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidServerNoClientSignersIgnoresServerSigner()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            var signers = new string[2]
            {
                serverKeypair.Address,
                client2Keypair.Address
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by client"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersValidServerAndClientSignersIgnoresDuplicateSigner()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var signers = new string[2]
            {
                clientKeypair.Address,
                clientKeypair.Address
            };

            var wantSigners = new string[1]
            {
                clientKeypair.Address
            };

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();

            for (int i = 0; i < wantSigners.Length; i++)
            {
                Assert.AreEqual(signersFound[i], wantSigners[i]);
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidServerAndClientSignersIgnoresDuplicateSignerInError()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            var client2Keypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(client2Keypair);

            var signers = new string[2]
            {
                clientKeypair.Address,
                clientKeypair.Address
            };

            var wantSigners = new string[1]
            {
                client2Keypair.Address
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction not signed by client"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidServerAndClientSignersFailsDuplicateSignatures()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);
            transaction.Sign(clientKeypair);

            var signers = new string[1]
            {
                clientKeypair.Address
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction has unrecognized signatures"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionSignersInvalidNoSigners()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder("testserver auth", base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var signers = new string[0]
            {
            };

            try
            {
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("signers must be non-empty"));
            }
        }
    }
}