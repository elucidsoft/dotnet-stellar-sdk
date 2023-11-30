// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten

using System;

namespace stellar_dotnet_sdk.xdr;

// === xdr source ============================================================

//  enum CryptoKeyType
//  {
//      KEY_TYPE_ED25519 = 0,
//      KEY_TYPE_PRE_AUTH_TX = 1,
//      KEY_TYPE_HASH_X = 2,
//      KEY_TYPE_ED25519_SIGNED_PAYLOAD = 3,
//      // MUXED enum values for supported type are derived from the enum values
//      // above by ORing them with 0x100
//      KEY_TYPE_MUXED_ED25519 = 0x100
//  };

//  ===========================================================================
public class CryptoKeyType
{
    public enum CryptoKeyTypeEnum
    {
        KEY_TYPE_ED25519 = 0,
        KEY_TYPE_PRE_AUTH_TX = 1,
        KEY_TYPE_HASH_X = 2,
        KEY_TYPE_ED25519_SIGNED_PAYLOAD = 3,
        KEY_TYPE_MUXED_ED25519 = 256
    }

    public CryptoKeyTypeEnum InnerValue { get; set; } = default;

    public static CryptoKeyType Create(CryptoKeyTypeEnum v)
    {
        return new CryptoKeyType
        {
            InnerValue = v
        };
    }

    public static CryptoKeyType Decode(XdrDataInputStream stream)
    {
        var value = stream.ReadInt();
        return value switch
        {
            0 => Create(CryptoKeyTypeEnum.KEY_TYPE_ED25519),
            1 => Create(CryptoKeyTypeEnum.KEY_TYPE_PRE_AUTH_TX),
            2 => Create(CryptoKeyTypeEnum.KEY_TYPE_HASH_X),
            3 => Create(CryptoKeyTypeEnum.KEY_TYPE_ED25519_SIGNED_PAYLOAD),
            256 => Create(CryptoKeyTypeEnum.KEY_TYPE_MUXED_ED25519),
            _ => throw new Exception("Unknown enum value: " + value)
        };
    }

    public static void Encode(XdrDataOutputStream stream, CryptoKeyType value)
    {
        stream.WriteInt((int)value.InnerValue);
    }
}