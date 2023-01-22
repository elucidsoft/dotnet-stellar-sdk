// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum SCObjectType
    //  {
    //      // We have a few objects that represent non-stellar-specific concepts
    //      // like general-purpose maps, vectors, numbers, blobs.
    //  
    //      SCO_VEC = 0,
    //      SCO_MAP = 1,
    //      SCO_U64 = 2,
    //      SCO_I64 = 3,
    //      SCO_U128 = 4,
    //      SCO_I128 = 5,
    //      SCO_BYTES = 6,
    //      SCO_CONTRACT_CODE = 7,
    //      SCO_ACCOUNT_ID = 8
    //  
    //      // TODO: add more
    //  };

    //  ===========================================================================
    public class SCObjectType
    {
        public enum SCObjectTypeEnum
        {
            SCO_VEC = 0,
            SCO_MAP = 1,
            SCO_U64 = 2,
            SCO_I64 = 3,
            SCO_U128 = 4,
            SCO_I128 = 5,
            SCO_BYTES = 6,
            SCO_CONTRACT_CODE = 7,
            SCO_ACCOUNT_ID = 8,
        }
        public SCObjectTypeEnum InnerValue { get; set; } = default(SCObjectTypeEnum);

        public static SCObjectType Create(SCObjectTypeEnum v)
        {
            return new SCObjectType
            {
                InnerValue = v
            };
        }

        public static SCObjectType Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(SCObjectTypeEnum.SCO_VEC);
                case 1: return Create(SCObjectTypeEnum.SCO_MAP);
                case 2: return Create(SCObjectTypeEnum.SCO_U64);
                case 3: return Create(SCObjectTypeEnum.SCO_I64);
                case 4: return Create(SCObjectTypeEnum.SCO_U128);
                case 5: return Create(SCObjectTypeEnum.SCO_I128);
                case 6: return Create(SCObjectTypeEnum.SCO_BYTES);
                case 7: return Create(SCObjectTypeEnum.SCO_CONTRACT_CODE);
                case 8: return Create(SCObjectTypeEnum.SCO_ACCOUNT_ID);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, SCObjectType value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
