using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class KeyPair
    {
        private byte[] _publicKey;
        private byte[] _privateKey;

        public KeyPair(byte[] publicKey)
            : this(publicKey, null)
        {

        }

        public KeyPair(byte[] publicKey, byte[] privateKey)
        {
            _publicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey), "publicKey cannot be null"); ;
            _privateKey = privateKey;
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
        public static KeyPair FromSecretSeed(char[] seed)
        {
            throw new NotImplementedException();

            //byte[] decoded = StrKey.decodeStellarSecretSeed(seed);
            //KeyPair keypair = fromSecretSeed(decoded);
            //Arrays.fill(decoded, (byte)0);
            //return keypair;
        }

        /// <summary>
        /// **INSECURE** Creates a new Stellar KeyPair from a strkey encoded Stellar secret seed.
        /// This method is insecure. Use only if you are aware of security implications.
        /// </summary>
        /// <param name="seed">The strkey encoded Stellar secret seed.</param>
        /// <returns><see cref="KeyPair"/></returns>
        public static KeyPair fromSecretSeed(String seed)
        {
            throw new NotImplementedException();

            //char[] charSeed = seed.toCharArray();
            //byte[] decoded = StrKey.decodeStellarSecretSeed(charSeed);
            //KeyPair keypair = fromSecretSeed(decoded);
            //Arrays.fill(charSeed, ' ');
            //return keypair;
        }

        /// <summary>
        ///  Creates a new Stellar keypair from a raw 32 byte secret seed.
        /// </summary>
        /// <param name="seed">seed The 32 byte secret seed.</param>
        /// <returns><see cref="KeyPair"/></returns>
        public static KeyPair FromSecretSeed(byte[] seed)
        {
            throw new NotImplementedException();

            //EdDSAPrivateKeySpec privKeySpec = new EdDSAPrivateKeySpec(seed, ed25519);
            //EdDSAPublicKeySpec publicKeySpec = new EdDSAPublicKeySpec(privKeySpec.getA().toByteArray(), ed25519);
            //return new KeyPair(new EdDSAPublicKey(publicKeySpec), new EdDSAPrivateKey(privKeySpec));
        }

        /**
 * 
 * @param 
 * @return {@link KeyPair}
 */

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

        /**
      * 
      * @param 
      * @return {@link KeyPair}
      */

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
        public static KeyPair random()
        {
            throw new NotImplementedException();
            //java.security.KeyPair keypair = new KeyPairGenerator().generateKeyPair();
            //return new KeyPair((EdDSAPublicKey)keypair.getPublic(), (EdDSAPrivateKey)keypair.getPrivate());
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
        public char[] getSecretSeed()
        {
            throw new NotImplementedException();
            //return StrKey.encodeStellarSecretSeed(_privateKey.getSeed());
        }

        public byte[] getPublicKey()
        {
            throw new NotImplementedException();
            //return mPublicKey.getAbyte();
        }

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

        //public static KeyPair fromXdrPublicKey(PublicKey key)
        //{
        //    throw new NotImplementedException();
        //    //return KeyPair.fromPublicKey(key.getEd25519().getUint256());
        //}

        //public static KeyPair fromXdrSignerKey(SignerKey key)
        //{
        //    throw new NotImplementedException();
        //    //return KeyPair.fromPublicKey(key.getEd25519().getUint256());
        //}

        /**
         * Sign the provided data with the keypair's private key.
         * @param data The data to sign.
         * @return signed bytes, null if the private key for this keypair is null.
         */
        public byte[] sign(byte[] data)
        {
            throw new NotImplementedException();
            //if (_privateKey == null)
            //{
            //    throw new RuntimeException("KeyPair does not contain secret key. Use KeyPair.fromSecretSeed method to create a new KeyPair with a secret key.");
            //}
            //try
            //{
            //    Signature sgr = new EdDSAEngine(MessageDigest.getInstance("SHA-512"));
            //    sgr.initSign(_privateKey);
            //    sgr.update(data);
            //    return sgr.sign();
            //}
            //catch (GeneralSecurityException e)
            //{
            //    throw new RuntimeException(e);
            //}
        }

        /**
         * Sign the provided data with the keypair's private key and returns {@link DecoratedSignature}.
         * @param data
         */
        //public DecoratedSignature signDecorated(byte[] data)
        //{
        //    throw new NotImplementedException();
        //    //byte[] signatureBytes = this.sign(data);

        //    //org.stellar.sdk.xdr.Signature signature = new org.stellar.sdk.xdr.Signature();
        //    //signature.setSignature(signatureBytes);

        //    //DecoratedSignature decoratedSignature = new DecoratedSignature();
        //    //decoratedSignature.setHint(this.getSignatureHint());
        //    //decoratedSignature.setSignature(signature);
        //    //return decoratedSignature;
        //}

        /**
         * Verify the provided data and signature match this keypair's public key.
         * @param data The data that was signed.
         * @param signature The signature.
         * @return True if they match, false otherwise.
         * @throws RuntimeException
         */
        public bool verify(byte[] data, byte[] signature)
        {
            throw new NotImplementedException();
            //try
            //{
            //    Signature sgr = new EdDSAEngine(MessageDigest.getInstance("SHA-512"));
            //    sgr.initVerify(mPublicKey);
            //    sgr.update(data);
            //    return sgr.verify(signature);
            //}
            //catch (SignatureException e)
            //{
            //    return false;
            //}
            //catch (GeneralSecurityException e)
            //{
            //    throw new RuntimeException(e);
            //}
        }


        //public bool equals(Object obj)
        //{
        //    return super.equals(obj);
        //}
    }
}
