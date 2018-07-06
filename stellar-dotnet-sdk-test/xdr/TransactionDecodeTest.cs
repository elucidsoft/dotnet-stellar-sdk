using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.xdr
{
    [TestClass]
    public class TransactionDecodeTest
    {
        [TestMethod]
        public void TestDecodeTxBody()
        {
            // pubnet - ledgerseq 5845058, txid  d5ec6645d86cdcae8212cbe60feaefb8d6b1a8b7d11aeea590608b0863ace4de
            var txBody = "AAAAAERmsKL73CyLV/HvjyQCERDXXpWE70Xhyb6MR5qPO3yQAAAAZAAIbkEAACD7AAAAAAAAAAN43bSwpXw8tSAhl7TBtQeOZTQAXwAAAAAAAAAAAAAAAAAAAAEAAAABAAAAAP1qe44j+i4uIT+arbD4QDQBt8ryEeJd7a0jskQ3nwDeAAAAAAAAAADdVhDVFrUiS/jPrRpblXY4bAW9u4hbRI2Hhw+2ATsFpQAAAAAtPWvAAAAAAAAAAAGPO3yQAAAAQHGWVHCBsjTyap/OY9JjPHmzWtN2Y2sL98aMERc/xJ3hcWz6kdQAwjlEhilItCyokDHCrvALZy3v/1TlaDqprA0=";

            var bytes = Convert.FromBase64String(txBody);
            GetDebugBytes(bytes);

            var unused = new MemoryStream(bytes);
            var transactionEnvelope = TransactionEnvelope.Decode(new XdrDataInputStream(bytes));

            var uint64 = transactionEnvelope.Tx.SeqNum.InnerValue.InnerValue;
            Assert.AreEqual(2373025265623291L, uint64);
        }
        
        [TestMethod]
        public void TestDecodeTxNoTail()
        {
            var noTailTransactionXdr =
                    "AAAAAEntn0+aXWXUuLtq9RDTOQHw6FKCz4YULdFpx6KrFLM2AAABLACOUUAAAAAEAAAAAQAAAABbFYjQAAAAAH////8AAAABAAAAFEVDTFlQU0VTQ09JTiBQYXltZW50AAAAAwAAAAAAAAABAAAAANqNgkK/Piz99iwrbp+Lr2LjwDKul8tgETQO2igoZaaNAAAAAkVDTFlQU0VTQ09JTgAAAABwYz2lcMDGOi6MU/sYdRuBye2KWD5aolXM/Yxdr8X9+AAAAAAF9eEAAAAAAAAAAAYAAAACRUNMWVBTRVNDT0lOAAAAAHBjPaVwwMY6LoxT+xh1G4HJ7YpYPlqiVcz9jF2vxf34AAAAAAAAAAAAAAABAAAAAEntn0+aXWXUuLtq9RDTOQHw6FKCz4YULdFpx6KrFLM2AAAACAAAAACxJkMYYoztLLzXom8YBWAqFrREfEt3DvmQepBBLNCzbQAAAAAAAAABLNCzbQAAAEDjlco8Cz8GnuFItJVUUEMPYizwC5LLTxueNI9KEf1fgyPZ0hYC5IQP5Augl/p78z5WpnHfFZXYz3vXMf6buhgN";

                var bytes = noTailTransactionXdr.ToCharArray();
                var xdrDataInputStream = new XdrDataInputStream(Convert.FromBase64CharArray(bytes, 0, bytes.Length));

                var decodedTransaction = Transaction.Decode(xdrDataInputStream);
                var memo = decodedTransaction.Memo.Text;
                Assert.AreEqual("ECLYPSESCOIN Payment", memo);
            
        }

        [TestMethod]
        public void TestDecodeTxResult()
        {
            var txResult = "1exmRdhs3K6CEsvmD+rvuNaxqLfRGu6lkGCLCGOs5N4AAAAAAAAAZAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAA==";
            var bytes = Convert.FromBase64String(txResult);

            var unused = new MemoryStream(bytes);
            var transactionResult = TransactionResultPair.Decode(new XdrDataInputStream(bytes));

            var discriminant = transactionResult.Result.Result.Discriminant;
            Assert.AreEqual(TransactionResultCode.TransactionResultCodeEnum.txSUCCESS, discriminant.InnerValue);
        }

        [TestMethod]
        public void TestDecodeTxMeta()
        {
            var txMeta = "AAAAAAAAAAEAAAADAAAAAABZMEIAAAAAAAAAAN1WENUWtSJL+M+tGluVdjhsBb27iFtEjYeHD7YBOwWlAAAAAC09a8AAWTBCAAAAAAAAAAAAAAAAAAAAAAAAAAABAAAAAAAAAAAAAAAAAAAAAAAAAwBZL8QAAAAAAAAAAP1qe44j+i4uIT+arbD4QDQBt8ryEeJd7a0jskQ3nwDeAALU1gZ4V7UACD1BAAAAHgAAAAoAAAAAAAAAAAAAAAABAAAAAAAACgAAAAARC07BokpLTOF+/vVKBwiAlop7hHGJTNeGGlY4MoPykwAAAAEAAAAAK+Lzfd3yDD+Ov0GbYu1g7SaIBrKZeBUxoCunkLuI7aoAAAABAAAAAERmsKL73CyLV/HvjyQCERDXXpWE70Xhyb6MR5qPO3yQAAAAAQAAAABSORGwAdyuanN3sNOHqNSpACyYdkUM3L8VafUu69EvEgAAAAEAAAAAeCzqJNkMM/jLvyuMIfyFHljBlLCtDyj17RMycPuNtRMAAAABAAAAAIEi4R7juq15ymL00DNlAddunyFT4FyUD4muC4t3bobdAAAAAQAAAACaNpLL5YMfjOTdXVEqrAh99LM12sN6He6pHgCRAa1f1QAAAAEAAAAAqB+lfAPV9ak+Zkv4aTNZwGaFFAfui4+yhM3dGhoYJ+sAAAABAAAAAMNJrEvdMg6M+M+n4BDIdzsVSj/ZI9SvAp7mOOsvAD/WAAAAAQAAAADbHA6xiKB1+G79mVqpsHMOleOqKa5mxDpP5KEp/Xdz9wAAAAEAAAAAAAAAAAAAAAEAWTBCAAAAAAAAAAD9anuOI/ouLiE/mq2w+EA0AbfK8hHiXe2tI7JEN58A3gAC1NXZOuv1AAg9QQAAAB4AAAAKAAAAAAAAAAAAAAAAAQAAAAAAAAoAAAAAEQtOwaJKS0zhfv71SgcIgJaKe4RxiUzXhhpWODKD8pMAAAABAAAAACvi833d8gw/jr9Bm2LtYO0miAaymXgVMaArp5C7iO2qAAAAAQAAAABEZrCi+9wsi1fx748kAhEQ116VhO9F4cm+jEeajzt8kAAAAAEAAAAAUjkRsAHcrmpzd7DTh6jUqQAsmHZFDNy/FWn1LuvRLxIAAAABAAAAAHgs6iTZDDP4y78rjCH8hR5YwZSwrQ8o9e0TMnD7jbUTAAAAAQAAAACBIuEe47qtecpi9NAzZQHXbp8hU+BclA+JrguLd26G3QAAAAEAAAAAmjaSy+WDH4zk3V1RKqwIffSzNdrDeh3uqR4AkQGtX9UAAAABAAAAAKgfpXwD1fWpPmZL+GkzWcBmhRQH7ouPsoTN3RoaGCfrAAAAAQAAAADDSaxL3TIOjPjPp+AQyHc7FUo/2SPUrwKe5jjrLwA/1gAAAAEAAAAA2xwOsYigdfhu/ZlaqbBzDpXjqimuZsQ6T+ShKf13c/cAAAABAAAAAAAAAAA=";
            var bytes = Convert.FromBase64String(txMeta);

            var transactionMeta = TransactionMeta.Decode(new XdrDataInputStream(bytes));
            Assert.AreEqual(1, transactionMeta.Operations.Length);
        }

        [TestMethod]
        public void TestTransactionEnvelopeWithMemo()
        {
            var transactionEnvelopeToDecode = "AAAAACq1Ixcw1fchtF5aLTSw1zaYAYjb3WbBRd4jqYJKThB9AAAAZAA8tDoAAAALAAAAAAAAAAEAAAAZR29sZCBwYXltZW50IGZvciBzZXJ2aWNlcwAAAAAAAAEAAAAAAAAAAQAAAAARREGslec48mbJJygIwZoLvRtL6/gGL4ss2TOpnOUOhgAAAAFHT0xEAAAAACq1Ixcw1fchtF5aLTSw1zaYAYjb3WbBRd4jqYJKThB9AAAAADuaygAAAAAAAAAAAA==";
            var bytes = Convert.FromBase64String(transactionEnvelopeToDecode);

            var transactionEnvelope = TransactionEnvelope.Decode(new XdrDataInputStream(bytes));

            Assert.AreEqual(1, transactionEnvelope.Tx.Operations.Length);

            var expected = Encoding.UTF8.GetBytes(new[] {'G', 'O', 'L', 'D'});
            var actual = transactionEnvelope.Tx.Operations[0].Body.PaymentOp.Asset.AlphaNum4.AssetCode;

            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        private void GetDebugBytes(byte[] bytes)
        {
            var unused = bytes.Select(a => (sbyte) a).ToArray();
        }
    }
}
