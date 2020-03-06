﻿using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk;
using FormatException = System.FormatException;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test
{
    [TestClass]
    public class MemoTest
    {
        [TestMethod]
        public void TestMemoNone()
        {
            var memo = Memo.None();
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_NONE, memo.ToXdr().Discriminant.InnerValue);
        }

        [TestMethod]
        public void TestMemoTextSuccess()
        {
            var memo = Memo.Text("test");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_TEXT, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("test", memo.MemoTextValue);
        }

        [TestMethod]
        public void TestMemoTextUtf8()
        {
            var memo = Memo.Text("三");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_TEXT, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("三", memo.MemoTextValue);
        }

        //[TestMethod]
        //public void TestMemoTextTooLong()
        //{
        //    try
        //    {
        //        Memo.Text("12345678901234567890123456789");
        //        Assert.Fail();
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.IsTrue(exception.Message.Contains("text must be <= 28 bytes."));
        //    }
        //}

        //[TestMethod]
        //public void TestMemoTextTooLongUtf8()
        //{
        //    try
        //    {
        //        Memo.Text("价值交易的开源协议!!");
        //        Assert.Fail();
        //    }
        //    catch (Exception exception)
        //    {
        //        Assert.IsTrue(exception.Message.Contains("text must be <= 28 bytes."));
        //    }
        //}

        [TestMethod]
        public void TestMemoId()
        {
            var memo = Memo.Id(9223372036854775807L);
            Assert.AreEqual(9223372036854775807UL, memo.IdValue);
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_ID, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual(9223372036854775807UL, memo.ToXdr().Id.InnerValue);
        }

        [TestMethod]
        public void TestMemoHashSuccess()
        {
            var memo = Memo.Hash("4142434445464748494a4b4c");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_HASH, memo.ToXdr().Discriminant.InnerValue);
            var test = "ABCDEFGHIJKL";
            Assert.AreEqual(test, Util.PaddedByteArrayToString(memo.MemoBytes));
            Assert.AreEqual("4142434445464748494a4b4c", memo.GetTrimmedHexValue());
        }

        [TestMethod]
        public void TestMemoHashBytesSuccess()
        {
            var bytes = Enumerable.Repeat((byte) 'A', 10).ToArray();
            var memo = Memo.Hash(bytes);
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_HASH, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("AAAAAAAAAA", Util.PaddedByteArrayToString(memo.MemoBytes));
            Assert.AreEqual("4141414141414141414100000000000000000000000000000000000000000000", memo.GetHexValue());
            Assert.AreEqual("41414141414141414141", memo.GetTrimmedHexValue());
        }

        [TestMethod]
        public void TestMemoHashTooLong()
        {
            var longer = Enumerable.Repeat((byte) 0, 33).ToArray();
            try
            {
                Memo.Hash(longer);
                Assert.Fail();
            }
            catch (MemoTooLongException exception)
            {
                Assert.IsTrue(exception.Message.Contains("MEMO_HASH can contain 32 bytes at max."));
            }
        }

        [TestMethod]
        public void TestMemoHashInvalidHex()
        {
            try
            {
                Memo.Hash("test");
                Assert.Fail();
            }
            catch (FormatException)
            {
            }
        }

        [TestMethod]
        public void TestMemoReturnHashSuccess()
        {
            var memo = Memo.ReturnHash("4142434445464748494a4b4c");
            var memoXdr = memo.ToXdr();
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_RETURN, memoXdr.Discriminant.InnerValue);
            Assert.IsNull(memoXdr.Hash);
            Assert.AreEqual("4142434445464748494a4b4c0000000000000000000000000000000000000000",
                BitConverter.ToString(memoXdr.RetHash.InnerValue).Replace("-", "").ToLower());
            Assert.AreEqual("4142434445464748494a4b4c", memo.GetTrimmedHexValue());
        }

        [TestMethod]
        public void TestMemoIDEquality()
        {
            var memo = Memo.Id(9223372036854775807L);
            var memo2 = Memo.Id(9223372036854775807L);

            Assert.AreEqual(memo.GetHashCode(), memo2.GetHashCode());
            Assert.AreEqual(memo, memo2);
        }

        [TestMethod]
        public void TestMemoReturnHashEquality()
        {
            var memo = Memo.ReturnHash("4142434445464748494a4b4c");
            var memo2 = Memo.ReturnHash("4142434445464748494a4b4c");

            Assert.AreEqual(memo.GetHashCode(), memo2.GetHashCode());
            Assert.AreEqual(memo, memo2);

            memo = Memo.ReturnHash(Encoding.UTF8.GetBytes("4142434445464748494a4b4c"));
            memo2 = Memo.ReturnHash(Encoding.UTF8.GetBytes("4142434445464748494a4b4c"));

            Assert.AreEqual(memo.GetHashCode(), memo2.GetHashCode());
            Assert.AreEqual(memo, memo2);
        }

        [TestMethod]
        public void TestMemoHashEquality()
        {
            var memo = Memo.Hash("4142434445464748494a4b4c");
            var memo2 = Memo.Hash("4142434445464748494a4b4c");

            Assert.AreEqual(memo.GetHashCode(), memo2.GetHashCode());
            Assert.AreEqual(memo, memo2);
        }

        [TestMethod]
        public void TestMemoTextEquality()
        {
            var memo = Memo.Text("test");
            var memo2 = Memo.Text("test");

            Assert.AreEqual(memo.GetHashCode(), memo2.GetHashCode());
            Assert.AreEqual(memo, memo2);
        }

        [TestMethod]
        public void TestMemoNoneEquality()
        {
            var memo = Memo.None();
            var memo2 = Memo.None();

            Assert.AreEqual(memo.GetHashCode(), memo2.GetHashCode());
            Assert.AreEqual(memo, memo2);
        }

        [TestMethod]
        public void TestMemoTextAsciiEncodedButTooLongIfUtf8()
        {
            // This test comes from transaction 3e88850619eafebb844acd8c672fce55159cbdabbcda4e9e4d6de1e0c3636116
            // on the test network. If the memo is decoded as UTF8, its length is above the limit of 28, but if
            // decoded as ASCII (as per spec) it's fine.
            var txXdr =
                "AAAAAGqjwSAMOZoob9PCTn1rNxY0qjJyBovxDR912gObtac6AAAAZAAGuwkAAABSAAAAAQAAAAAAAAAAAAAAAF5h4XsAAAABAAAAFDd8q5x7M/nN9XhWt2RxdmpxZmltAAAAAQAAAAAAAAABAAAAAOHq5mmDjeY0Rhr+xQSKjKYMWFD9MgjiE5JxgQrBxthIAAAAAU1PT04AAAAA4ermaYON5jRGGv7FBIqMpgxYUP0yCOITknGBCsHG2EgAAAAAAJiWgAAAAAAAAAABm7WnOgAAAEDO4OJ76sJic/t9tm4Nk1uLxvfD5ZUIZedqBQj2k87nCmMVtJGc96moJdDfe2t/C9Dx8ZsIgD/NHHUXs9hXvXsN";
            var tx = Transaction.FromEnvelopeXdr(txXdr);
            if (tx.Memo is MemoText memo)
            {
                Assert.AreEqual(20, memo.MemoTextValue.Length);
            }
            else
            {
                throw new Exception("Memo should be MemoText");
            }
        }
    }
}