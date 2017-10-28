using Chaos.NaCl;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class KeyPair
    {
        private byte[] _publicKey;
        private byte[] _privateKey;
        private byte[] _seedBytes;

        public KeyPair(byte[] publicKey)
            : this(publicKey, null, null)
        {

        }

        public KeyPair(byte[] publicKey, byte[] privateKey, byte[] seed)
        {
            _publicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey), "publicKey cannot be null"); ;
            _privateKey = privateKey;
            _seedBytes = seed;
        }


        /// <summary>
        ///  Returns true if this Keypair is capable of signing
        /// </summary>
        /// <returns></returns>
        public bool CanSign()
        {
            return _privateKey != null;
        }

        /// <summary>
        /// Creates a new Stellar KeyPair from a strkey encoded Stellar secret seed.
        /// </summary>
        /// <param name="seed">eed Char array containing strkey encoded Stellar secret seed.</param>
        /// <returns><see cref="KeyPair"/></returns>
        public static KeyPair FromSecretSeed(string seed)
        {
            byte[] decoded = StrKey.DecodeStellarSecretSeed(seed);
            KeyPair keypair = FromSecretSeed(decoded);
            Array.Fill(decoded, (byte)0);
            return keypair;
        }

        /// <summary>
        ///  Creates a new Stellar keypair from a raw 32 byte secret seed.
        /// </summary>
        /// <param name="seed">seed The 32 byte secret seed.</param>
        /// <returns><see cref="KeyPair"/></returns>
        public static KeyPair FromSecretSeed(byte[] seed)
        {
            Ed25519.KeyPairFromSeed(out byte[] publicKey, out byte[] privateKey, seed);

            return new KeyPair(publicKey, privateKey, seed);
        }


        /// <summary>
        /// Creates a new Stellar KeyPair from a strkey encoded Stellar account ID.
        /// </summary>
        /// <param name="accountId">accountId The strkey encoded Stellar account ID.</param>
        /// <returns><see cref="KeyPair"/></returns>
        public static KeyPair fromAccountId(String accountId)
        {
            throw new NotImplementedException();
            //byte[] decoded = StrKey.decodeStellarAccountId(accountId);
            //return fromPublicKey(decoded);
        }

        /// <summary>
        /// Creates a new Stellar keypair from a 32 byte address.
        /// </summary>
        /// <param name="publicKey">publicKey The 32 byte public key.</param>
        /// <returns><see cref="KeyPair"/></returns>
        public static KeyPair fromPublicKey(byte[] publicKey)
        {
            throw new NotImplementedException();
            //EdDSAPublicKeySpec publicKeySpec = new EdDSAPublicKeySpec(publicKey, ed25519);
            //return new KeyPair(new EdDSAPublicKey(publicKeySpec));
        }

        /// <summary>
        /// Generates a random Stellar keypair.
        /// </summary>
        /// <returns>a random Stellar keypair</returns>
        public static KeyPair Random()
        {
            var b = new byte[32];
            using (var rngCrypto = new RNGCryptoServiceProvider())
            {
                rngCrypto.GetBytes(b);
            }
            return FromSecretSeed(b);
        }

        /// <summary>
        /// Returns the human readable account ID encoded in strkey.
        /// </summary>
        /// <returns></returns>
        public String getAccountId()
        {
            throw new NotImplementedException();
            //return StrKey.encodeStellarAccountId(mPublicKey.getAbyte());
        }

        /**
         * Returns the human readable secret seed encoded in strkey.
         */

        //TODO: Implement once XDR objects are defined.
        //public SignatureHint getSignatureHint()
        //{
        //    throw new NotImplementedException();
        //    //try
        //    //{
        //    //    ByteArrayOutputStream publicKeyBytesStream = new ByteArrayOutputStream();
        //    //    XdrDataOutputStream xdrOutputStream = new XdrDataOutputStream(publicKeyBytesStream);
        //    //    PublicKey.encode(xdrOutputStream, this.getXdrPublicKey());
        //    //    byte[] publicKeyBytes = publicKeyBytesStream.toByteArray();
        //    //    byte[] signatureHintBytes = Arrays.copyOfRange(publicKeyBytes, publicKeyBytes.length - 4, publicKeyBytes.length);

        //    //    SignatureHint signatureHint = new SignatureHint();
        //    //    signatureHint.setSignatureHint(signatureHintBytes);
        //    //    return signatureHint;
        //    //}
        //    //catch (IOException e)
        //    //{
        //    //    throw new AssertionError(e);
        //    //}
        //}

        //TODO: Implement once XDR objects are defined.
        //public PublicKey getXdrPublicKey()
        //{
        //    throw new NotImplementedException();
        //    //PublicKey publicKey = new PublicKey();
        //    //publicKey.setDiscriminant(PublicKeyType.PUBLIC_KEY_TYPE_ED25519);
        //    //Uint256 uint256 = new Uint256();
        //    //uint256.setUint256(getPublicKey());
        //    //publicKey.setEd25519(uint256);
        //    //return publicKey;
        //}

        //TODO: Implement once XDR objects are defined.
        //public SignerKey getXdrSignerKey()
        //{
        //    throw new NotImplementedException();
        //    //SignerKey signerKey = new SignerKey();
        //    //signerKey.setDiscriminant(SignerKeyType.SIGNER_KEY_TYPE_ED25519);
        //    //Uint256 uint256 = new Uint256();
        //    //uint256.setUint256(getPublicKey());
        //    //signerKey.setEd25519(uint256);
        //    //return signerKey;
        //}

        //TODO: Implement once XDR objects are defined.
        //public static KeyPair fromXdrPublicKey(PublicKey key)
        //{
        //    throw new NotImplementedException();
        //    //return KeyPair.fromPublicKey(key.getEd25519().getUint256());
        //}

        //TODO: Implement once XDR objects are defined.
        //public static KeyPair fromXdrSignerKey(SignerKey key)
        //{
        //    throw new NotImplementedException();
        //    //return KeyPair.fromPublicKey(key.getEd25519().getUint256());
        //}

        /// <summary>
        /// Sign the provided data with the keypair's private key.
        /// </summary>
        /// <param name="data">The data to sign.</param>
        /// <returns>signed bytes, null if the private key for this keypair is null.</returns>
        public byte[] Sign(byte[] data)
        {
            if (_privateKey == null)
            {
                throw new Exception("KeyPair does not contain secret key. Use KeyPair.fromSecretSeed method to create a new KeyPair with a secret key.");
            }

            return Ed25519.Sign(data, _privateKey);
        }

        //TODO: Implement once XDR objects are defined.
        ///**
        // * Sign the provided data with the keypair's private key and returns {@link DecoratedSignature}.
        // * @param data
        // */
        //public DecoratedSignature signDecorated(byte[] data)
        //{


        //    //byte[] signatureBytes = Sign(data);

        //    //org.stellar.sdk.xdr.Signature signature = new org.stellar.sdk.xdr.Signature();
        //    //signature.setSignature(signatureBytes);

        //    //DecoratedSignature decoratedSignature = new DecoratedSignature();
        //    //decoratedSignature.setHint(this.getSignatureHint());
        //    //decoratedSignature.setSignature(signature);
        //    //return decoratedSignature;
        //}

        /// <summary>
        /// Verify the provided data and signature match this keypair's public key.
        /// </summary>
        /// <param name="data">The data that was signed.</param>
        /// <param name="signature">The signature.</param>
        /// <returns>True if they match, false otherwise.</returns>
        public bool Verify(byte[] data, byte[] signature)
        {
            var result = false;

            try
            {
                result = Ed25519.Verify(signature, data, _publicKey);
            }
            catch { result = false; }

            return result;
        }

        public string Address
        {
            get
            {
                return StrKey.EncodeCheck(StrKey.VersionByte.ACCOUNT_ID, _publicKey);
            }
        }

        public string SecretSeed
        {
            get
            {
                return StrKey.EncodeStellarSecretSeed(_seedBytes);
            }
        }

        public byte[] PublicKey
        {
            get { return _publicKey; }
        }
    }
}
