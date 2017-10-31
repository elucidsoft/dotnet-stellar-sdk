using stellar_dotnetcore_sdk.xdr;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class Transaction
    {
        private readonly int BASE_FEE = 100;

        private readonly int _Fee;
        private readonly KeyPair _SourceAccount;
        private readonly long _SequenceNumber;
        private readonly Operation[] _Operations;
        private readonly Memo _Memo;
        private readonly TimeBounds _TimeBounds;
        private List<DecoratedSignature> _Signatures;

        public int Fee { get { return _Fee; } }
        public KeyPair SourceAccount { get { return _SourceAccount; } }
        public long SequenceNumber { get { return _SequenceNumber; } }
        public Operation[] Operations { get { return _Operations; } }
        public Memo Memo { get { return _Memo; } }
        public TimeBounds TimeBounds { get { return _TimeBounds; } }
        public List<DecoratedSignature> Signatures { get { return _Signatures; } }

        Transaction(KeyPair sourceAccount, long sequenceNumber, Operation[] operations, Memo memo, TimeBounds timeBounds)
        {
            _SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
            _SequenceNumber = sequenceNumber;
            _Operations = operations ?? throw new ArgumentNullException(nameof(operations), "operations cannot be null");
            if (operations.Length == 0)
                throw new ArgumentException(nameof(operations), "At least one operation required");

            _Fee = operations.Length * BASE_FEE;
            _Signatures = new List<xdr.DecoratedSignature>();
            _Memo = memo != null ? memo : Memo.None();
            _TimeBounds = timeBounds;
        }

        /**
         * Adds a new signature ed25519PublicKey to this transaction.
         * @param signer {@link KeyPair} object representing a signer
         */
        public void Sign(KeyPair signer)
        {
            if (signer == null)
                throw new ArgumentNullException(nameof(signer), "signer cannot be null");

            byte[] txHash = Hash();
            _Signatures.Add(signer.SignDecorated(txHash));
        }

        /**
         * Adds a new sha256Hash signature to this transaction by revealing preimage.
         * @param preimage the sha256 hash of preimage should be equal to signer hash
         */
        public void Sign(byte[] preimage)
        {
            Signature signature = new Signature();
            signature.InnerValue = preimage ?? throw new ArgumentNullException(nameof(preimage), "preimage cannot be null");

            byte[] hash = Util.Hash(preimage);

            var length = hash.Length;
            var signatureHintBytes = hash.Skip(length - 4).Take(4).ToArray();

            SignatureHint signatureHint = new SignatureHint();
            signatureHint.InnerValue = signatureHintBytes;

            DecoratedSignature decoratedSignature = new DecoratedSignature();
            decoratedSignature.Hint = signatureHint;
            decoratedSignature.Signature = signature;

            _Signatures.Add(decoratedSignature);
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
            {
                throw new NoNetworkSelectedException();
            }

            var memoryStream = new MemoryStream();
            var writer = new BinaryWriter(memoryStream);

            // Hashed NetworkID
            writer.Write(Network.Current.NetworkId);

            // Envelope Type - 4 bytes
            writer.Write((int)EnvelopeType.Create(EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX).InnerValue);

            // Transaction XDR bytes
            var txMemoryStream = new MemoryStream();
            var txWriter = new XdrDataOutputStream(txMemoryStream);
            xdr.Transaction.Encode(txWriter, ToXdr());
            writer.Write(txMemoryStream.ToArray());

            return memoryStream.ToArray();

        }

        /**
         * Generates Transaction XDR object.
         */
        public xdr.Transaction ToXdr()
        {
            // fee
            Uint32 fee = new Uint32();
            fee.InnerValue = _Fee;

            // sequenceNumber
            Uint64 sequenceNumberUint = new Uint64();
            sequenceNumberUint.InnerValue = _SequenceNumber;
            SequenceNumber sequenceNumber = new SequenceNumber();
            sequenceNumber.InnerValue = sequenceNumberUint;

            // sourceAccount
            AccountID sourceAccount = new AccountID();
            sourceAccount.InnerValue = _SourceAccount.XdrPublicKey;

            // operations
            xdr.Operation[] operations = new xdr.Operation[_Operations.Length];

            for (int i = 0; i < _Operations.Length; i++)
            {
                operations[i] = _Operations[i].ToXdr();
            }

            // ext
            xdr.Transaction.TransactionExt ext = new xdr.Transaction.TransactionExt();
            ext.Discriminant = 0;

            xdr.Transaction transaction = new xdr.Transaction();
            transaction.Fee = fee;
            transaction.SeqNum = sequenceNumber;
            transaction.SourceAccount = sourceAccount;
            transaction.Operations = operations;
            transaction.Memo = _Memo.ToXdr();
            transaction.TimeBounds = _TimeBounds == null ? null : _TimeBounds.ToXdr();
            transaction.Ext = ext;
            return transaction;
        }

        /**
         * Generates TransactionEnvelope XDR object. Transaction need to have at least one signature.
         */
        public xdr.TransactionEnvelope ToEnvelopeXdr()
        {
            if (_Signatures.Count == 0)
            {
                throw new NotEnoughSignaturesException("Transaction must be signed by at least one signer. Use transaction.sign().");
            }

            TransactionEnvelope thisXdr = new TransactionEnvelope();
            xdr.Transaction transaction = ToXdr();
            thisXdr.Tx = transaction;

            DecoratedSignature[] signatures = new DecoratedSignature[_Signatures.Count];
            signatures = _Signatures.ToArray();
            thisXdr.Signatures = signatures;
            return thisXdr;
        }

        /**
         * Returns base64-encoded TransactionEnvelope XDR object. Transaction need to have at least one signature.
         */
        public String ToEnvelopeXdrBase64()
        {
            TransactionEnvelope envelope = ToEnvelopeXdr();
            var memoryStream = new MemoryStream();
            var writer = new XdrDataOutputStream(memoryStream);
            TransactionEnvelope.Encode(writer, envelope);

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        /**
         * Builds a new Transaction object.
         */
        public class Builder
        {
            private readonly ITransactionBuilderAccount mSourceAccount;
            private Memo mMemo;
            private TimeBounds mTimeBounds;
            BlockingCollection<Operation> mOperations;

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
                {
                    throw new ArgumentNullException(nameof(operation), "operation cannot be null");
                }

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
                {
                    throw new ArgumentException(nameof(memo), "Memo has been already added.");
                }

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
                {
                    throw new ArgumentException(nameof(timeBounds), "TimeBounds has been already added.");
                }

                mTimeBounds = timeBounds ?? throw new ArgumentNullException(nameof(timeBounds), "timeBounds cannot be null");

                return this;
            }

            /**
             * Builds a transaction. It will increment sequence number of the source account.
             */
            public Transaction Build()
            {
                Operation[] operations = mOperations.ToArray();

                Transaction transaction = new Transaction(mSourceAccount.KeyPair, mSourceAccount.GetIncrementedSequenceNumber(), operations, mMemo, mTimeBounds);
                // Increment sequence number when there were no exceptions when creating a transaction
                mSourceAccount.IncrementSequenceNumber();
                return transaction;
            }
        }
    }
}
