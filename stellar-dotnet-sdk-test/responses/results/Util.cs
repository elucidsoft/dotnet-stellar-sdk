using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses;
using System;
using XDR = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.responses.results
{
    public static class Util
    {
        public static TransactionResult AssertResultOfType(string xdr, Type resultType, bool isSuccess)
        {
            var result = TransactionResult.FromXdr(xdr);
            Assert.IsInstanceOfType(result, typeof(TransactionResultFailed));
            var failed = (TransactionResultFailed)result;
            Assert.IsFalse(failed.IsSuccess);
            Assert.AreEqual(1, failed.Results.Count);
            Assert.IsInstanceOfType(failed.Results[0], resultType);
            Assert.AreEqual(isSuccess, failed.Results[0].IsSuccess);
            return result;
        }

        public static string CreateTransactionResultXDR(XDR.OperationResult.OperationResultTr operationResultTr)
        {
            var transactionResult = new XDR.TransactionResult();
            transactionResult.Result = new XDR.TransactionResult.TransactionResultResult();
            transactionResult.Result.Discriminant.InnerValue = XDR.TransactionResultCode.TransactionResultCodeEnum.txFAILED;
            transactionResult.Result.Results = new XDR.OperationResult[1];
            transactionResult.Ext = new XDR.TransactionResult.TransactionResultExt();
            transactionResult.FeeCharged = new XDR.Int64(100L);

            var operationResult = new XDR.OperationResult();
            operationResult.Tr = operationResultTr;

            transactionResult.Result.Results[0] = operationResult;

            var outputStream = new XDR.XdrDataOutputStream();
            XDR.TransactionResult.Encode(outputStream, transactionResult);
            return Convert.ToBase64String(outputStream.ToArray());
        }
    }
}
