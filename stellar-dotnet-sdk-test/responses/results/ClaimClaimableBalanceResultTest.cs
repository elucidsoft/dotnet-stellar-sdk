using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;
using XDR = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class ClaimClaimableBalanceResultTest
    {
        [TestMethod]
        public void TestCannotClaim()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE;

            var result = new XDR.ClaimClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_CANNOT_CLAIM;
            operationResultTr.ClaimClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(ClaimClaimableBalanceCannotClaim), false);
        }

        [TestMethod]
        public void TestDoesNotExist()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE;

            var result = new XDR.ClaimClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_DOES_NOT_EXIST;
            operationResultTr.ClaimClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(ClaimClaimableBalanceDoesNotExist), false);
        }

        [TestMethod]
        public void TestLineFull()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE;

            var result = new XDR.ClaimClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_LINE_FULL;
            operationResultTr.ClaimClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(ClaimClaimableBalanceLineFull), false);
        }

        [TestMethod]
        public void TestNotAuthorized()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE;

            var result = new XDR.ClaimClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_NOT_AUTHORIZED;
            operationResultTr.ClaimClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(ClaimClaimableBalanceNotAuthorized), false);
        }

        [TestMethod]
        public void TestNoTrust()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE;

            var result = new XDR.ClaimClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_NO_TRUST;
            operationResultTr.ClaimClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(ClaimClaimableBalanceNoTrust), false);
        }

        [TestMethod]
        public void TestSuccess()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE;

            var result = new XDR.ClaimClaimableBalanceResult();
            result.Discriminant.InnerValue = XDR.ClaimClaimableBalanceResultCode.ClaimClaimableBalanceResultCodeEnum.CLAIM_CLAIMABLE_BALANCE_SUCCESS;
            operationResultTr.ClaimClaimableBalanceResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(ClaimClaimableBalanceSuccess), true);
        }
    }
}
