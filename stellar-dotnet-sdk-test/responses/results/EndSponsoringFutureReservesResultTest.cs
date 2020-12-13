using Microsoft.VisualStudio.TestTools.UnitTesting;
using stellar_dotnet_sdk.responses.results;
using XDR = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk_test.responses.results
{
    [TestClass]
    public class EndSponsoringFutureReservesResultTest
    {
        [TestMethod]
        public void TestNotSponsored()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.END_SPONSORING_FUTURE_RESERVES;

            var result = new XDR.EndSponsoringFutureReservesResult();
            result.Discriminant.InnerValue = XDR.EndSponsoringFutureReservesResultCode.EndSponsoringFutureReservesResultCodeEnum.END_SPONSORING_FUTURE_RESERVES_NOT_SPONSORED;
            operationResultTr.EndSponsoringFutureReservesResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(EndSponsoringFutureReservesNotSponsored), false);
        }

        [TestMethod]
        public void TestSuccess()
        {
            var operationResultTr = new XDR.OperationResult.OperationResultTr();
            operationResultTr.Discriminant.InnerValue = XDR.OperationType.OperationTypeEnum.END_SPONSORING_FUTURE_RESERVES;

            var result = new XDR.EndSponsoringFutureReservesResult();
            result.Discriminant.InnerValue = XDR.EndSponsoringFutureReservesResultCode.EndSponsoringFutureReservesResultCodeEnum.END_SPONSORING_FUTURE_RESERVES_SUCCESS;
            operationResultTr.EndSponsoringFutureReservesResult = result;

            Util.AssertResultOfType(Util.CreateTransactionResultXDR(operationResultTr), typeof(EndSponsoringFutureReservesSuccess), true);
        }
    }
}
