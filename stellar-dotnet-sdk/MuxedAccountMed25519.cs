using System;
using System.ComponentModel;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class MuxedAccountMed25519 : IAccountId
    {
        public ulong Id { get; }
        public KeyPair Key { get; }
        public byte[] PublicKey => Key.PublicKey;

        /// <summary>
        /// Create a new MuxedAccountMed25519 with the given key and id.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        public MuxedAccountMed25519(KeyPair key, ulong id)
        {
            Id = id;
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        /// Create a new MuxedAccountMed25519 from the xdr object.
        /// </summary>
        /// <param name="muxed"></param>
        /// <returns></returns>
        public static MuxedAccountMed25519 FromMuxedAccountXdr(xdr.MuxedAccount.MuxedAccountMed25519 muxed)
        {
            var innerKey = KeyPair.FromPublicKey(muxed.Ed25519.InnerValue);
            var id = muxed.Id.InnerValue;
            return new MuxedAccountMed25519(innerKey, id);
        }

        /// <summary>
        /// Create a new MuxedAccountMed25519 from an account id in the format "M...".
        /// </summary>
        /// <param name="muxedAccountId"></param>
        /// <returns></returns>
        public static MuxedAccountMed25519 FromMuxedAccountId(string muxedAccountId)
        {
            var (id, data) = StrKey.DecodeStellarMuxedAccount(muxedAccountId);
            var key = KeyPair.FromPublicKey(data);
            return new MuxedAccountMed25519(key, id);
        }

        /// <summary>
        /// Get the xdr MuxedAccount.
        /// </summary>
        public xdr.MuxedAccount MuxedAccount
        {
            get
            {
                var muxedAccount = new xdr.MuxedAccount
                {
                    Discriminant = new CryptoKeyType { InnerValue = CryptoKeyType.CryptoKeyTypeEnum.KEY_TYPE_MUXED_ED25519 }
                };

                muxedAccount.Med25519 = new xdr.MuxedAccount.MuxedAccountMed25519();
                muxedAccount.Med25519.Id = new Uint64(Id);
                muxedAccount.Med25519.Ed25519 = new Uint256(Key.PublicKey);
                return muxedAccount;
            }
        }

        /// <summary>
        /// Get the MuxedAccount address, starting with M.
        /// </summary>
        public string Address => StrKey.EncodeStellarMuxedAccount(MuxedAccount);

        /// <summary>
        /// Get the MuxedAccount account id, starting with M.
        /// </summary>
        public string AccountId => Address;

        /// <summary>
        /// Return the signing key for the muxed account.
        /// </summary>
        public KeyPair SigningKey => Key;

        public bool IsMuxedAccount => true;
    }
}