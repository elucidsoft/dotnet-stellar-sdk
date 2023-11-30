using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public static class MuxedAccount
    {
        public static IAccountId FromXdrMuxedAccount(xdr.MuxedAccount muxedAccount)
        {
            return muxedAccount.Discriminant.InnerValue switch
            {
                CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_ED25519 => KeyPair.FromPublicKey(
                    muxedAccount.Ed25519.InnerValue),
                CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_MUXED_ED25519 => MuxedAccountMed25519.FromMuxedAccountXdr(
                    muxedAccount.Med25519),
                _ => throw new InvalidOperationException("Invalid MuxedAccount type")
            };
        }
    }
}