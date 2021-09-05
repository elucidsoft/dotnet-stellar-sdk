// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  union SetTrustLineFlagsResult switch (SetTrustLineFlagsResultCode code)
    //  {
    //  case SET_TRUST_LINE_FLAGS_SUCCESS:
    //      void;
    //  default:
    //      void;
    //  };

    //  ===========================================================================
    public class SetTrustLineFlagsResult
    {
        public SetTrustLineFlagsResult() { }

        public SetTrustLineFlagsResultCode Discriminant { get; set; } = new SetTrustLineFlagsResultCode();

        public static void Encode(XdrDataOutputStream stream, SetTrustLineFlagsResult encodedSetTrustLineFlagsResult)
        {
            stream.WriteInt((int)encodedSetTrustLineFlagsResult.Discriminant.InnerValue);
            switch (encodedSetTrustLineFlagsResult.Discriminant.InnerValue)
            {
                case SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_SUCCESS:
                    break;
                default:
                    break;
            }
        }
        public static SetTrustLineFlagsResult Decode(XdrDataInputStream stream)
        {
            SetTrustLineFlagsResult decodedSetTrustLineFlagsResult = new SetTrustLineFlagsResult();
            SetTrustLineFlagsResultCode discriminant = SetTrustLineFlagsResultCode.Decode(stream);
            decodedSetTrustLineFlagsResult.Discriminant = discriminant;
            switch (decodedSetTrustLineFlagsResult.Discriminant.InnerValue)
            {
                case SetTrustLineFlagsResultCode.SetTrustLineFlagsResultCodeEnum.SET_TRUST_LINE_FLAGS_SUCCESS:
                    break;
                default:
                    break;
            }
            return decodedSetTrustLineFlagsResult;
        }
    }
}
