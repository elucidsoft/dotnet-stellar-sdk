using System;
using System.Linq;

namespace stellar_dotnet_sdk.responses.results
{
    public class InflationResult : OperationResult
    {
        public static InflationResult FromXdr(xdr.InflationResult result)
        {
            switch (result.Discriminant.InnerValue)
            {
                case xdr.InflationResultCode.InflationResultCodeEnum.INFLATION_SUCCESS:
                    return new InflationSuccess
                    {
                        Payouts = PayoutsFromXdr(result.Payouts)
                    };
                case xdr.InflationResultCode.InflationResultCodeEnum.INFLATION_NOT_TIME:
                    return new InflationNotTime();
                default:
                    throw new SystemException("Unknown Inflation type");
            }
        }

        private static InflationSuccess.InflationPayout[] PayoutsFromXdr(xdr.InflationPayout[] payouts)
        {
            return payouts.Select(InflationSuccess.InflationPayout.FromXdr).ToArray();
        }
    }
}