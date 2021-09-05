// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum EndSponsoringFutureReservesResultCode
    //  {
    //      // codes considered as "success" for the operation
    //      END_SPONSORING_FUTURE_RESERVES_SUCCESS = 0,
    //  
    //      // codes considered as "failure" for the operation
    //      END_SPONSORING_FUTURE_RESERVES_NOT_SPONSORED = -1
    //  };

    //  ===========================================================================
    public class EndSponsoringFutureReservesResultCode
    {
        public enum EndSponsoringFutureReservesResultCodeEnum
        {
            END_SPONSORING_FUTURE_RESERVES_SUCCESS = 0,
            END_SPONSORING_FUTURE_RESERVES_NOT_SPONSORED = -1,
        }
        public EndSponsoringFutureReservesResultCodeEnum InnerValue { get; set; } = default(EndSponsoringFutureReservesResultCodeEnum);

        public static EndSponsoringFutureReservesResultCode Create(EndSponsoringFutureReservesResultCodeEnum v)
        {
            return new EndSponsoringFutureReservesResultCode
            {
                InnerValue = v
            };
        }

        public static EndSponsoringFutureReservesResultCode Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(EndSponsoringFutureReservesResultCodeEnum.END_SPONSORING_FUTURE_RESERVES_SUCCESS);
                case -1: return Create(EndSponsoringFutureReservesResultCodeEnum.END_SPONSORING_FUTURE_RESERVES_NOT_SPONSORED);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, EndSponsoringFutureReservesResultCode value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
