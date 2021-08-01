using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class WebAuthenticationTest
    {
        private const string HomeDomain = "thisisatest.sandbox.anchor.anchordomain.com";
        private const string WebAuthDomain = "thisisatest.sandbox.anchor.webauth.com";
        private const string ClientDomain = "thisisatest.sandbox.anchor.client.com";

        private string ManageDataOperationName => $"{HomeDomain} auth";

        [TestMethod]
        public void TestBuildChallengeTransaction()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";
            Network.UseTestNetwork();
            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientAccountId, HomeDomain, WebAuthDomain);

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
            var clientAccountId = KeyPair.FromAccountId("GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF");

            var nonce = new byte[48];
            Array.Clear(nonce, 0, nonce.Length);

            var now = new DateTimeOffset();
            var duration = TimeSpan.FromMinutes(10.0);

            var tx = WebAuthentication
                .BuildChallengeTransaction(serverKeypair, clientAccountId, HomeDomain, WebAuthDomain, nonce, now, duration, Network.Test());

            var serializedTx = tx.ToEnvelopeXdrBase64();
            var back = Transaction.FromEnvelopeXdr(serializedTx);

            var timeout = back.TimeBounds.MaxTime - back.TimeBounds.MinTime;
            Assert.AreEqual(600, timeout);

            CheckAccounts(back, serverKeypair);
            CheckOperation(back, clientAccountId.Address);
        }

        [TestMethod]
        public void TestBuildChallengeTransactionFailsWithMuxedAccount()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId =
                MuxedAccountMed25519.FromMuxedAccountId(
                    "MAAAAAAAAAAAJURAAB2X52XFQP6FBXLGT6LWOOWMEXWHEWBDVRZ7V5WH34Y22MPFBHUHY");

            var nonce = new byte[48];
            Array.Clear(nonce, 0, nonce.Length);

            var now = new DateTimeOffset();
            var duration = TimeSpan.FromMinutes(10.0);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                var tx = WebAuthentication
                    .BuildChallengeTransaction(serverKeypair, clientAccountId.Address, HomeDomain, WebAuthDomain, nonce, now, duration,
                        Network.Test());
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionReturnsTrueForValidTransaction()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Sign(clientKeypair);

            Assert.IsTrue(WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now));
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfSequenceIsNotZero()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var nonce = new byte[64];
            var tx = new TransactionBuilder(new Account(serverKeypair.AccountId, 0))
                .AddOperation(new ManageDataOperation.Builder("NET auth", nonce).Build())
                .Build();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfServerAccountIdIsDifferent()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, KeyPair.Random().AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfTransactionHasNoManageDataOperation()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
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
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfOperationHasNoSourceAccount()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;
            var nonce = new byte[64];
            var tx = new TransactionBuilder(new Account(serverKeypair.AccountId, -1))
                .AddOperation(new ManageDataOperation.Builder("NET auth", nonce).Build())
                .Build();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {

                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfOperationDataIsNotBase64Encoded()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();
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
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfNotSignedByServer()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Signatures.Clear();
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfSignedByServerOnDifferentNetwork()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now, network: Network.Public());
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfNotSignedByClient()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfSignedByClientOnDifferentNetwork()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Sign(clientKeypair, Network.Public());

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now, network: Network.Test());
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfItsTooEarly()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
                {
                    WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now.Subtract(TimeSpan.FromDays(1.0)));
                });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfItsTooLate()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair.AccountId, HomeDomain, WebAuthDomain, now: now);
            tx.Sign(clientKeypair);

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
                {
                    WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now.Add(TimeSpan.FromDays(1.0)));
                });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfServerIsMuxedAccount()
        {
            // It's impossible to build a wrong tx from our api. Use an xdr instead.
            var txXdr = "AAAAAgAAAQAAAAAAAAAE0rqb5mZeN3cTjZYz9BOSuxs4tkP5296i8kJKWXS13pWGAAAAZAAAAAAAAAAAAAAAAQAAAABerG8LAAAAAF6scDcAAAAAAAAAAQAAAAEAAAAA13Pc/rMj75EaJFmzR1eWVHBeJuoq+8FinXpG7DXEsvoAAAAKAAAACE5FVCBhdXRoAAAAAQAAAEBIRmxJQi94UFFsYTBaSzNRamx3akFUL25JS3pUeFFFK1hFVE9EQkIzZHpOQWRsR0svOGJnbFBydSttaEJpNzdEAAAAAAAAAAK13pWGAAAAQGlkGeaHtcnaSyQP4NSU/CaRC6rUd7qXvVlJc/3TuWmY0kAC9/mXmLtnzFn2Hz+0cwVi1+wwtxfboxIHOABIsg81xLL6AAAAQB23cGeF7SR9bZEf6rRh+ck7h6PqvUQFDDDI3qE09y19SdvMWMs5Ksthm//dXMZE7+QJbKqxpJbpKC2klMTZJQ0=";

            var serverKeypair =
                KeyPair.FromAccountId("GC5JXZTGLY3XOE4NSYZ7IE4SXMNTRNSD7HN55IXSIJFFS5FV32KYM6PH");
            var now = DateTimeOffset.Now;
            var tx = Transaction.FromEnvelopeXdr(txXdr);
            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now.Add(TimeSpan.FromDays(1.0)));
            });
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionThrowsIfClientIsMuxedAccount()
        {
            // It's impossible to build a wrong tx from our api. Use an xdr instead.
            var txXdr = "AAAAAgAAAABe3OrOugPm3BfyQ9xP+UUMEk8hiM0WwMSpVN9FIOxuXgAAAGQAAAAAAAAAAAAAAAEAAAAAXqxwZQAAAABerHGRAAAAAAAAAAEAAAABAAABAAAAAAAAAATSXtzqzroD5twX8kPcT/lFDBJPIYjNFsDEqVTfRSDsbl4AAAAKAAAACE5FVCBhdXRoAAAAAQAAAEAvU0VaNWppQjRZTXZTYlBNN1VobzJ6QmxqcVBiN0IyRDVJbGx6NEZxUWh4SmhHVmJWT0VsdHhyRlE5ZUNIL2RLAAAAAAAAAAIg7G5eAAAAQGKw8yxSA/tnK34nv6VIQ/r1bazvm3vInbU4dpSersY/7uN5MKZEKIMbioevHIpYZ6pwJdm7qRPbGj9YyCU+BQsNYg7iAAAAQKCdrKY6g6pEg/DfhOfOyRU8cKcg1qVSQwekXlKkQTzw/MpyLqYYRlxP5Z+P0TLDxmCn8KyawafIum24hvE11ws=";

            var serverKeypair =
                KeyPair.FromAccountId("GBPNZ2WOXIB6NXAX6JB5YT7ZIUGBETZBRDGRNQGEVFKN6RJA5RXF4SJ2");
            var now = DateTimeOffset.Now;
            var tx = Transaction.FromEnvelopeXdr(txXdr);
            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: now.Add(TimeSpan.FromDays(1.0)));
            });
        }

        private void CheckAccounts(Transaction tx, KeyPair serverKeypair)
        {

            Assert.AreEqual(0, tx.SequenceNumber);
            Assert.AreEqual(serverKeypair.AccountId, tx.SourceAccount.AccountId);
        }

        private void CheckOperation(Transaction tx, string clientAccountId)
        {
            Assert.AreEqual(2, tx.Operations.Length);
            var operation = tx.Operations[0] as ManageDataOperation;
            Assert.IsNotNull(operation);
            Assert.AreEqual($"{HomeDomain} auth", operation.Name);
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Challenge transaction sequence number must be 0"));
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
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).Build();
            var transaction = new TransactionBuilder(txSource).AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            try
            {
                var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test());
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();
            }
            catch (Exception exception)
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionThreshold(transaction, serverKeypair.AccountId, threshold, signerSummary, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test());

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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

            var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();

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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();
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

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
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
                var signersFound = WebAuthentication.VerifyChallengeTransactionSigners(transaction, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, Network.Test()).ToList();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("signers must be non-empty"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionNotValidSubsequentOperation()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var notValidOperation = new PaymentOperation.Builder(KeyPair.Random(), new AssetTypeNative(), "50").SetSourceAccount(opSource.KeyPair).Build();

            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddOperation(notValidOperation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            try
            {
                WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("The transaction has operations that are not of type 'manageData'"));
            }
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionNotValidSubsequentDataOperation()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var notValidOperation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(KeyPair.Random()).Build();

            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddOperation(notValidOperation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            try
            {
                WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, WebAuthDomain, Network.Test());
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("The transaction has operations that are unrecognized"));
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionBadHomeDomain()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";

            Network.UseTestNetwork();
            try
            {
                var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientAccountId, HomeDomain, WebAuthDomain);
                WebAuthentication.ReadChallengeTransaction(tx, serverKeypair.AccountId, $"{HomeDomain}bad", WebAuthDomain);
            }
            catch (InvalidWebAuthenticationException e)
            {
                Assert.AreEqual(e.Message, "Invalid homeDomains: the transaction's operation key name does not match the expected home domain");
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionNoHomeDomain()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";

            Network.UseTestNetwork();
            try
            {
                var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientAccountId, HomeDomain, WebAuthDomain);
                WebAuthentication.ReadChallengeTransaction(tx, serverKeypair.AccountId, new string[0], WebAuthDomain);
            }
            catch (InvalidWebAuthenticationException e)
            {
                Assert.AreEqual(e.Message, "Invalid homeDomains: a home domain must be provided for verification");
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionNoTransaction()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";

            Network.UseTestNetwork();
            try
            {
                WebAuthentication.ReadChallengeTransaction(null, serverKeypair.AccountId, HomeDomain, WebAuthDomain);
            }
            catch (InvalidWebAuthenticationException e)
            {
                Assert.AreEqual(e.Message, "Challenge transaction cannot be null");
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionExpiredTimeBounds()
        {
            var serverKeypair = KeyPair.Random();
            var clientAccountId = "GBDIT5GUJ7R5BXO3GJHFXJ6AZ5UQK6MNOIDMPQUSMXLIHTUNR2Q5CFNF";

            Network.UseTestNetwork();
            try
            {
                var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientAccountId, HomeDomain, WebAuthDomain);
                WebAuthentication.ReadChallengeTransaction(tx, serverKeypair.AccountId, HomeDomain, WebAuthDomain, now: DateTimeOffset.Now.Subtract(new TimeSpan(0, 20, 0)));
            }
            catch (InvalidWebAuthenticationException e)
            {
                Assert.AreEqual(e.Message, "Challenge transaction expired");
            }
        }

        [TestMethod]
        public void TestReadChallengeTransactionNoWebAuthDomain()
        {
            Network.Use(Network.Test());

            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            var txSource = new Account(serverKeypair.Address, -1);
            var opSource = new Account(clientKeypair.Address, 0);

            var plainTextBytes = Encoding.UTF8.GetBytes(new string(' ', 48));
            var base64Data = Encoding.ASCII.GetBytes(Convert.ToBase64String(plainTextBytes));

            var operation = new ManageDataOperation.Builder(ManageDataOperationName, base64Data).SetSourceAccount(opSource.KeyPair).Build();
            var transaction = new TransactionBuilder(txSource)
                .AddOperation(operation)
                .AddTimeBounds(new TimeBounds(DateTimeOffset.Now, DateTimeOffset.Now.AddSeconds(1000)))
                .Build();

            transaction.Sign(serverKeypair);
            transaction.Sign(clientKeypair);

            var readTransactionID = WebAuthentication.ReadChallengeTransaction(transaction, serverKeypair.AccountId, HomeDomain, "", Network.Test());

            Assert.AreEqual(clientKeypair.AccountId, readTransactionID);
        }

        [TestMethod]
        public void TestVerifyChallengeTransactionWithClientDomain()
        {
            var serverKeypair = KeyPair.Random();
            var clientKeypair = KeyPair.Random();

            Network.UseTestNetwork();

            var now = DateTimeOffset.Now;

            var tx = WebAuthentication.BuildChallengeTransaction(serverKeypair, clientKeypair, HomeDomain, WebAuthDomain, now: now, clientDomain: ClientDomain, clientSigningKey: clientKeypair);
            var manageDataOperation = (ManageDataOperation)tx.Operations[2];

            var signers = new List<string>();
            signers.Add(serverKeypair.AccountId);
            signers.Add(clientKeypair.AccountId);

            Assert.AreEqual(manageDataOperation.Name, "client_domain");

            Assert.ThrowsException<InvalidWebAuthenticationException>(() =>
            {
                WebAuthentication.VerifyChallengeTransactionSigners(tx, serverKeypair.AccountId, signers, HomeDomain, WebAuthDomain, now: now);
            });
        }
    }
}
