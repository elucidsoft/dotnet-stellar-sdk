// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum SCStatusType
    //  {
    //      SST_OK = 0,
    //      SST_UNKNOWN_ERROR = 1,
    //      SST_HOST_VALUE_ERROR = 2,
    //      SST_HOST_OBJECT_ERROR = 3,
    //      SST_HOST_FUNCTION_ERROR = 4,
    //      SST_HOST_STORAGE_ERROR = 5,
    //      SST_HOST_CONTEXT_ERROR = 6,
    //      SST_VM_ERROR = 7,
    //      SST_CONTRACT_ERROR = 8
    //      // TODO: add more
    //  };

    //  ===========================================================================
    public class SCStatusType
    {
        public enum SCStatusTypeEnum
        {
            SST_OK = 0,
            SST_UNKNOWN_ERROR = 1,
            SST_HOST_VALUE_ERROR = 2,
            SST_HOST_OBJECT_ERROR = 3,
            SST_HOST_FUNCTION_ERROR = 4,
            SST_HOST_STORAGE_ERROR = 5,
            SST_HOST_CONTEXT_ERROR = 6,
            SST_VM_ERROR = 7,
            SST_CONTRACT_ERROR = 8,
        }
        public SCStatusTypeEnum InnerValue { get; set; } = default(SCStatusTypeEnum);

        public static SCStatusType Create(SCStatusTypeEnum v)
        {
            return new SCStatusType
            {
                InnerValue = v
            };
        }

        public static SCStatusType Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(SCStatusTypeEnum.SST_OK);
                case 1: return Create(SCStatusTypeEnum.SST_UNKNOWN_ERROR);
                case 2: return Create(SCStatusTypeEnum.SST_HOST_VALUE_ERROR);
                case 3: return Create(SCStatusTypeEnum.SST_HOST_OBJECT_ERROR);
                case 4: return Create(SCStatusTypeEnum.SST_HOST_FUNCTION_ERROR);
                case 5: return Create(SCStatusTypeEnum.SST_HOST_STORAGE_ERROR);
                case 6: return Create(SCStatusTypeEnum.SST_HOST_CONTEXT_ERROR);
                case 7: return Create(SCStatusTypeEnum.SST_VM_ERROR);
                case 8: return Create(SCStatusTypeEnum.SST_CONTRACT_ERROR);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, SCStatusType value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
