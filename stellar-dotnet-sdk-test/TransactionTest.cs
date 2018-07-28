using System;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
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
        public void TestBuilderSuccessTestnet()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var sequenceNumber = 2908908335136768L;
            var account = new Account(source, sequenceNumber);
            var transaction = new Transaction.Builder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .Build();

            transaction.Sign(source);

            Assert.AreEqual(
                "AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAEqBfIAAAAAAAAAAABtoeVYgAAAEDLki9Oi700N60Lo8gUmEFHbKvYG4QSqXiLIt9T0ru2O5BphVl/jR9tYtHAD+UeDYhgXNgwUxqTEu1WukvEyYcD",
                transaction.ToEnvelopeXdrBase64());

            Assert.AreEqual(transaction.SourceAccount, source);
            Assert.AreEqual(transaction.SequenceNumber, sequenceNumber + 1);
            Assert.AreEqual(transaction.Fee, 100);
        }
        
        [TestMethod]
        public void TestFromXdr()
        {
            Transaction transaction = Transaction.FromEnvelopeXdr("AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAA7eBSYbzcL5UKo7oXO24y1ckX+XuCtkDsyNHOp1n1bxAAAAAEqBfIAAAAAAAAAAABtoeVYgAAAEDLki9Oi700N60Lo8gUmEFHbKvYG4QSqXiLIt9T0ru2O5BphVl/jR9tYtHAD+UeDYhgXNgwUxqTEu1WukvEyYcD");
            Transaction transaction2 = Transaction.FromEnvelopeXdr(transaction.ToEnvelopeXdr());


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
        public void TestBuilderMemoText()
        {
            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source, 2908908335136768);
            var transaction = new Transaction.Builder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddMemo(Memo.Text("Hello world!"))
                .Build();

            transaction.Sign(source);

            Assert.AreEqual(
                "AAAAAF7FIiDToW1fOYUFBC0dmyufJbFTOa2GQESGz+S2h5ViAAAAZAAKVaMAAAABAAAAAAAAAAEAAAAMSGVsbG8gd29ybGQhAAAAAQAAAAAAAAAAAAAAAO3gUmG83C+VCqO6FztuMtXJF/l7grZA7MjRzqdZ9W8QAAAABKgXyAAAAAAAAAAAAbaHlWIAAABAxzofBhoayuUnz8t0T1UNWrTgmJ+lCh9KaeOGu2ppNOz9UGw0abGLhv+9oWQsstaHx6YjwWxL+8GBvwBUVWRlBQ==",
                transaction.ToEnvelopeXdrBase64());

            Transaction transaction2 = Transaction.FromEnvelopeXdr(transaction.ToEnvelopeXdr());

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

            var account = new Account(source, 2908908335136768L);
            var transaction = new Transaction.Builder(account)
                .AddOperation(new CreateAccountOperation.Builder(destination, "2000").Build())
                .AddTimeBounds(new TimeBounds(42, 1337))
                .AddMemo(Memo.Hash("abcdef"))
                .Build();

            transaction.Sign(source);

            // Convert transaction to binary XDR and back again to make sure timebounds are correctly de/serialized.
            var bytes = transaction.ToEnvelopeXdrBase64().ToCharArray();
            var xdrDataInputStream = new XdrDataInputStream(Convert.FromBase64CharArray(bytes, 0, bytes.Length));

            var decodedTransaction = XdrTransaction.Decode(xdrDataInputStream);

            Assert.AreEqual(decodedTransaction.TimeBounds.MinTime.InnerValue, 42);
            Assert.AreEqual(decodedTransaction.TimeBounds.MaxTime.InnerValue, 1337);

            Transaction transaction2 = Transaction.FromEnvelopeXdr(transaction.ToEnvelopeXdr());
            
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
        public void TestBuilderSuccessPublic()
        {
            Network.UsePublicNetwork();

            // GBPMKIRA2OQW2XZZQUCQILI5TMVZ6JNRKM423BSAISDM7ZFWQ6KWEBC4
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination = KeyPair.FromAccountId("GDW6AUTBXTOC7FIKUO5BOO3OGLK4SF7ZPOBLMQHMZDI45J2Z6VXRB5NR");

            var account = new Account(source, 2908908335136768L);
            var transaction = new Transaction.Builder(account)
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

            var account = new Account(source, 0L);
            var transaction = new Transaction.Builder(account)
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

            var account = new Account(source, 2908908335136768L);
            var transaction = new Transaction.Builder(account)
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

            var account = new Account(source, 2908908335136768L);
            var transaction = new Transaction.Builder(account)
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

            var account = new Account(source, 2908908335136768L);
            var transaction = new Transaction.Builder(account)
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

            var account = new Account(source, 2908908335136768L);
            try
            {
                var unused = new Transaction.Builder(account).Build();
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
                var account = new Account(source, 2908908335136768L);
                new Transaction.Builder(account)
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
    }
}