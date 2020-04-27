using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class Transaction : TransactionBase
    {
        public Transaction(IAccountId sourceAccount, uint fee, long sequenceNumber, Operation[] operations, Memo memo, TimeBounds timeBounds)
        {
            SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
            Fee = fee;
            SequenceNumber = sequenceNumber;
            Operations = operations ?? throw new ArgumentNullException(nameof(operations), "operations cannot be null");

            if (operations.Length == 0)
                throw new ArgumentNullException(nameof(operations), "At least one operation required");

            Memo = memo ?? Memo.None();
            TimeBounds = timeBounds;
        }

        public uint Fee { get; }

        public IAccountId SourceAccount { get; }

        public long SequenceNumber { get; }

        public Operation[] Operations { get; }

        public Memo Memo { get; }

        public TimeBounds TimeBounds { get; }

        /// <summary>
        ///     Returns signature base for the given network.
        /// </summary>
        /// <param name="network">The network <see cref="Network"/> the transaction will be sent to.</param>
        /// <returns></returns>
        public override byte[] SignatureBase(Network network)
        {
            if (network == null)
                throw new NoNetworkSelectedException();

            var writer = new XdrDataOutputStream();

            // Hashed NetworkID
            writer.Write(network.NetworkId);

            // Envelope Type - 4 bytes
            EnvelopeType.Encode(writer, EnvelopeType.Create(EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX));

            // Transaction XDR bytes
            var txWriter = new XdrDataOutputStream();
            xdr.PublicKeyType.Encode(txWriter,
                new PublicKeyType {InnerValue = PublicKeyType.PublicKeyTypeEnum.PUBLIC_KEY_TYPE_ED25519});
            xdr.TransactionV0.Encode(txWriter, ToXdrV0());

            writer.Write(txWriter.ToArray());

            return writer.ToArray();
        }


        /// <summary>
        ///     Generates Transaction XDR object.
        /// </summary>
        /// <returns></returns>
        public TransactionV0 ToXdr()
        {
            return ToXdrV0();
        }

        /// <summary>
        ///     Generates Transaction XDR object.
        /// </summary>
        /// <returns></returns>
        public TransactionV0 ToXdrV0()
        {
            if (!(SourceAccount is KeyPair))
                throw new Exception("TransactionEnvelope V0 expects a KeyPair source account");

            // fee
            var fee = new Uint32 {InnerValue = Fee};

            // sequenceNumber
            var sequenceNumberUint = new xdr.Int64(SequenceNumber);
            var sequenceNumber = new SequenceNumber {InnerValue = sequenceNumberUint};

            // sourceAccount
            var sourceAccount = new Uint256(SourceAccount.PublicKey);

            // operations
            var operations = new xdr.Operation[Operations.Length];

            for (var i = 0; i < Operations.Length; i++)
                operations[i] = Operations[i].ToXdr();

            // ext
            var ext = new TransactionV0.TransactionV0Ext {Discriminant = 0};

            var transaction = new TransactionV0
            {
                Fee = fee,
                SeqNum = sequenceNumber,
                SourceAccountEd25519 = sourceAccount,
                Operations = operations,
                Memo = Memo.ToXdr(),
                TimeBounds = TimeBounds?.ToXdr(),
                Ext = ext
            };
            return transaction;
        }

        /// <summary>
        ///     Generates Transaction XDR object.
        /// </summary>
        /// <returns></returns>
        public xdr.Transaction ToXdrV1()
        {
            // fee
            var fee = new Uint32 {InnerValue = Fee};

            // sequenceNumber
            var sequenceNumberUint = new xdr.Int64(SequenceNumber);
            var sequenceNumber = new SequenceNumber {InnerValue = sequenceNumberUint};

            // sourceAccount
            var sourceAccount = SourceAccount.MuxedAccount;

            // operations
            var operations = new xdr.Operation[Operations.Length];

            for (var i = 0; i < Operations.Length; i++)
                operations[i] = Operations[i].ToXdr();

            // ext
            var ext = new xdr.Transaction.TransactionExt {Discriminant = 0};

            var transaction = new xdr.Transaction
            {
                Fee = fee,
                SeqNum = sequenceNumber,
                SourceAccount = sourceAccount,
                Operations = operations,
                Memo = Memo.ToXdr(),
                TimeBounds = TimeBounds?.ToXdr(),
                Ext = ext
            };
            return transaction;
        }

        /// <summary>
        ///     Generates TransactionEnvelope XDR object. Transaction need to have at least one signature.
        /// </summary>
        /// <returns></returns>
        public override TransactionEnvelope ToEnvelopeXdr(TransactionXdrVersion version = TransactionXdrVersion.V0)
        {
            if (Signatures.Count == 0)
                throw new NotEnoughSignaturesException("Transaction must be signed by at least one signer. Use transaction.sign().");

            return ToEnvelopeXdr(version, Signatures.ToArray());
        }

        /// <summary>
        ///     Generates TransactionEnvelope XDR object. This transaction MUST be signed before being useful
        /// </summary>
        /// <returns></returns>
        public override TransactionEnvelope ToUnsignedEnvelopeXdr(TransactionXdrVersion version = TransactionXdrVersion.V0)
        {
            if (Signatures.Count > 0)
                throw new TooManySignaturesException("Transaction must not be signed. Use ToEnvelopeXDR.");

            return ToEnvelopeXdr(version, new DecoratedSignature[0]);
        }

        private TransactionEnvelope ToEnvelopeXdr(TransactionXdrVersion version, DecoratedSignature[] signatures)
        {
            var thisXdr = new TransactionEnvelope();
            if (version == TransactionXdrVersion.V0)
            {
                thisXdr.Discriminant = new EnvelopeType {InnerValue = EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX_V0};
                thisXdr.V0 = new TransactionV0Envelope();

                var transaction = ToXdrV0();
                thisXdr.V0.Tx = transaction;
                thisXdr.V0.Signatures = signatures;
            } else if (version == TransactionXdrVersion.V1)
            {
                thisXdr.Discriminant = new EnvelopeType {InnerValue = EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX};
                thisXdr.V1 = new TransactionV1Envelope();
                var transaction = ToXdrV1();
                thisXdr.V1.Tx = transaction;
                thisXdr.V1.Signatures = signatures;
            }
            else
            {
                throw new Exception($"Invalid TransactionXdrVersion {version}");
            }
            return thisXdr;
        }

        public static Transaction FromEnvelopeXdr(string envelope)
        {
            byte[] bytes = Convert.FromBase64String(envelope);

            TransactionEnvelope transactionEnvelope = TransactionEnvelope.Decode(new XdrDataInputStream(bytes));
            return FromEnvelopeXdr(transactionEnvelope);
        }

        public static Transaction FromEnvelopeXdr(TransactionEnvelope envelope)
        {
            {
                switch (envelope.Discriminant.InnerValue)
                {
                    case EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX_V0:
                        return FromEnvelopeXdrV0(envelope.V0);
                    case EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX:
                        return FromEnvelopeXdrV1(envelope.V1);
                    default:
                        throw new ArgumentException($"Invalid TransactionEnvelope: expected an ENVELOPE_TYPE_TX or ENVELOPE_TYPE_TX_V0 but received {envelope.Discriminant.InnerValue}");
                }
            }
        }

        public static Transaction FromEnvelopeXdrV0(TransactionV0Envelope envelope)
        {
            var transactionXdr = envelope.Tx;
            var fee = transactionXdr.Fee.InnerValue;
            KeyPair sourceAccount = KeyPair.FromPublicKey(transactionXdr.SourceAccountEd25519.InnerValue);
            long sequenceNumber = transactionXdr.SeqNum.InnerValue.InnerValue;
            Memo memo = Memo.FromXdr(transactionXdr.Memo);
            TimeBounds timeBounds = TimeBounds.FromXdr(transactionXdr.TimeBounds);

            Operation[] operations = new Operation[transactionXdr.Operations.Length];
            for (int i = 0; i < transactionXdr.Operations.Length; i++)
            {
                operations[i] = Operation.FromXdr(transactionXdr.Operations[i]);
            }

            Transaction transaction = new Transaction(sourceAccount, fee, sequenceNumber, operations, memo, timeBounds);

            foreach (var signature in envelope.Signatures)
            {
                transaction.Signatures.Add(signature);
            }

            return transaction;
        }

        public static Transaction FromEnvelopeXdrV1(TransactionV1Envelope envelope)
        {
            var transactionXdr = envelope.Tx;
            var fee = transactionXdr.Fee.InnerValue;
            var sourceAccount = MuxedAccount.FromXdrMuxedAccount(transactionXdr.SourceAccount);
            long sequenceNumber = transactionXdr.SeqNum.InnerValue.InnerValue;
            Memo memo = Memo.FromXdr(transactionXdr.Memo);
            TimeBounds timeBounds = TimeBounds.FromXdr(transactionXdr.TimeBounds);

            Operation[] operations = new Operation[transactionXdr.Operations.Length];
            for (int i = 0; i < transactionXdr.Operations.Length; i++)
            {
                operations[i] = Operation.FromXdr(transactionXdr.Operations[i]);
            }

            Transaction transaction = new Transaction(sourceAccount, fee, sequenceNumber, operations, memo, timeBounds);

            foreach (var signature in envelope.Signatures)
            {
                transaction.Signatures.Add(signature);
            }

            return transaction;
        }

        [Obsolete("Use TransactionBuilder instead")]
        public class Builder : TransactionBuilder
        {
            public Builder(ITransactionBuilderAccount sourceAccount) : base(sourceAccount)
            {
            }
        }
   }
}