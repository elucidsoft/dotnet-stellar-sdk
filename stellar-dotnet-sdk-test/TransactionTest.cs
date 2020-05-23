using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.xdr;
using Memo = stellar_dotnet_sdk.Memo;
using TimeBounds = stellar_dotnet_sdk.TimeBounds;
using Transaction = stellar_dotnet_sdk.Transaction;
using XdrTransaction = stellar_dotnet_sdk.xdr.Transaction;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class TransactionTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Network.UseTestNetwork();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Network.Use(null);
        }

        [TestMethod]
        public void TestOldTransactionBuilder()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var sequenceNumber = 2908908335136768L;
            var account = new Account(source.AccountId, sequenceNumber);
            // Test that we do not break the old api. So suppress the warning for now.
#pragma warning disable 0618
            var transaction = new Transaction.Builder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();
#pragma warning restore 0618
            transaction.Sign(source);

            Assert.AreEqual(
                "AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAEqBfIAAAAAAAAAAABtoeVYgAAAEDLki9Oi700N60Lo8gUmEFHbKvYG4QSqXiLIt9T0ru2O5BphVl/jR9tYtHAD+UeDYhgXNgwUxqTEu1WukvEyYcD",
                transaction.ToEnvelopeXdrBase64());

            Assert.AreEqual(transaction.SourceAccount.AccountId, source.AccountId);
            Assert.AreEqual(transaction.SequenceNumber, sequenceNumber + 1);
            Assert.AreEqual(transaction.Fee, 100U);
        }

        [TestMethod]
        public void TestBuilderSuccessTestnet()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var sequenceNumber = 2908908335136768L;
            var account = new Account(source.AccountId, sequenceNumber);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();

            transaction.Sign(source);

            Assert.AreEqual(
                "AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAEqBfIAAAAAAAAAAABtoeVYgAAAEDLki9Oi700N60Lo8gUmEFHbKvYG4QSqXiLIt9T0ru2O5BphVl/jR9tYtHAD+UeDYhgXNgwUxqTEu1WukvEyYcD",
                transaction.ToEnvelopeXdrBase64());

            Assert.AreEqual(transaction.SourceAccount.AccountId, source.AccountId);
            Assert.AreEqual(transaction.SequenceNumber, sequenceNumber + 1);
            Assert.AreEqual(transaction.Fee, 100U);
        }

        [TestMethod]
        public void TestFromXdr()
        {
            var transaction = Transaction.FromEnvelopeXdr("AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAEqBfIAAAAAAAAAAABtoeVYgAAAEDLki9Oi700N60Lo8gUmEFHbKvYG4QSqXiLIt9T0ru2O5BphVl/jR9tYtHAD+UeDYhgXNgwUxqTEu1WukvEyYcD");
            var transaction2 = Transaction.FromEnvelopeXdr(transaction.ToEnvelopeXdr());

            Assert.AreEqual(transaction.SourceAccount.AccountId, transaction2.SourceAccount.AccountId);
            Assert.AreEqual(transaction.SequenceNumber, transaction2.SequenceNumber);
            Assert.AreEqual(transaction.Fee, transaction2.Fee);
            Assert.AreEqual(
                ((CreateAccountOperation)transaction.Operations[0]).StartingBalance,
                ((CreateAccountOperation)transaction2.Operations[0]).StartingBalance
            );

            CollectionAssert.AreEqual(transaction.Signatures, transaction2.Signatures);
        }

        [TestMethod]
        public void TestFromXdrWithMemo()
        {
            var transaction = Transaction.FromEnvelopeXdr(
                "AAAAACq1Ixcw1fchtF5aLTSw1zaYAYjb3WbBRd4jqYJKThB9AAAAZAA8tDoAAAALAAAAAAAAAAEAAAAZR29sZCBwYXltZW50IGZvciBzZXJ2aWNlcwAAAAAAAAEAAAAAAAAAAQAAAAARREGslec48mbJJygIwZoLvRtL6/gGL4ss2TOpnOUOhgAAAAFHT0xEAAAAACq1Ixcw1fchtF5aLTSw1zaYAYjb3WbBRd4jqYJKThB9AAAAADuaygAAAAAAAAAAAA==");
            Assert.AreEqual(1, transaction.Operations.Length);
            Assert.IsInstanceOfType(transaction.Memo, typeof(MemoText));
            var op = transaction.Operations[0];
            Assert.IsNull(op.SourceAccount);
            Assert.IsInstanceOfType(op, typeof(PaymentOperation));
            var payment = op as PaymentOperation;
            Assert.IsNotNull(payment);
            Assert.AreEqual("100", payment.Amount);
            var asset = payment.Asset as AssetTypeCreditAlphaNum;
            Assert.IsNotNull(asset);
            Assert.AreEqual("GOLD", asset.Code);
        }

        [TestMethod]
        public void TestBuilderMemoText()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddMemo(Memo.Text("Hello world!"))
                .Build();

            transaction.Sign(source);

            Assert.AreEqual(
                "AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAEAAAAMSGVsbG8gd29ybGQhAAAAAQAAAAAAAAAAAAAAAO3gUmG83C+VCqO6FztuMtXJF/l7grZA7MjRzqdZ9W8QAAAABKgXyAAAAAAAAAAAAbaHlWIAAABAxzofBhoayuUnz8t0T1UNWrTgmJ+lCh9KaeOGu2ppNOz9UGw0abGLhv+9oWQsstaHx6YjwWxL+8GBvwBUVWRlBQ==",
                transaction.ToEnvelopeXdrBase64());

            var transaction2 = Transaction.FromEnvelopeXdr(transaction.ToEnvelopeXdr());

            Assert.AreEqual(transaction.SourceAccount.AccountId, transaction2.SourceAccount.AccountId);
            Assert.AreEqual(transaction.SequenceNumber, transaction2.SequenceNumber);
            Assert.AreEqual(transaction.Memo, transaction2.Memo);
            Assert.AreEqual(transaction.Fee, transaction2.Fee);
            Assert.AreEqual(
                ((CreateAccountOperation)transaction.Operations[0]).StartingBalance,
                ((CreateAccountOperation)transaction2.Operations[0]).StartingBalance
            );
        }

        [TestMethod]
        public void TestBuilderTimeBounds()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddTimeBounds(new TimeBounds(42, 1337))
                .AddMemo(Memo.Hash("abcdef"))
                .Build();

            transaction.Sign(source);

            // Convert transaction to binary XDR and back again to make sure timebounds are correctly de/serialized.
            var bytes = transaction.ToEnvelopeXdrBase64().ToCharArray();
            var xdrDataInputStream = new XdrDataInputStream(Convert.FromBase64CharArray(bytes, 0, bytes.Length));

            var decodedTransaction = XdrTransaction.Decode(xdrDataInputStream);

            Assert.AreEqual(decodedTransaction.TimeBounds.MinTime.InnerValue.InnerValue, 42U);
            Assert.AreEqual(decodedTransaction.TimeBounds.MaxTime.InnerValue.InnerValue, 1337U);

            var transaction2 = Transaction.FromEnvelopeXdr(transaction.ToEnvelopeXdr());

            Assert.AreEqual(transaction.SourceAccount.AccountId, transaction2.SourceAccount.AccountId);
            Assert.AreEqual(transaction.SequenceNumber, transaction2.SequenceNumber);
            Assert.AreEqual(transaction.Memo, transaction2.Memo);
            Assert.AreEqual(transaction.TimeBounds, transaction2.TimeBounds);
            Assert.AreEqual(transaction.Fee, transaction2.Fee);
            Assert.AreEqual(
                ((CreateAccountOperation)transaction.Operations[0]).StartingBalance,
                ((CreateAccountOperation)transaction2.Operations[0]).StartingBalance
            );
        }

        [TestMethod]
        public void TestBuilderFee()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .SetFee(173)
                .Build();

            // Convert transaction to binary XDR and back again to make sure fee is correctly de/serialized.
            var bytes = transaction.ToUnsignedEnvelopeXdrBase64().ToCharArray();
            var xdrDataInputStream = new XdrDataInputStream(Convert.FromBase64CharArray(bytes, 0, bytes.Length));

            var decodedTransaction = XdrTransaction.Decode(xdrDataInputStream);

            Assert.AreEqual(decodedTransaction.Fee.InnerValue, 173 * 2U);

            var transaction2 = Transaction.FromEnvelopeXdr(transaction.ToUnsignedEnvelopeXdr());

            Assert.AreEqual(transaction.SourceAccount.AccountId, transaction2.SourceAccount.AccountId);
            Assert.AreEqual(transaction.SequenceNumber, transaction2.SequenceNumber);
            Assert.AreEqual(transaction.Memo, transaction2.Memo);
            Assert.AreEqual(transaction.TimeBounds, transaction2.TimeBounds);
            Assert.AreEqual(transaction.Fee, transaction2.Fee);
            Assert.AreEqual(
                ((CreateAccountOperation)transaction.Operations[0]).StartingBalance,
                ((CreateAccountOperation)transaction2.Operations[0]).StartingBalance
            );
            Assert.AreEqual(
                ((CreateAccountOperation)transaction.Operations[1]).StartingBalance,
                ((CreateAccountOperation)transaction2.Operations[1]).StartingBalance
            );
        }

        [TestMethod]
        public void TestBuilderSuccessPublic()
        {
            Network.UsePublicNetwork();

            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();

            transaction.Sign(source);

            Assert.AreEqual(
                "AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAEqBfIAAAAAAAAAAABtoeVYgAAAEDzfR5PgRFim5Wdvq9ImdZNWGBxBWwYkQPa9l5iiBdtPLzAZv6qj+iOfSrqinsoF0XrLkwdIcZQVtp3VRHhRoUE",
                transaction.ToEnvelopeXdrBase64());
        }

        [TestMethod]
        public void TestSha256HashSigning()
        {
            Network.UsePublicNetwork();

            var source = KeyPair.FromAccountId("GBBM6BKZPEHWYO3E3YKREDPQXMS4VK35YLNU7NFBRI26RAN7GI5POFBB");
            var destination = KeyPair.FromAccountId("GDJJRRMBK4IWLEPJGIE6SXD2LP7REGZODU7WDC3I2D6MR37F4XSHBKX2");

            var account = new Account(source.AccountId, 0L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new PaymentOperation.Builder(destination, new AssetTypeNative(), "2000").Build())
                .Build();

            var preimage = new byte[64];

            var rngCsp = new RNGCryptoServiceProvider();

            rngCsp.GetBytes(preimage);
            var hash = Util.Hash(preimage);

            transaction.Sign(preimage);

            Assert.IsTrue(transaction.Signatures[0].Signature.InnerValue.Equals(preimage));

            var length = hash.Length;
            var rangeHashCopy = hash.Skip(length - 4).Take(4).ToArray();

            Assert.IsTrue(transaction.Signatures[0].Hint.InnerValue.SequenceEqual(rangeHashCopy));
        }

        [TestMethod]
        public void TestToBase64EnvelopeXdrBuilderNoSignatures()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();

            try
            {
                transaction.ToEnvelopeXdrBase64();
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Transaction must be signed by at least one signer."));
            }
        }

        [TestMethod]
        public void TestToUnsignedEnvelopeXdr()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();
            try
            {
                transaction.ToUnsignedEnvelopeXdr();
            }
            catch (Exception exception)
            {
                Assert.Fail("Expected no exception, but got: " + exception.Message);
            }
        }

        [TestMethod]
        public void TestToUnsignedEnvelopeXdrBase64()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source.AccountId, 2908908335136768L);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();
            try
            {
                transaction.ToUnsignedEnvelopeXdrBase64();
            }
            catch (Exception exception)
            {
                Assert.Fail("Expected no exception, but got: " + exception.Message);
            }
        }

        [TestMethod]
        public void TestNoOperations()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");

            var account = new Account(source.AccountId, 2908908335136768L);
            try
            {
                var unused = new TransactionBuilder(account).Build();
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("At least one operation required"));
                Assert.AreEqual(2908908335136768L, account.SequenceNumber);
            }
        }

        [TestMethod]
        public void TestTryingToAddMemoTwice()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            try
            {
                var account = new Account(source.AccountId, 2908908335136768L);
                new TransactionBuilder(account)
                    .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                    .AddMemo(Memo.None())
                    .AddMemo(Memo.None());
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("Memo has been already added."));
            }
        }

        [TestMethod]
        public void TestExplicitNetworkArgument()
        {
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var sequenceNumber = 2908908335136768L;
            var account = new Account(source.AccountId, sequenceNumber);
            var transaction = new TransactionBuilder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();

            Network.UsePublicNetwork();
            var publicNetworkHash = transaction.Hash();

            Network.UseTestNetwork();
            var testNetworkHash = transaction.Hash();

            Assert.IsFalse(testNetworkHash.SequenceEqual(publicNetworkHash));

            var network = Network.Public();
            var explicitPublicNetworkHash = transaction.Hash(network);

            Assert.IsTrue(publicNetworkHash.SequenceEqual(explicitPublicNetworkHash));
        }

        [TestMethod]
        public void TestFromAccountResponse()
        {
            var response = new AccountResponse("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR", 123);
            var transaction = new TransactionBuilder(response)
                .AddOperation(new CreateAccountOperation.Builder(response.KeyPair, "2000").Build())
                .Build();

            Assert.IsNotNull(transaction);
        }

        [TestMethod]
        public void TestFromXdrWithMemoId()
        {
            // https://github.com/elucidsoft/dotnet-stellar-sdk/issues/208
            var tx = Transaction.FromEnvelopeXdr(
                "AAAAAEdL24Ttos6RnqXCsn8duaV035/QZSC9RXw29IknigHpAAAD6AFb56cAAukDAAAAAQAAAAAAAAAAAAAAAF20fKAAAAACjCiEBz2CpG0AAAABAAAAAAAAAAEAAAAADq+QhtWseqhtnwRIFyZRdLMOVtIqzkujfzUQ22rwZuEAAAAAAAAAAGZeJLcAAAAAAAAAASeKAekAAABAE+X7cGoBhuJ5SDB8WH2B1ZA2RrWIXxGtx+n6wE5d/EggDTpZhRm92b33QqjPUFOfcZ+zbcM+Ny0WR2vcYHEXDA==");
            Assert.AreEqual("GBDUXW4E5WRM5EM6UXBLE7Y5XGSXJX472BSSBPKFPQ3PJCJHRIA6SH4C", tx.SourceAccount.AccountId);
        }

        [TestMethod]
        public void TestToUnsignedEnvelopeXdrWithSignatures()
        {
            var network = new Network("Standalone Network ; February 2017");
            var source = KeyPair.FromSecretSeed(network.NetworkId);
            var txSource = KeyPair.Random();
            var account = new Account(txSource, 7);
            var destination = KeyPair.FromAccountId("GDQERENWDDSQZS7R7WKHZI3BSOYMV3FSWR7TFUYFTKQ447PIX6NREOJM");
            var amount = "2000";
            var asset = new AssetTypeNative();
            var tx = new TransactionBuilder(account)
                .SetFee(100)
                .AddTimeBounds(new TimeBounds(0, 0))
                .AddOperation(
                    new PaymentOperation.Builder(destination, asset, amount).Build())
                .AddMemo(new MemoText("Happy birthday!"))
                .Build();

            tx.Sign(KeyPair.Random());

            try
            {
                tx.ToUnsignedEnvelopeXdr();
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, "Transaction must not be signed. Use ToEnvelopeXDR.");
            }
        }
    }
}