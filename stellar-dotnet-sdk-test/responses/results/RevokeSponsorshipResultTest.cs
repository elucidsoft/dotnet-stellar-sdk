using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;
using XDR = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class RevokeSponsorshipResultTest
    {
        [TestMethod]
        public void TestDoesNotExist()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP;

            var result = new XDR.RevokeSponsorshipResult();
            result.Discriminant.InnerValue = XDR.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_DOES_NOT_EXIST;
            operationResultTr.RevokeSponsorshipResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(RevokeSponsorshipDoesNotExist), false);
        }

        [TestMethod]
        public void TestLowReserve()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP;

            var result = new XDR.RevokeSponsorshipResult();
            result.Discriminant.InnerValue = XDR.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_LOW_RESERVE;
            operationResultTr.RevokeSponsorshipResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(RevokeSponsorshipLowReserve), false);
        }

        [TestMethod]
        public void TestNotSponsor()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP;

            var result = new XDR.RevokeSponsorshipResult();
            result.Discriminant.InnerValue = XDR.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_NOT_SPONSOR;
            operationResultTr.RevokeSponsorshipResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(RevokeSponsorshipNotSponsor), false);
        }

        [TestMethod]
        public void TestOnlyTransferable()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP;

            var result = new XDR.RevokeSponsorshipResult();
            result.Discriminant.InnerValue = XDR.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_ONLY_TRANSFERABLE;
            operationResultTr.RevokeSponsorshipResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(RevokeSponsorshipOnlyTransferable), false);
        }

        [TestMethod]
        public void TestSuccesss()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.REVOKE_SPONSORSHIP;

            var result = new XDR.RevokeSponsorshipResult();
            result.Discriminant.InnerValue = XDR.RevokeSponsorshipResultCode.RevokeSponsorshipResultCodeEnum.REVOKE_SPONSORSHIP_SUCCESS;
            operationResultTr.RevokeSponsorshipResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(RevokeSponsorshipSuccess), true);
        }
    }
}
