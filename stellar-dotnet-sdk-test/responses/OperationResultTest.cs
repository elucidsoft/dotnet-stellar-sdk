using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk.responses.results;

namespace stellar_dotnet_sdk_test.responses
{
    [TestClass]
    public class OperationResultTest
    {
        [TestMethod]
        public void TestBadAuth()
        {
            var result = TransactionResult.FromXdr("AAAAAACYloD/////AAAAAf////8AAAAA");
            Assert.IsInstanceOfType(result, typeof(TransactionResultFailed));
            var failed = (TransactionResultFailed)result;
            Assert.IsFalse(failed.IsSuccess);
            Assert.AreEqual(1, failed.Results.Count);
            var op = failed.Results[0];
            Assert.IsInstanceOfType(op, typeof(OperationResultBadAuth));
        }

        [TestMethod]
        public void TestNoAccount()
        {
            var result = TransactionResult.FromXdr("AAAAAACYloD/////AAAAAf////4AAAAA");
            Assert.IsInstanceOfType(result, typeof(TransactionResultFailed));
            var failed = (TransactionResultFailed)result;
            Assert.IsFalse(failed.IsSuccess);
            Assert.AreEqual(1, failed.Results.Count);
            var op = failed.Results[0];
            Assert.IsInstanceOfType(op, typeof(OperationResultNoAccount));
        }

        [TestMethod]
        public void TestNotSupported()
        {
            var result = TransactionResult.FromXdr("AAAAAACYloD/////AAAAAf////0AAAAA");
            Assert.IsInstanceOfType(result, typeof(TransactionResultFailed));
            var failed = (TransactionResultFailed)result;
            Assert.IsFalse(failed.IsSuccess);
            Assert.AreEqual(1, failed.Results.Count);
            var op = failed.Results[0];
            Assert.IsInstanceOfType(op, typeof(OperationResultNotSupported));
        }

        [TestMethod]
        public void TestMultipleFailures()
        {
            var result = TransactionResult.FromXdr("AAAAAACYloD/////AAAAA/////3//////////gAAAAA=");
            Assert.IsInstanceOfType(result, typeof(TransactionResultFailed));
            var failed = (TransactionResultFailed)result;
            Assert.IsFalse(failed.IsSuccess);
            Assert.AreEqual(3, failed.Results.Count);
            Assert.IsInstanceOfType(failed.Results[0], typeof(OperationResultNotSupported));
            Assert.IsInstanceOfType(failed.Results[1], typeof(OperationResultBadAuth));
            Assert.IsInstanceOfType(failed.Results[2], typeof(OperationResultNoAccount));
        }
    }
}