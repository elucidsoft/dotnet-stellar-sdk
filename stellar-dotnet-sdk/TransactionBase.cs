using System;
using System.Collections.Generic;
using System.Linq;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public abstract class TransactionBase
    {
        /// <summary>
        /// Feature flag to control which version of the envelope is generated.
        /// </summary>
        public enum TransactionXdrVersion
        {
            V0,
            V1
        }

        public TransactionBase(uint fee)
        {
            Fee = fee;
            Signatures = new List<DecoratedSignature>();
        }

        public uint Fee { get; }

        public List<DecoratedSignature> Signatures { get; }

        /// <summary>
        ///     Returns signature base for the given network.
        /// </summary>
        /// <param name="network">The network <see cref="Network"/> the transaction will be sent to.</param>
        /// <returns></returns>
        public abstract byte[] SignatureBase(Network network);

        /// <summary>
        ///     Generates TransactionEnvelope XDR object. Transaction need to have at least one signature.
        /// </summary>
        /// <param name="version">The envelope version to generated. Defaults to V0</param>
        /// <returns></returns>
        public abstract TransactionEnvelope ToEnvelopeXdr(TransactionXdrVersion version = TransactionXdrVersion.V0);


        /// <summary>
        ///     Generates TransactionEnvelope XDR object. This transaction MUST be signed before being useful
        /// </summary>
        /// <param name="version">The envelope version to generated. Defaults to V0</param>
        /// <returns></returns>
        public abstract TransactionEnvelope ToUnsignedEnvelopeXdr(TransactionXdrVersion version = TransactionXdrVersion.V0);

        /// <summary>
        ///     Returns signature base.
        /// </summary>
        /// <returns></returns>
        public byte[] SignatureBase()
        {
            return SignatureBase(Network.Current);
        }

        /// <summary>
        /// Adds a new signature ed25519PublicKey to this transaction.
        /// </summary>
        /// <param name="signer"> signer <see cref="KeyPair"/> object representing a signer</param>
        public void Sign(KeyPair signer)
        {
            Sign(signer, Network.Current);
        }

        /// <summary>
        /// Adds a new signature ed25519PublicKey to this transaction.
        /// </summary>
        /// <param name="signer"> signer <see cref="KeyPair"/> object representing a signer</param>
        /// <param name="network">The network <see cref="Network"/> the transaction will be sent to.</param>
        public void Sign(KeyPair signer, Network network)
        {
            if (signer == null)
                throw new ArgumentNullException(nameof(signer), "signer cannot be null");

            var txHash = Hash(network);
            Signatures.Add(signer.SignDecorated(txHash));
        }

        /// <summary>
        ///     Adds a new sha256Hash signature to this transaction by revealing preimage.
        /// </summary>
        /// <param name="preimage">the sha256 hash of preimage should be equal to signer hash</param>
        public void Sign(byte[] preimage)
        {
            var signature = new Signature
            {
                InnerValue = preimage ?? throw new ArgumentNullException(nameof(preimage), "preimage cannot be null")
            };

            var hash = Util.Hash(preimage);

            var length = hash.Length;
            var signatureHintBytes = hash.Skip(length - 4).Take(4).ToArray();

            var signatureHint = new SignatureHint {InnerValue = signatureHintBytes};

            var decoratedSignature = new DecoratedSignature
            {
                Hint = signatureHint,
                Signature = signature
            };

            Signatures.Add(decoratedSignature);
        }

        /// <summary>
        ///     Returns transaction hash.
        /// </summary>
        /// <returns></returns>
        public byte[] Hash()
        {
            return Hash(Network.Current);
        }

        /// <summary>
        ///     Returns transaction hash for the given network.
        /// </summary>
        /// <param name="network">The network <see cref="Network"/> the transaction will be sent to.</param>
        /// <returns></returns>
        public byte[] Hash(Network network)
        {
            return Util.Hash(SignatureBase(network));
        }

        /// <summary>
        ///     Generates TransactionEnvelope XDR object. This transaction MUST be signed before being useful
        /// </summary>
        /// <returns></returns>
        public string ToUnsignedEnvelopeXdrBase64()
        {
            var envelope = ToUnsignedEnvelopeXdr();
            var writer = new XdrDataOutputStream();
            TransactionEnvelope.Encode(writer, envelope);

            return Convert.ToBase64String(writer.ToArray());
        }

        /// <summary>
        ///     Returns base64-encoded TransactionEnvelope XDR object. Transaction need to have at least one signature.
        /// </summary>
        /// <returns></returns>
        public string ToEnvelopeXdrBase64()
        {
            var envelope = ToEnvelopeXdr();
            var writer = new XdrDataOutputStream();
            TransactionEnvelope.Encode(writer, envelope);

            return Convert.ToBase64String(writer.ToArray());
        }

    }
}