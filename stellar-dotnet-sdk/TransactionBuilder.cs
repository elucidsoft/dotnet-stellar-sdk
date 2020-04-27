using System;
using System.Collections.Concurrent;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class TransactionBuilder
    {
        private const uint BaseFee = 100;

        private readonly BlockingCollection<Operation> _operations;
        private readonly ITransactionBuilderAccount _sourceAccount;
        private Memo _memo;
        private TimeBounds _timeBounds;
        private uint _fee;

        /// <summary>
        ///     Construct a new transaction builder.
        /// </summary>
        /// <param name="sourceAccount">
        ///     The source account for this transaction. This account is the account
        ///     who will use a sequence number. When build() is called, the account object's sequence number will be incremented.
        /// </param>
        public TransactionBuilder(ITransactionBuilderAccount sourceAccount)
        {
            _sourceAccount = sourceAccount ??
                             throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
            _operations = new BlockingCollection<Operation>();
            _fee = BaseFee;
        }

        public int OperationsCount => _operations.Count;

        /// <summary>
        ///     Adds a new operation to this transaction.
        ///     See: https://www.stellar.org/developers/learn/concepts/list-of-operations.html
        /// </summary>
        /// <param name="operation">operation</param>
        /// <returns>Builder object so you can chain methods.</returns>
        public TransactionBuilder AddOperation(Operation operation)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation), "operation cannot be null");

            _operations.Add(operation);
            return this;
        }

        /// <summary>
        ///     Adds a memo to this transaction.
        ///     See: https://www.stellar.org/developers/learn/concepts/transactions.html
        /// </summary>
        /// <param name="memo">Memo</param>
        /// <returns>Builder object so you can chain methods.</returns>
        public TransactionBuilder AddMemo(Memo memo)
        {
            if (_memo != null)
                throw new ArgumentException("Memo has been already added.", nameof(memo));

            _memo = memo ?? throw new ArgumentNullException(nameof(memo), "memo cannot be null");

            return this;
        }

        /// <summary>
        ///     Adds a time-bounds to this transaction.
        ///     See: https://www.stellar.org/developers/learn/concepts/transactions.html
        /// </summary>
        /// <param name="timeBounds">timeBounds</param>
        /// <returns>Builder object so you can chain methods.</returns>
        public TransactionBuilder AddTimeBounds(TimeBounds timeBounds)
        {
            if (_timeBounds != null)
                throw new ArgumentException("TimeBounds has been already added.", nameof(timeBounds));

            _timeBounds = timeBounds ??
                          throw new ArgumentNullException(nameof(timeBounds), "timeBounds cannot be null");

            return this;
        }

        /// <summary>
        ///     Set the transaction fee (in Stroops) per operation.
        ///     See: https://www.stellar.org/developers/learn/concepts/transactions.html
        /// </summary>
        /// <param name="fee">fee (in Stroops) for each operation in the transaction</param>
        /// <returns>Builder object so you can chain methods.</returns>
        public TransactionBuilder SetFee(uint fee)
        {
            if (_fee <= 0)
                throw new ArgumentException("Fee must be a positive amount", nameof(fee));

            _fee = fee;

            return this;
        }

        /// <summary>
        ///     Builds a transaction. It will increment sequence number of the source account.
        /// </summary>
        /// <returns></returns>
        public Transaction Build()
        {
            var operations = _operations.ToArray();

            var totalFee = operations.Length * _fee;
            if (totalFee > UInt32.MaxValue) throw new InvalidOperationException("Transaction fee overflow");

            var transaction = new Transaction(_sourceAccount.KeyPair, (uint) totalFee,
                _sourceAccount.IncrementedSequenceNumber, operations, _memo, _timeBounds);
            // Increment sequence number when there were no exceptions when creating a transaction
            _sourceAccount.IncrementSequenceNumber();
            return transaction;
        }

        public static Transaction FromEnvelopeXdr(string envelope)
        {
            byte[] bytes = Convert.FromBase64String(envelope);

            TransactionEnvelope transactionEnvelope = TransactionEnvelope.Decode(new XdrDataInputStream(bytes));
            return FromEnvelopeXdr(transactionEnvelope);
        }

        public static Transaction FromEnvelopeXdr(TransactionEnvelope envelope)
        {
            switch (envelope.Discriminant.InnerValue)
            {
                case EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX_V0:
                    return Transaction.FromEnvelopeXdr(envelope);
                case EnvelopeType.EnvelopeTypeEnum.ENVELOPE_TYPE_TX:
                    return Transaction.FromEnvelopeXdr(envelope);
                default:
                    throw new ArgumentException($"Unknown envelope type {envelope.Discriminant.InnerValue}");
            }
        }

    }
}