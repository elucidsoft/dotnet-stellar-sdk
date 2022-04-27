using System;
using xdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    ///     Signer is a helper class that creates <see cref="xdr.SignerKey" /> objects.
    /// </summary>
    public class Signer
    {
        /// <summary>
        ///     Create <code>ed25519PublicKey</code> <see cref="xdr.SignerKey" /> from
        ///     a <see cref="xdr.KeyPair" />
        /// </summary>
        /// <param name="keyPair"></param>
        /// <returns>sdkxdr.SignerKey</returns>
        public static xdr.SignerKey Ed25519PublicKey(KeyPair keyPair)
        {
            if (keyPair == null)
                throw new ArgumentNullException(nameof(keyPair), "keyPair cannot be null");

            return keyPair.XdrSignerKey;
        }

        /// <summary>
        ///     Create <code>sha256Hash</code> <see cref="xdr.SignerKey" /> from
        ///     a sha256 hash of a preimage.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>sdkxdr.SignerKey</returns>
        public static xdr.SignerKey Sha256Hash(byte[] hash)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "hash cannot be null");

            var signerKey = new xdr.SignerKey();
            var value = CreateUint256(hash);

            signerKey.Discriminant = xdr.SignerKeyType.Create(xdr.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_HASH_X);
            signerKey.HashX = value;

            return signerKey;
        }

        /// <summary>
        ///     Create <code>preAuthTx</code> <see cref="xdr.SignerKey" /> from
        ///     a <see cref="xdr.Transaction" /> hash.
        /// </summary>
        /// <param name="tx"></param>
        /// <returns>sdkxdr.SignerKey</returns>
        public static xdr.SignerKey PreAuthTx(Transaction tx)
        {
            return PreAuthTx(tx, Network.Current);
        }

        /// <summary>
        ///     Create <code>preAuthTx</code> <see cref="xdr.SignerKey" /> from
        ///     a <see cref="xdr.Transaction" /> hash.
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="network"></param>
        /// <returns>sdkxdr.SignerKey</returns>
        public static xdr.SignerKey PreAuthTx(Transaction tx, Network network)
        {
            if (tx == null)
                throw new ArgumentNullException(nameof(tx), "tx cannot be null");

            return PreAuthTx(tx.Hash(network));
        }

        /// <summary>
        ///     Create <code>preAuthTx</code> <see cref="xdr.SignerKey" /> from
        ///     a transaction hash.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>sdkxdr.SignerKey</returns>
        public static xdr.SignerKey PreAuthTx(byte[] hash)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "hash cannot be null");

            var signerKey = new xdr.SignerKey();
            var value = CreateUint256(hash);

            signerKey.Discriminant = xdr.SignerKeyType.Create(xdr.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_PRE_AUTH_TX);
            signerKey.PreAuthTx = value;

            return signerKey;
        }

        /// <summary>
        /// Create SignerKey from SignedPayloadSigner
        /// </summary>
        /// <param name="signedPayloadSigner"></param>
        /// <returns></returns>
        public static xdr.SignerKey SignedPayload(SignedPayloadSigner signedPayloadSigner)
        {
            xdr.SignerKey signerKey = new xdr.SignerKey();
            xdr.SignerKey.SignerKeyEd25519SignedPayload payloadSigner = new xdr.SignerKey.SignerKeyEd25519SignedPayload();
            payloadSigner.Payload = signedPayloadSigner.Payload;
            payloadSigner.Ed25519 = signedPayloadSigner.SignerAccountID.InnerValue.Ed25519;

            signerKey.Discriminant.InnerValue = xdr.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_ED25519_SIGNED_PAYLOAD;
            signerKey.Ed25519SignedPayload = payloadSigner;

            return signerKey;
        }

        /// <summary>
        ///     Creates a Uint256 from a byte hash.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>A Uint256</returns>
        private static xdr.Uint256 CreateUint256(byte[] hash)
        {
            if (hash.Length != 32)
                throw new ArgumentException("hash must be 32 bytes long");

            var value = new xdr.Uint256();
            value.InnerValue = hash;
            return value;
        }
    }
}