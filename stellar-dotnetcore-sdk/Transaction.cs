using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class Transaction
    {
        private readonly int BASE_FEE = 100;

        private Transaction(KeyPair sourceAccount, long sequenceNumber, Operation[] operations, Memo memo, TimeBounds timeBounds)
        {
            SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
            SequenceNumber = sequenceNumber;
            Operations = operations ?? throw new ArgumentNullException(nameof(operations), "operations cannot be null");
            if (operations.Length == 0)
                throw new ArgumentException(nameof(operations), "At least one operation required");

            Fee = operations.Length * BASE_FEE;
            Signatures = new List<DecoratedSignature>();
            Memo = memo != null ? memo : Memo.None();
            TimeBounds = timeBounds;
        }

        public int Fee { get; }

        public KeyPair SourceAccount { get; }

        public long SequenceNumber { get; }

        public Operation[] Operations { get; }

        public Memo Memo { get; }

        public TimeBounds TimeBounds { get; }

        public List<DecoratedSignature> Signatures { get; }

        /**
         * Adds a new signature ed25519PublicKey to this transaction.
         * @param signer {@link KeyPair} object representing a signer
         */
        public void Sign(KeyPair signer)
        {
            if (signer == null)
                throw new ArgumentNullException(nameof(signer), "signer cannot be null");

            var txHash = Hash();
            Signatures.Add(signer.SignDecorated(txHash));
        }

        /**
         * Adds a new sha256Hash signature to this transaction by revealing preimage.
         * @param preimage the sha256 hash of preimage should be equal to signer hash
         */
        public void Sign(byte[] preimage)
        {
            var signature = new Signature();
            signature.InnerValue = preimage ?? throw new ArgumentNullException(nameof(preimage), "preimage cannot be null");

            var hash = Util.Hash(preimage);

            var length = hash.Length;
            var signatureHintBytes = hash.Skip(length - 4).Take(4).ToArray();

            var signatureHint = new SignatureHint();
            signatureHint.InnerValue = signatureHintBytes;

            var decoratedSignature = new DecoratedSignature();
            decoratedSignature.Hint = signatureHint;
            decoratedSignature.Signature = signature;

            Signatures.Add(decoratedSignature);
        }

        /**
         * Returns transaction hash.
         */
        public byte[] Hash()
        {
            return Util.Hash(SignatureBase());
        }

        /**
         * Returns signature base.
         */
        public byte[] SignatureBase()
        {
            if (Network.Current == null)
                throw new NoNetworkSelectedException();

            var writer = new XdrDataOutputStream();

            // Hashed NetworkID
            writer.Write(Network.Current.NetworkId);

            // Envelope Type - 4 bytes
            EnvelopeType.Encode(writer, EnvelopeType.Create(EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX));

            // Transaction XDR bytes
            var txWriter = new XdrDataOutputStream();
            xdr.Transaction.Encode(txWriter, ToXdr());

            writer.Write(txWriter.ToArray());

            return writer.ToArray();
        }

        /**
         * Generates Transaction XDR object.
         */
        public xdr.Transaction ToXdr()
        {
            // fee
            var fee = new Uint32();
            fee.InnerValue = Fee;

            // sequenceNumber
            var sequenceNumberUint = new Uint64();
            sequenceNumberUint.InnerValue = SequenceNumber;
            var sequenceNumber = new SequenceNumber();
            sequenceNumber.InnerValue = sequenceNumberUint;

            // sourceAccount
            var sourceAccount = new AccountID();
            sourceAccount.InnerValue = SourceAccount.XdrPublicKey;

            // operations
            var operations = new xdr.Operation[Operations.Length];

            for (var i = 0; i < Operations.Length; i++)
                operations[i] = Operations[i].ToXdr();

            // ext
            var ext = new xdr.Transaction.TransactionExt();
            ext.Discriminant = 0;

            var transaction = new xdr.Transaction();
            transaction.Fee = fee;
            transaction.SeqNum = sequenceNumber;
            transaction.SourceAccount = sourceAccount;
            transaction.Operations = operations;
            transaction.Memo = Memo.ToXdr();
            transaction.TimeBounds = TimeBounds == null ? null : TimeBounds.ToXdr();
            transaction.Ext = ext;
            return transaction;
        }

        /**
         * Generates TransactionEnvelope XDR object. Transaction need to have at least one signature.
         */
        public TransactionEnvelope ToEnvelopeXdr()
        {
            if (Signatures.Count == 0)
                throw new NotEnoughSignaturesException("Transaction must be signed by at least one signer. Use transaction.sign().");

            var thisXdr = new TransactionEnvelope();
            var transaction = ToXdr();
            thisXdr.Tx = transaction;

            var signatures = new DecoratedSignature[Signatures.Count];
            signatures = Signatures.ToArray();
            thisXdr.Signatures = signatures;
            return thisXdr;
        }

        /**
         * Returns base64-encoded TransactionEnvelope XDR object. Transaction need to have at least one signature.
         */
        public string ToEnvelopeXdrBase64()
        {
            var envelope = ToEnvelopeXdr();
            var writer = new XdrDataOutputStream();
            TransactionEnvelope.Encode(writer, envelope);

            return Convert.ToBase64String(writer.ToArray());
        }

        /**
         * Builds a new Transaction object.
         */
        public class Builder
        {
            private readonly ITransactionBuilderAccount mSourceAccount;
            private Memo mMemo;
            private readonly BlockingCollection<Operation> mOperations;
            private TimeBounds mTimeBounds;

            /**
             * Construct a new transaction builder.
             * @param sourceAccount The source account for this transaction. This account is the account
             * who will use a sequence number. When build() is called, the account object's sequence number
             * will be incremented.
             */
            public Builder(ITransactionBuilderAccount sourceAccount)
            {
                if (sourceAccount == null)
                    throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");

                mSourceAccount = sourceAccount;
                mOperations = new BlockingCollection<Operation>();
            }

            public int getOperationsCount()
            {
                return mOperations.Count;
            }

            /**
             * Adds a new <a href="https://www.stellar.org/developers/learn/concepts/list-of-operations.html" target="_blank">operation</a> to this transaction.
             * @param operation
             * @return Builder object so you can chain methods.
             * @see Operation
             */
            public Builder AddOperation(Operation operation)
            {
                if (operation == null)
                    throw new ArgumentNullException(nameof(operation), "operation cannot be null");

                mOperations.Add(operation);
                return this;
            }

            /**
             * Adds a <a href="https://www.stellar.org/developers/learn/concepts/transactions.html" target="_blank">memo</a> to this transaction.
             * @param memo
             * @return Builder object so you can chain methods.
             * @see Memo
             */
            public Builder AddMemo(Memo memo)
            {
                if (mMemo != null)
                    throw new ArgumentException(nameof(memo), "Memo has been already added.");

                mMemo = memo ?? throw new ArgumentNullException(nameof(memo), "memo cannot be null");

                return this;
            }

            /**
             * Adds a <a href="https://www.stellar.org/developers/learn/concepts/transactions.html" target="_blank">time-bounds</a> to this transaction.
             * @param timeBounds
             * @return Builder object so you can chain methods.
             * @see TimeBounds
             */
            public Builder AddTimeBounds(TimeBounds timeBounds)
            {
                if (mTimeBounds != null)
                    throw new ArgumentException(nameof(timeBounds), "TimeBounds has been already added.");

                mTimeBounds = timeBounds ?? throw new ArgumentNullException(nameof(timeBounds), "timeBounds cannot be null");

                return this;
            }

            /**
             * Builds a transaction. It will increment sequence number of the source account.
             */
            public Transaction Build()
            {
                var operations = mOperations.ToArray();

                var transaction = new Transaction(mSourceAccount.KeyPair, mSourceAccount.GetIncrementedSequenceNumber(), operations, mMemo, mTimeBounds);
                // Increment sequence number when there were no exceptions when creating a transaction
                mSourceAccount.IncrementSequenceNumber();
                return transaction;
            }
        }
    }
}