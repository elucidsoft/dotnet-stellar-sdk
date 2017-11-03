using System;
using System.Linq;
using System.Security.Cryptography;
using Chaos.NaCl;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class KeyPair
    {
        public KeyPair(byte[] publicKey)
            : this(publicKey, null, null)
        {
        }

        public KeyPair(byte[] publicKey, byte[] privateKey, byte[] seed)
        {
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey), "publicKey cannot be null");
            ;
            PrivateKey = privateKey;
            SeedBytes = seed;
        }

        public byte[] PublicKey { get; }
        public byte[] PrivateKey { get; }
        public byte[] SeedBytes { get; }

        public string AccountId => StrKey.EncodeStellarAccountId(PublicKey);

        public string Address => StrKey.EncodeCheck(StrKey.VersionByte.ACCOUNT_ID, PublicKey);

        public string SecretSeed => StrKey.EncodeStellarSecretSeed(SeedBytes);

        public SignatureHint SignatureHint
        {
            get
            {
                var stream = new XdrDataOutputStream();
                var accountId = new AccountID(XdrPublicKey);
                AccountID.Encode(stream, accountId);
                var bytes = stream.ToArray();
                var length = bytes.Length;
                var signatureHintBytes = bytes.Skip(length - 4).Take(4).ToArray();

                var signatureHint = new SignatureHint(signatureHintBytes);
                return signatureHint;
            }
        }

        public PublicKey XdrPublicKey
        {
            get
            {
                var publicKey = new PublicKey
                {
                    Discriminant = new PublicKeyType {InnerValue = PublicKeyType.PublicKeyTypeEnum.PUBLIC_KEY_TYPE_ED25519}
                };

                var uint256 = new Uint256(PublicKey);
                publicKey.Ed25519 = uint256;

                return publicKey;
            }
        }

        public SignerKey XdrSignerKey
        {
            get
            {
                var signerKey = new SignerKey
                {
                    Discriminant = new SignerKeyType {InnerValue = SignerKeyType.SignerKeyTypeEnum.SIGNER_KEY_TYPE_ED25519}
                };

                var uint256 = new Uint256(PublicKey);
                signerKey.Ed25519 = uint256;

                return signerKey;
            }
        }

        public static KeyPair FromXdrPublicKey(PublicKey publicKey)
        {
            return FromPublicKey(publicKey.Ed25519.InnerValue);
        }

        public static KeyPair FromXdrSignerKey(SignerKey signerKey)
        {
            return FromPublicKey(signerKey.Ed25519.InnerValue);
        }

        /// <summary>
        ///     Returns true if this Keypair is capable of signing
        /// </summary>
        /// <returns></returns>
        public bool CanSign()
        {
            return PrivateKey != null;
        }

        /// <summary>
        ///     Creates a new Stellar KeyPair from a strkey encoded Stellar secret seed.
        /// </summary>
        /// <param name="seed">eed Char array containing strkey encoded Stellar secret seed.</param>
        /// <returns>
        ///     <see cref="KeyPair" />
        /// </returns>
        public static KeyPair FromSecretSeed(string seed)
        {
            var bytes = StrKey.DecodeStellarSecretSeed(seed);
            return FromSecretSeed(bytes);
        }

        /// <summary>
        ///     Creates a new Stellar keypair from a raw 32 byte secret seed.
        /// </summary>
        /// <param name="seed">seed The 32 byte secret seed.</param>
        /// <returns>
        ///     <see cref="KeyPair" />
        /// </returns>
        public static KeyPair FromSecretSeed(byte[] seed)
        {
            Ed25519.KeyPairFromSeed(out var publicKey, out var privateKey, seed);

            return new KeyPair(publicKey, privateKey, seed);
        }


        /// <summary>
        ///     Creates a new Stellar KeyPair from a strkey encoded Stellar account ID.
        /// </summary>
        /// <param name="accountId">accountId The strkey encoded Stellar account ID.</param>
        /// <returns>
        ///     <see cref="KeyPair" />
        /// </returns>
        public static KeyPair FromAccountId(string accountId)
        {
            var decoded = StrKey.DecodeStellarAccountId(accountId);
            return FromPublicKey(decoded);
        }

        /// <summary>
        ///     Creates a new Stellar keypair from a 32 byte address.
        /// </summary>
        /// <param name="publicKey">publicKey The 32 byte public key.</param>
        /// <returns>
        ///     <see cref="KeyPair" />
        /// </returns>
        public static KeyPair FromPublicKey(byte[] publicKey)
        {
            return new KeyPair(publicKey);
        }

        /// <summary>
        ///     Generates a random Stellar keypair.
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
        ///     Sign the provided data with the keypair's private key.
        /// </summary>
        /// <param name="data">The data to sign.</param>
        /// <returns>signed bytes, null if the private key for this keypair is null.</returns>
        public byte[] Sign(byte[] data)
        {
            if (PrivateKey == null)
                throw new Exception("KeyPair does not contain secret key. Use KeyPair.fromSecretSeed method to create a new KeyPair with a secret key.");

            return Ed25519.Sign(data, PrivateKey);
        }

        public DecoratedSignature SignDecorated(byte[] message)
        {
            var rawSig = Sign(message);

            return new DecoratedSignature
            {
                Hint = new SignatureHint(SignatureHint.InnerValue),
                Signature = new Signature(rawSig)
            };
        }

        /// <summary>
        ///     Verify the provided data and signature match this keypair's public key.
        /// </summary>
        /// <param name="data">The data that was signed.</param>
        /// <param name="signature">The signature.</param>
        /// <returns>True if they match, false otherwise.</returns>
        public bool Verify(byte[] data, byte[] signature)
        {
            var result = false;

            try
            {
                result = Ed25519.Verify(signature, data, PublicKey);
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}