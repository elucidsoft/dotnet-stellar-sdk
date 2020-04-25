using System;
using System.ComponentModel;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class MuxedAccountMed25519 : IAccountId
    {
        public ulong Id { get; }
        public KeyPair Key { get; }

        public MuxedAccountMed25519(ulong id, KeyPair key)
        {
            Id = id;
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        public static MuxedAccountMed25519 FromMuxedAccountXdr(xdr.MuxedAccount.MuxedAccountMed25519 muxed)
        {
            var innerKey = KeyPair.FromPublicKey(muxed.Ed25519.InnerValue);
            var id = muxed.Id.InnerValue;
            return new MuxedAccountMed25519(id, innerKey);
        }

        public xdr.MuxedAccount MuxedAccount => throw new NotImplementedException();
        public string Address => throw new NotImplementedException();
        public string AccountId => throw new NotImplementedException();
    }
}