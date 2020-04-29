using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public static class MuxedAccount
    {
        public static IAccountId FromXdrMuxedAccount(xdr.MuxedAccount muxedAccount)
        {
            switch (muxedAccount.Discriminant.InnerValue)
            {
                case CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_ED25519:
                    return KeyPair.FromPublicKey(muxedAccount.Ed25519.InnerValue);
                case CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_MUXED_ED25519:
                    return MuxedAccountMed25519.FromMuxedAccountXdr(muxedAccount.Med25519);
                default:
                    throw new InvalidOperationException("Invalid MuxedAccount type");
            }

        }
    }
}