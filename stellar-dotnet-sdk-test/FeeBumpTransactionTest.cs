using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class FeeBumpTransactionTest
    {
        public long BaseFee { get; set; }
        public string NetworkPassphrase { get; set; }
        public Network Network { get; set; }
        public KeyPair InnerSource { get; set; }
        public Account InnerAccount { get; set; }
        public KeyPair Destination { get; set; }
        public string Amount { get; set; }
        public Asset Asset { get; set; }
        public Transaction InnerTransaction { get; set; }
        public KeyPair FeeSource { get; set; }
        public FeeBumpTransaction Transaction { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            BaseFee = 100;
            NetworkPassphrase = "Standalone Network ; February 2017";
            Network = new Network(NetworkPassphrase);
            InnerSource = KeyPair.FromSecretSeed(Network.NetworkId);
            InnerAccount = new Account(InnerSource.AccountId, 7);
            Destination = KeyPair.FromAccountId("GDQERENWDDSQZS7R7WKHZI3BSOYMV3FSWR7TFUYFTKQ447PIX6NREOJM");
            Amount = "2000.0000000";
            Asset = new AssetTypeNative();
            InnerTransaction = new TransactionBuilder(InnerAccount)
                .SetFee(100)
                .AddTimeBounds(new TimeBounds(0, 0))
                .AddOperation(
                    new PaymentOperation.Builder(Destination, Asset, Amount)
                        .Build())
                .AddMemo(new MemoText("Happy birthday!"))
                .Build();
            InnerTransaction.Sign(InnerSource, Network);
            FeeSource = KeyPair.FromSecretSeed("SB7ZMPZB3YMMK5CUWENXVLZWBK4KYX4YU5JBXQNZSK2DP2Q7V3LVTO5V");
            Transaction = TransactionBuilder.BuildFeeBumpTransaction(FeeSource, InnerTransaction, 100);
        }

        [TestMethod]
        public void TestLessThanInnerBaseFeeRate()
        {
            try
            {
                var transaction = TransactionBuilder.BuildFeeBumpTransaction(FeeSource, InnerTransaction, 50);
            }
            catch (Exception e)
            {
                var innerOps = InnerTransaction.Operations.Length;
                var innerBaseFeeRate = InnerTransaction.Fee / innerOps;

                Assert.AreEqual(e.Message, $"Invalid fee, it should be at least {innerBaseFeeRate} stroops");
            }
        }

        [TestMethod]
        public void TestLessThanBaseFee()
        {
            try
            {
                var transaction = TransactionBuilder.BuildFeeBumpTransaction(FeeSource, InnerTransaction, 50);
            }
            catch (Exception e)
            {
                Assert.AreEqual(e.Message, $"Invalid fee, it should be at least {BaseFee} stroops");
            }
        }

        [TestMethod]
        public void TestBuildFromTransactionEnvelope()
        {
            Transaction.Sign(FeeSource, Network);
            Assert.AreEqual(FeeSource, Transaction.FeeSource);
            Assert.AreEqual(200, Transaction.Fee);

            var expectedXdr =
                "AAAABQAAAADgSJG2GOUMy/H9lHyjYZOwyuyytH8y0wWaoc596L+bEgAAAAAAAADIAAAAAgAAAABzdv3ojkzWHMD7KUoXhrPx0GH18vHKV0ZfqpMiEblG1gAAAGQAAAAAAAAACAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAA9IYXBweSBiaXJ0aGRheSEAAAAAAQAAAAAAAAABAAAAAOBIkbYY5QzL8f2UfKNhk7DK7LK0fzLTBZqhzn3ov5sSAAAAAAAAAASoF8gAAAAAAAAAAAERuUbWAAAAQK933Dnt1pxXlsf1B5CYn81PLxeYsx+MiV9EGbMdUfEcdDWUySyIkdzJefjpR5ejdXVp/KXosGmNUQ+DrIBlzg0AAAAAAAAAAei/mxIAAABAijIIQpL6KlFefiL4FP8UWQktWEz4wFgGNSaXe7mZdVMuiREntehi1b7MRqZ1h+W+Y0y+Z2HtMunsilT2yS5mAA==";
            Assert.AreEqual(expectedXdr, Transaction.ToEnvelopeXdrBase64());
        }

        [TestMethod]
        public void TestSign()
        {
            Transaction.Sign(FeeSource, Network);
            var xdr = Transaction.ToEnvelopeXdr();
            Assert.AreEqual(1, xdr.FeeBump.Signatures.Length);
            var rawSig = xdr.FeeBump.Signatures[0];
            Assert.IsTrue(FeeSource.Verify(Transaction.Hash(Network), rawSig.Signature));
        }

        [TestMethod]
        public void TestSignUsingPreImage()
        {
            var rng = RandomNumberGenerator.Create();
            var preImage = new byte[64];
            rng.GetBytes(preImage);
            Transaction.Sign(preImage);
            var xdr = Transaction.ToEnvelopeXdr();
            var rawSig = xdr.FeeBump.Signatures[0];
            CollectionAssert.AreEqual(preImage, rawSig.Signature.InnerValue);
        }

        [TestMethod]
        public void TestFromEnvelopeXdr()
        {
            var xdr =
                "AAAABQAAAADgSJG2GOUMy/H9lHyjYZOwyuyytH8y0wWaoc596L+bEgAAAAAAAADIAAAAAgAAAABzdv3ojkzWHMD7KUoXhrPx0GH18vHKV0ZfqpMiEblG1gAAAGQAAAAAAAAACAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAA9IYXBweSBiaXJ0aGRheSEAAAAAAQAAAAAAAAABAAAAAOBIkbYY5QzL8f2UfKNhk7DK7LK0fzLTBZqhzn3ov5sSAAAAAAAAAASoF8gAAAAAAAAAAAERuUbWAAAAQK933Dnt1pxXlsf1B5CYn81PLxeYsx+MiV9EGbMdUfEcdDWUySyIkdzJefjpR5ejdXVp/KXosGmNUQ+DrIBlzg0AAAAAAAAAAei/mxIAAABAijIIQpL6KlFefiL4FP8UWQktWEz4wFgGNSaXe7mZdVMuiREntehi1b7MRqZ1h+W+Y0y+Z2HtMunsilT2yS5mAA==";
            var tx = TransactionBuilder.FromEnvelopeXdr(xdr);
            Assert.AreEqual(xdr, tx.ToEnvelopeXdrBase64());
        }

        [TestMethod]
        public void TestMuxedAccounts()
        {
            var originalXdr =
                "AAAABQAAAQAAAAAAAAAAAOBIkbYY5QzL8f2UfKNhk7DK7LK0fzLTBZqhzn3ov5sSAAAAAAAAAMgAAAACAAAAAHN2/eiOTNYcwPspSheGs/HQYfXy8cpXRl+qkyIRuUbWAAAAZAAAAAAAAAAIAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAABAAAAD0hhcHB5IGJpcnRoZGF5IQAAAAABAAAAAAAAAAEAAAAA4EiRthjlDMvx/ZR8o2GTsMrssrR/MtMFmqHOfei/mxIAAAAAAAAABKgXyAAAAAAAAAAAARG5RtYAAABAr3fcOe3WnFeWx/UHkJifzU8vF5izH4yJX0QZsx1R8Rx0NZTJLIiR3Ml5+OlHl6N1dWn8peiwaY1RD4OsgGXODQAAAAAAAAAA";
            var tx = TransactionBuilder.FromEnvelopeXdr(originalXdr);
            var xdr = tx.ToUnsignedEnvelopeXdr();
            var txMuxed = MuxedAccount.FromXdrMuxedAccount(xdr.FeeBump.Tx.FeeSource);
            Assert.IsNotNull(txMuxed.Address);
        }

        [TestMethod]
        public void TestBaseFeeOverflowsLong()
        {
            var network = Network.Test();
            var innerTx = CreateInnerTransaction(100+1, network);
            var feeSource = KeyPair.FromAccountId("GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3");
            Assert.ThrowsException<OverflowException>(() =>
            {
                TransactionBuilder.BuildFeeBumpTransaction(feeSource, innerTx, Int64.MaxValue);
            });
        }

        [TestMethod]
        public void TestTransactionHash()
        {
            var network = Network.Test();
            var innerTx = CreateInnerTransaction(100, network);

            Assert.AreEqual(
                "fa21347ad5eafa4d6ef5355184984b12f2ce8d6762c40f4547f70dcddffb9cb1",
                Util.BytesToHex(innerTx.Hash(network)).ToLowerInvariant());

            var feeSource = KeyPair.FromAccountId("GDQNY3PBOJOKYZSRMK2S7LHHGWZIUISD4QORETLMXEWXBI7KFZZMKTL3");
            var feeBumpTx = TransactionBuilder.BuildFeeBumpTransaction(feeSource, innerTx, 200);

            Assert.AreEqual(
                "76a83f93acad3aaabfbc5d1739192d7f9b6f3707c9cab5a1254e10aaa81a1272",
                Util.BytesToHex(feeBumpTx.Hash(network)).ToLowerInvariant());
        }

        private Transaction CreateInnerTransaction(uint fee, Network network)
        {
            var source = KeyPair.FromSecretSeed("SCH27VUZZ6UAKB67BDNF6FA42YMBMQCBKXWGMFD5TZ6S5ZZCZFLRXKHS");
            var destination =
                KeyPair.FromAccountId("GCRMOGHI75GRWXBBRPFOBMVZ3HZFSEBAEY6JZ7YWV266VCXQOSEUCIWX");
            var account = new Account(source, 2908908335136768L);
            var innerTx = new TransactionBuilder(account)
                .AddOperation(
                    new PaymentOperation.Builder(destination, new AssetTypeNative(), "200.0").Build())
                .SetFee(fee)
                .AddTimeBounds(new TimeBounds(10, 11))
                .Build();
            innerTx.Sign(source, network);
            return innerTx;
        }
    }
}