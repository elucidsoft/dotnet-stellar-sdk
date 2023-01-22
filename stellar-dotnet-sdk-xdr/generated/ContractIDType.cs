// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum ContractIDType
    //  {
    //      CONTRACT_ID_FROM_SOURCE_ACCOUNT = 0,
    //      CONTRACT_ID_FROM_ED25519_PUBLIC_KEY = 1,
    //      CONTRACT_ID_FROM_ASSET = 2
    //  };

    //  ===========================================================================
    public class ContractIDType
    {
        public enum ContractIDTypeEnum
        {
            CONTRACT_ID_FROM_SOURCE_ACCOUNT = 0,
            CONTRACT_ID_FROM_ED25519_PUBLIC_KEY = 1,
            CONTRACT_ID_FROM_ASSET = 2,
        }
        public ContractIDTypeEnum InnerValue { get; set; } = default(ContractIDTypeEnum);

        public static ContractIDType Create(ContractIDTypeEnum v)
        {
            return new ContractIDType
            {
                InnerValue = v
            };
        }

        public static ContractIDType Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(ContractIDTypeEnum.CONTRACT_ID_FROM_SOURCE_ACCOUNT);
                case 1: return Create(ContractIDTypeEnum.CONTRACT_ID_FROM_ED25519_PUBLIC_KEY);
                case 2: return Create(ContractIDTypeEnum.CONTRACT_ID_FROM_ASSET);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, ContractIDType value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
