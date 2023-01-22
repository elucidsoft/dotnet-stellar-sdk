// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum ContractEventType
    //  {
    //      SYSTEM = 0,
    //      CONTRACT = 1
    //  };

    //  ===========================================================================
    public class ContractEventType
    {
        public enum ContractEventTypeEnum
        {
            SYSTEM = 0,
            CONTRACT = 1,
        }
        public ContractEventTypeEnum InnerValue { get; set; } = default(ContractEventTypeEnum);

        public static ContractEventType Create(ContractEventTypeEnum v)
        {
            return new ContractEventType
            {
                InnerValue = v
            };
        }

        public static ContractEventType Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(ContractEventTypeEnum.SYSTEM);
                case 1: return Create(ContractEventTypeEnum.CONTRACT);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, ContractEventType value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
