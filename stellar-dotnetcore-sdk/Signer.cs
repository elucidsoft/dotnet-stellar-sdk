using System;
using System.Collections.Generic;
using System.Text;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    /// <summary>
    /// Signer is a helper class that creates <see cref="sdkxdr.SignerKey"/> objects.
    /// </summary>
    public class Signer
    {
        ///<summary>
        /// Create <code>ed25519PublicKey</code> <see cref="sdkxdr.SignerKey"/> from
        /// a <see cref="sdkxdr.KeyPair"/>
        ///</summary>
        ///<param name="keyPair"></param>
        ///<returns>sdkxdr.SignerKey</returns>
        public static sdkxdr.SignerKey Ed25519PublicKey(KeyPair keyPair)
        {
            if (keyPair == null)
                throw new ArgumentNullException(nameof(keyPair), "keyPair cannot be null");

            return keyPair.XdrSignerKey;
        }

        ///<summary>
        /// Create <code>sha256Hash</code> <see cref="sdkxdr.SignerKey"/> from
        /// a sha256 hash of a preimage.
        ///</summary>
        ///<param name="hash"></param>
        ///<returns>sdkxdr.SignerKey</returns>

        public static sdkxdr.SignerKey Sha256Hash(byte[] hash)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "hash cannot be null");

            sdkxdr.SignerKey signerKey = new sdkxdr.SignerKey();
            sdkxdr.Uint256 value = Signer.CreateUint256(hash);

            signerKey.Discriminant = sdkxdr.SignerKeyType.Create(sdkxdr.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_HASH_X);
            signerKey.HashX = value;

            return signerKey;
        }

        ///<summary>
        /// Create <code>preAuthTx</code> <see cref="sdkxdr.SignerKey"/> from
        /// a <see cref="sdkxdr.Transaction"/> hash.
        ///</summary>
        ///<param name="tx"></param>
        ///<returns>sdkxdr.SignerKey</returns>

        public static sdkxdr.SignerKey PreAuthTx(Transaction tx)
        {
            if (tx == null)
                throw new ArgumentNullException(nameof(tx), "tx cannot be null");

            sdkxdr.SignerKey signerKey = new sdkxdr.SignerKey();
            sdkxdr.Uint256 value = Signer.CreateUint256(tx.Hash());

            signerKey.Discriminant = sdkxdr.SignerKeyType.Create(sdkxdr.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_PRE_AUTH_TX);
            signerKey.PreAuthTx = value;

            return signerKey;
        }

        ///<summary>
        /// Create <code>preAuthTx</code> <see cref="sdkxdr.SignerKey"/> from
        /// a transaction hash.
        ///</summary>
        ///<param name="hash"></param>
        ///<returns>sdkxdr.SignerKey</returns> 
        public static sdkxdr.SignerKey PreAuthTx(byte[] hash)
        {
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "hash cannot be null");

            sdkxdr.SignerKey signerKey = new sdkxdr.SignerKey();
            sdkxdr.Uint256 value = Signer.CreateUint256(hash);

            signerKey.Discriminant = sdkxdr.SignerKeyType.Create(sdkxdr.SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_PRE_AUTH_TX);
            signerKey.PreAuthTx = value;

            return signerKey;
        }

        /// <summary>
        /// Creates a Uint256 from a byte hash.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>A Uint256</returns>
        private static sdkxdr.Uint256 CreateUint256(byte[] hash)
        {
            if (hash.Length != 32)
            {
                throw new ArgumentException("hash must be 32 bytes long");
            }

            sdkxdr.Uint256 value = new sdkxdr.Uint256();
            value.InnerValue = hash;
            return value;
        }
    }
}
