using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;
using XDR = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class CreateClaimableBalanceResultTest
    {
        [TestMethod]
        public void TestLowReserve()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE;

            var result = new XDR.CreateClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_LOW_RESERVE;
            operationResultTr.CreateClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(CreateClaimableBalanceResult), false);
        }

        [TestMethod]
        public void TestMalformed()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE;

            var result = new XDR.CreateClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_MALFORMED;
            operationResultTr.CreateClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(CreateClaimableBalanceResult), false);
        }

        [TestMethod]
        public void TestNotAuthorized()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE;

            var result = new XDR.CreateClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_NOT_AUTHORIZED;
            operationResultTr.CreateClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(CreateClaimableBalanceResult), false);
        }

        [TestMethod]
        public void TestNoTrust()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE;

            var result = new XDR.CreateClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_NO_TRUST;
            operationResultTr.CreateClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(CreateClaimableBalanceResult), false);
        }

        [TestMethod]
        public void TestSuccess()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE;

            var result = new XDR.CreateClaimableBalanceResult();
            result.BalanceID = new XDR.ClaimableBalanceID();
            result.BalanceID.V0 = new XDR.Hash(System.Convert.FromBase64String("i7gJhVls6QELGhMtAlC+ScMatzkwXW/s9+UoKVhN13Y="));
            result.Discriminant.InnerValue = XDR.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_SUCCESS;
            operationResultTr.CreateClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(CreateClaimableBalanceResult), true);
        }

        [TestMethod]
        public void TestUnderfunded()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE;

            var result = new XDR.CreateClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.CreateClaimableBalanceResultCode.CreateClaimableBalanceResultCodeEnum.CREATE_CLAIMABLE_BALANCE_UNDERFUNDED;
            operationResultTr.CreateClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(CreateClaimableBalanceResult), false);
        }
    }
}
