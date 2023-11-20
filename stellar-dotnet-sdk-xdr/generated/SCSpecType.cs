// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

using System;

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  enum SCSpecType
//  {
//      SC_SPEC_TYPE_VAL = 0,
//  
//      // Types with no parameters.
//      SC_SPEC_TYPE_BOOL = 1,
//      SC_SPEC_TYPE_VOID = 2,
//      SC_SPEC_TYPE_ERROR = 3,
//      SC_SPEC_TYPE_U32 = 4,
//      SC_SPEC_TYPE_I32 = 5,
//      SC_SPEC_TYPE_U64 = 6,
//      SC_SPEC_TYPE_I64 = 7,
//      SC_SPEC_TYPE_TIMEPOINT = 8,
//      SC_SPEC_TYPE_DURATION = 9,
//      SC_SPEC_TYPE_U128 = 10,
//      SC_SPEC_TYPE_I128 = 11,
//      SC_SPEC_TYPE_U256 = 12,
//      SC_SPEC_TYPE_I256 = 13,
//      SC_SPEC_TYPE_BYTES = 14,
//      SC_SPEC_TYPE_STRING = 16,
//      SC_SPEC_TYPE_SYMBOL = 17,
//      SC_SPEC_TYPE_ADDRESS = 19,
//  
//      // Types with parameters.
//      SC_SPEC_TYPE_OPTION = 1000,
//      SC_SPEC_TYPE_RESULT = 1001,
//      SC_SPEC_TYPE_VEC = 1002,
//      SC_SPEC_TYPE_MAP = 1004,
//      SC_SPEC_TYPE_TUPLE = 1005,
//      SC_SPEC_TYPE_BYTES_N = 1006,
//  
//      // User defined types.
//      SC_SPEC_TYPE_UDT = 2000
//  };

//  ===========================================================================
public class SCSpecType
{
    public enum SCSpecTypeEnum
    {
        SC_SPEC_TYPE_VAL = 0,
        SC_SPEC_TYPE_BOOL = 1,
        SC_SPEC_TYPE_VOID = 2,
        SC_SPEC_TYPE_ERROR = 3,
        SC_SPEC_TYPE_U32 = 4,
        SC_SPEC_TYPE_I32 = 5,
        SC_SPEC_TYPE_U64 = 6,
        SC_SPEC_TYPE_I64 = 7,
        SC_SPEC_TYPE_TIMEPOINT = 8,
        SC_SPEC_TYPE_DURATION = 9,
        SC_SPEC_TYPE_U128 = 10,
        SC_SPEC_TYPE_I128 = 11,
        SC_SPEC_TYPE_U256 = 12,
        SC_SPEC_TYPE_I256 = 13,
        SC_SPEC_TYPE_BYTES = 14,
        SC_SPEC_TYPE_STRING = 16,
        SC_SPEC_TYPE_SYMBOL = 17,
        SC_SPEC_TYPE_ADDRESS = 19,
        SC_SPEC_TYPE_OPTION = 1000,
        SC_SPEC_TYPE_RESULT = 1001,
        SC_SPEC_TYPE_VEC = 1002,
        SC_SPEC_TYPE_MAP = 1004,
        SC_SPEC_TYPE_TUPLE = 1005,
        SC_SPEC_TYPE_BYTES_N = 1006,
        SC_SPEC_TYPE_UDT = 2000
    }

    public SCSpecTypeEnum InnerValue { get; set; } = default;

    public static SCSpecType Create(SCSpecTypeEnum v)
    {
        return new SCSpecType
        {
            InnerValue = v
        };
    }

    public static SCSpecType Decode(XdrDataInputStream stream)
    {
        var value = stream.ReadInt();
        switch (value)
        {
            case 0: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_VAL);
            case 1: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_BOOL);
            case 2: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_VOID);
            case 3: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_ERROR);
            case 4: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_U32);
            case 5: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_I32);
            case 6: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_U64);
            case 7: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_I64);
            case 8: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_TIMEPOINT);
            case 9: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_DURATION);
            case 10: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_U128);
            case 11: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_I128);
            case 12: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_U256);
            case 13: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_I256);
            case 14: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_BYTES);
            case 16: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_STRING);
            case 17: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_SYMBOL);
            case 19: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_ADDRESS);
            case 1000: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_OPTION);
            case 1001: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_RESULT);
            case 1002: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_VEC);
            case 1004: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_MAP);
            case 1005: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_TUPLE);
            case 1006: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_BYTES_N);
            case 2000: return Create(SCSpecTypeEnum.SC_SPEC_TYPE_UDT);
            default:
                throw new Exception("Unknown enum value: " + value);
        }
    }

    public static void Encode(XdrDataOutputStream stream, SCSpecType value)
    {
        stream.WriteInt((int)value.InnerValue);
    }
}