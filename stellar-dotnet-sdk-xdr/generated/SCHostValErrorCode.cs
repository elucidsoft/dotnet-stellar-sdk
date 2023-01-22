// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum SCHostValErrorCode
    //  {
    //      HOST_VALUE_UNKNOWN_ERROR = 0,
    //      HOST_VALUE_RESERVED_TAG_VALUE = 1,
    //      HOST_VALUE_UNEXPECTED_VAL_TYPE = 2,
    //      HOST_VALUE_U63_OUT_OF_RANGE = 3,
    //      HOST_VALUE_U32_OUT_OF_RANGE = 4,
    //      HOST_VALUE_STATIC_UNKNOWN = 5,
    //      HOST_VALUE_MISSING_OBJECT = 6,
    //      HOST_VALUE_SYMBOL_TOO_LONG = 7,
    //      HOST_VALUE_SYMBOL_BAD_CHAR = 8,
    //      HOST_VALUE_SYMBOL_CONTAINS_NON_UTF8 = 9,
    //      HOST_VALUE_BITSET_TOO_MANY_BITS = 10,
    //      HOST_VALUE_STATUS_UNKNOWN = 11
    //  };

    //  ===========================================================================
    public class SCHostValErrorCode
    {
        public enum SCHostValErrorCodeEnum
        {
            HOST_VALUE_UNKNOWN_ERROR = 0,
            HOST_VALUE_RESERVED_TAG_VALUE = 1,
            HOST_VALUE_UNEXPECTED_VAL_TYPE = 2,
            HOST_VALUE_U63_OUT_OF_RANGE = 3,
            HOST_VALUE_U32_OUT_OF_RANGE = 4,
            HOST_VALUE_STATIC_UNKNOWN = 5,
            HOST_VALUE_MISSING_OBJECT = 6,
            HOST_VALUE_SYMBOL_TOO_LONG = 7,
            HOST_VALUE_SYMBOL_BAD_CHAR = 8,
            HOST_VALUE_SYMBOL_CONTAINS_NON_UTF8 = 9,
            HOST_VALUE_BITSET_TOO_MANY_BITS = 10,
            HOST_VALUE_STATUS_UNKNOWN = 11,
        }
        public SCHostValErrorCodeEnum InnerValue { get; set; } = default(SCHostValErrorCodeEnum);

        public static SCHostValErrorCode Create(SCHostValErrorCodeEnum v)
        {
            return new SCHostValErrorCode
            {
                InnerValue = v
            };
        }

        public static SCHostValErrorCode Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(SCHostValErrorCodeEnum.HOST_VALUE_UNKNOWN_ERROR);
                case 1: return Create(SCHostValErrorCodeEnum.HOST_VALUE_RESERVED_TAG_VALUE);
                case 2: return Create(SCHostValErrorCodeEnum.HOST_VALUE_UNEXPECTED_VAL_TYPE);
                case 3: return Create(SCHostValErrorCodeEnum.HOST_VALUE_U63_OUT_OF_RANGE);
                case 4: return Create(SCHostValErrorCodeEnum.HOST_VALUE_U32_OUT_OF_RANGE);
                case 5: return Create(SCHostValErrorCodeEnum.HOST_VALUE_STATIC_UNKNOWN);
                case 6: return Create(SCHostValErrorCodeEnum.HOST_VALUE_MISSING_OBJECT);
                case 7: return Create(SCHostValErrorCodeEnum.HOST_VALUE_SYMBOL_TOO_LONG);
                case 8: return Create(SCHostValErrorCodeEnum.HOST_VALUE_SYMBOL_BAD_CHAR);
                case 9: return Create(SCHostValErrorCodeEnum.HOST_VALUE_SYMBOL_CONTAINS_NON_UTF8);
                case 10: return Create(SCHostValErrorCodeEnum.HOST_VALUE_BITSET_TOO_MANY_BITS);
                case 11: return Create(SCHostValErrorCodeEnum.HOST_VALUE_STATUS_UNKNOWN);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, SCHostValErrorCode value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
