using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class TransactionResultTest
    {
        [TestMethod]
        public void TestTooEarly()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////+AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultTooEarly));
        }

        [TestMethod]
        public void TestTooLate()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////9AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultTooLate));
        }

        [TestMethod]
        public void TestMissingOperation()
        {
            var result = TransactionResult.FromXdr(" AAAAAAAPQkD////8AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultMissingOperation));
        }

        [TestMethod]
        public void TestBadSeq()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////7AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultBadSeq));
        }

        [TestMethod]
        public void TestBadAuth()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////6AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultBadAuth));
        }

        [TestMethod]
        public void TestInsufficientBalance()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////5AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultInsufficientBalance));
        }

        [TestMethod]
        public void TestNoAccount()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////4AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultNoAccount));
        }

        [TestMethod]
        public void TestInsufficientFee()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////3AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultInsufficientFee));
        }

        [TestMethod]
        public void TestBadAuthExtra()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////2AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultBadAuthExtra));
        }

        [TestMethod]
        public void TestInternalError()
        {
            var result = TransactionResult.FromXdr("AAAAAAAPQkD////1AAAAAA==");
            Assert.AreEqual("0.1", result.FeeCharged);
            Assert.IsInstanceOfType(result, typeof(TransactionResultInternalError));
        }
    }
}