// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  enum IPAddrType
    //  {
    //      IPv4 = 0,
    //      IPv6 = 1
    //  };

    //  ===========================================================================
    public class IPAddrType
    {
        public enum IPAddrTypeEnum
        {
            IPv4 = 0,
            IPv6 = 1,
        }
        public IPAddrTypeEnum InnerValue { get; set; } = default(IPAddrTypeEnum);

        public static IPAddrType Create(IPAddrTypeEnum v)
        {
            return new IPAddrType
            {
                InnerValue = v
            };
        }

        public static IPAddrType Decode(XdrDataInputStream stream)
        {
            int value = stream.ReadInt();
            switch (value)
            {
                case 0: return Create(IPAddrTypeEnum.IPv4);
                case 1: return Create(IPAddrTypeEnum.IPv6);
                default:
                    throw new Exception("Unknown enum value: " + value);
            }
        }

        public static void Encode(XdrDataOutputStream stream, IPAddrType value)
        {
            stream.WriteInt((int)value.InnerValue);
        }
    }
}
