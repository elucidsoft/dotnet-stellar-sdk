using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Data model for the https://github.com/stellar/stellar-protocol/blob/master/core/cap-0040.md#xdr-changes signed payload signer
    /// </summary>
    public class SignedPayloadSigner
    {
        public const int SIGNED_PAYLOAD_MAX_PAYLOAD_LENGTH = 64;

        public xdr.AccountID SignerAccountID { get; private set; }
        public byte[] Payload { get; private set; }

        public SignedPayloadSigner(xdr.AccountID signerAccountID, byte[] payload)
        {
            if (signerAccountID == null)
            {
                throw new ArgumentNullException(nameof(signerAccountID), "signerAccountID cannot be null");
            }

            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "payload cannot be null");
            }

            if (payload.Length > SIGNED_PAYLOAD_MAX_PAYLOAD_LENGTH)
            {
                throw new ArgumentException($"Invalid payload length, must be less than {SIGNED_PAYLOAD_MAX_PAYLOAD_LENGTH}");
            }

            if (signerAccountID.InnerValue.Discriminant == null || signerAccountID.InnerValue.Discriminant.InnerValue != xdr.PublicKeyType.PublicKeyTypeEnum.PUBLIC_KEY_TYPE_ED25519)
            {
                throw new ArgumentException("Invalid payload signer, only ED25519 public key accounts are supported at the moment");
            }

            SignerAccountID = signerAccountID;
            Payload = payload;
        }

        public SignedPayloadSigner(byte[] signerED25519PublicKey, byte[] payload)
        {
            var publicKeyXDR = new xdr.PublicKey();
            publicKeyXDR.Discriminant.InnerValue = xdr.PublicKeyType.PublicKeyTypeEnum.PUBLIC_KEY_TYPE_ED25519;
            publicKeyXDR.Ed25519 = new xdr.Uint256(signerED25519PublicKey);
            new xdr.AccountID(publicKeyXDR);
            Payload = payload;
        }
    }
}