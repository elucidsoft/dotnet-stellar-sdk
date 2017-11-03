using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnetcore_sdk;
using sdkxdr = stellar_dotnetcore_sdk.xdr;
using System;
using System.Linq;

namespace stellar_dotnetcore_unittest
{
    [TestClass]
    public class MemoTest
    {
        [TestMethod]
        public void TestMemoNone()
        {
            MemoNone memo = Memo.None();
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_NONE, memo.ToXdr().Discriminant.InnerValue);
        }

        [TestMethod]
        public void TestMemoTextSuccess()
        {
            MemoText memo = Memo.Text("test");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_TEXT, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("test", memo.MemoTextValue);
        }

        [TestMethod]
        public void TestMemoTextUtf8()
        {
            MemoText memo = Memo.Text("三");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_TEXT, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("三", memo.MemoTextValue);
        }

        [TestMethod]
        public void TestMemoTextTooLong()
        {
            try
            {
                Memo.Text("12345678901234567890123456789");
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("text must be <= 28 bytes."));
            }
        }

        [TestMethod]
        public void TestMemoTextTooLongUtf8()
        {
            try
            {
                Memo.Text("价值交易的开源协议!!");
                Assert.Fail();
            }
            catch (Exception exception)
            {
                Assert.IsTrue(exception.Message.Contains("text must be <= 28 bytes."));
            }
        }

        [TestMethod]
        public void TestMemoId()
        {
            MemoId memo = Memo.Id(9223372036854775807L);
            Assert.AreEqual(9223372036854775807L, memo.Id);
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_ID, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual(9223372036854775807L, memo.ToXdr().Id.InnerValue);
        }

        [TestMethod]
        public void TestMemoHashSuccess()
        {
            MemoHash memo = Memo.Hash("4142434445464748494a4b4c");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_HASH, memo.ToXdr().Discriminant.InnerValue);
            String test = "ABCDEFGHIJKL";
            Assert.AreEqual(test, Util.PaddedByteArrayToString(memo.MemoBytes));
            Assert.AreEqual("4142434445464748494a4b4c", memo.GetTrimmedHexValue());
        }

        [TestMethod]
        public void TestMemoHashBytesSuccess()
        {
            byte[] bytes = Enumerable.Repeat((byte)'A', 10).ToArray();
            MemoHash memo = Memo.Hash(bytes);
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_HASH, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("AAAAAAAAAA", Util.PaddedByteArrayToString(memo.MemoBytes));
            Assert.AreEqual("4141414141414141414100000000000000000000000000000000000000000000", memo.GetHexValue());
            Assert.AreEqual("41414141414141414141", memo.GetTrimmedHexValue());
        }

        [TestMethod]
        public void TestMemoHashTooLong()
        {
            byte[] longer = Enumerable.Repeat((byte)0, 33).ToArray();
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
            catch(System.FormatException)
            {

            }
        }

        [TestMethod]
        public void TestMemoReturnHashSuccess()
        {
            MemoReturnHash memo = Memo.returnHash("4142434445464748494a4b4c");
            Assert.AreEqual(sdkxdr.MemoType.MemoTypeEnum.MEMO_RETURN, memo.ToXdr().Discriminant.InnerValue);
            Assert.AreEqual("4142434445464748494a4b4c", memo.GetTrimmedHexValue());
        }
    }
}
