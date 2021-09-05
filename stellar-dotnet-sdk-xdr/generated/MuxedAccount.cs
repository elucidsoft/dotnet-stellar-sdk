// Automatically generated by xdrgen
// DO NOT EDIT or your changes may be overwritten
using System;

namespace stellar_dotnet_sdk.xdr
{

    // === xdr source ============================================================

    //  union MuxedAccount switch (CryptoKeyType type)
    //  {
    //  case KEY_TYPE_ED25519:
    //      uint256 ed25519;
    //  case KEY_TYPE_MUXED_ED25519:
    //      struct
    //      {
    //          uint64 id;
    //          uint256 ed25519;
    //      } med25519;
    //  };

    //  ===========================================================================
    public class MuxedAccount
    {
        public MuxedAccount() { }

        public CryptoKeyType Discriminant { get; set; } = new CryptoKeyType();

        public Uint256 Ed25519 { get; set; }
        public MuxedAccountMed25519 Med25519 { get; set; }
        public static void Encode(XdrDataOutputStream stream, MuxedAccount encodedMuxedAccount)
        {
            stream.WriteInt((int)encodedMuxedAccount.Discriminant.InnerValue);
            switch (encodedMuxedAccount.Discriminant.InnerValue)
            {
                case CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_ED25519:
                    Uint256.Encode(stream, encodedMuxedAccount.Ed25519);
                    break;
                case CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_MUXED_ED25519:
                    MuxedAccountMed25519.Encode(stream, encodedMuxedAccount.Med25519);
                    break;
            }
        }
        public static MuxedAccount Decode(XdrDataInputStream stream)
        {
            MuxedAccount decodedMuxedAccount = new MuxedAccount();
            CryptoKeyType discriminant = CryptoKeyType.Decode(stream);
            decodedMuxedAccount.Discriminant = discriminant;
            switch (decodedMuxedAccount.Discriminant.InnerValue)
            {
                case CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_ED25519:
                    decodedMuxedAccount.Ed25519 = Uint256.Decode(stream);
                    break;
                case CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_MUXED_ED25519:
                    decodedMuxedAccount.Med25519 = MuxedAccountMed25519.Decode(stream);
                    break;
            }
            return decodedMuxedAccount;
        }

        public class MuxedAccountMed25519
        {
            public MuxedAccountMed25519() { }
            public Uint64 Id { get; set; }
            public Uint256 Ed25519 { get; set; }

            public static void Encode(XdrDataOutputStream stream, MuxedAccountMed25519 encodedMuxedAccountMed25519)
            {
                Uint64.Encode(stream, encodedMuxedAccountMed25519.Id);
                Uint256.Encode(stream, encodedMuxedAccountMed25519.Ed25519);
            }
            public static MuxedAccountMed25519 Decode(XdrDataInputStream stream)
            {
                MuxedAccountMed25519 decodedMuxedAccountMed25519 = new MuxedAccountMed25519();
                decodedMuxedAccountMed25519.Id = Uint64.Decode(stream);
                decodedMuxedAccountMed25519.Ed25519 = Uint256.Decode(stream);
                return decodedMuxedAccountMed25519;
            }

        }
    }
}
