// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum SCVmErrorCode {
    //      VM_UNKNOWN = 0,
    //      VM_VALIDATION = 1,
    //      VM_INSTANTIATION = 2,
    //      VM_FUNCTION = 3,
    //      VM_TABLE = 4,
    //      VM_MEMORY = 5,
    //      VM_GLOBAL = 6,
    //      VM_VALUE = 7,
    //      VM_TRAP_UNREACHABLE = 8,
    //      VM_TRAP_MEMORY_ACCESS_OUT_OF_BOUNDS = 9,
    //      VM_TRAP_TABLE_ACCESS_OUT_OF_BOUNDS = 10,
    //      VM_TRAP_ELEM_UNINITIALIZED = 11,
    //      VM_TRAP_DIVISION_BY_ZERO = 12,
    //      VM_TRAP_INTEGER_OVERFLOW = 13,
    //      VM_TRAP_INVALID_CONVERSION_TO_INT = 14,
    //      VM_TRAP_STACK_OVERFLOW = 15,
    //      VM_TRAP_UNEXPECTED_SIGNATURE = 16,
    //      VM_TRAP_MEM_LIMIT_EXCEEDED = 17,
    //      VM_TRAP_CPU_LIMIT_EXCEEDED = 18
    //  };

    //  ===========================================================================
    public class SCVmErrorCode
    {
        public enum SCVmErrorCodeEnum
        {
            VM_UNKNOWN = 0,
            VM_VALIDATION = 1,
            VM_INSTANTIATION = 2,
            VM_FUNCTION = 3,
            VM_TABLE = 4,
            VM_MEMORY = 5,
            VM_GLOBAL = 6,
            VM_VALUE = 7,
            VM_TRAP_UNREACHABLE = 8,
            VM_TRAP_MEMORY_ACCESS_OUT_OF_BOUNDS = 9,
            VM_TRAP_TABLE_ACCESS_OUT_OF_BOUNDS = 10,
            VM_TRAP_ELEM_UNINITIALIZED = 11,
            VM_TRAP_DIVISION_BY_ZERO = 12,
            VM_TRAP_INTEGER_OVERFLOW = 13,
            VM_TRAP_INVALID_CONVERSION_TO_INT = 14,
            VM_TRAP_STACK_OVERFLOW = 15,
            VM_TRAP_UNEXPECTED_SIGNATURE = 16,
            VM_TRAP_MEM_LIMIT_EXCEEDED = 17,
            VM_TRAP_CPU_LIMIT_EXCEEDED = 18,
        }
        public SCVmErrorCodeEnum InnerValue { get; set; } = default(SCVmErrorCodeEnum);

        public static SCVmErrorCode Create(SCVmErrorCodeEnum v)
        {
            return new SCVmErrorCode
            {
                InnerValue = v
            };
        }

        public static SCVmErrorCode Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(SCVmErrorCodeEnum.VM_UNKNOWN);
                case 1: return Create(SCVmErrorCodeEnum.VM_VALIDATION);
                case 2: return Create(SCVmErrorCodeEnum.VM_INSTANTIATION);
                case 3: return Create(SCVmErrorCodeEnum.VM_FUNCTION);
                case 4: return Create(SCVmErrorCodeEnum.VM_TABLE);
                case 5: return Create(SCVmErrorCodeEnum.VM_MEMORY);
                case 6: return Create(SCVmErrorCodeEnum.VM_GLOBAL);
                case 7: return Create(SCVmErrorCodeEnum.VM_VALUE);
                case 8: return Create(SCVmErrorCodeEnum.VM_TRAP_UNREACHABLE);
                case 9: return Create(SCVmErrorCodeEnum.VM_TRAP_MEMORY_ACCESS_OUT_OF_BOUNDS);
                case 10: return Create(SCVmErrorCodeEnum.VM_TRAP_TABLE_ACCESS_OUT_OF_BOUNDS);
                case 11: return Create(SCVmErrorCodeEnum.VM_TRAP_ELEM_UNINITIALIZED);
                case 12: return Create(SCVmErrorCodeEnum.VM_TRAP_DIVISION_BY_ZERO);
                case 13: return Create(SCVmErrorCodeEnum.VM_TRAP_INTEGER_OVERFLOW);
                case 14: return Create(SCVmErrorCodeEnum.VM_TRAP_INVALID_CONVERSION_TO_INT);
                case 15: return Create(SCVmErrorCodeEnum.VM_TRAP_STACK_OVERFLOW);
                case 16: return Create(SCVmErrorCodeEnum.VM_TRAP_UNEXPECTED_SIGNATURE);
                case 17: return Create(SCVmErrorCodeEnum.VM_TRAP_MEM_LIMIT_EXCEEDED);
                case 18: return Create(SCVmErrorCodeEnum.VM_TRAP_CPU_LIMIT_EXCEEDED);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, SCVmErrorCode value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
