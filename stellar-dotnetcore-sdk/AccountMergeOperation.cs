using System;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AccountMergeOperation : Operation
    {
        private AccountMergeOperation(KeyPair destination)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
        }

        /// <summary>
        /// The account that receives the remaining XLM balance of the source account.
        /// </summary>
        public KeyPair Destination { get; }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var body = new xdr.Operation.OperationBody();
            var destination = new AccountID { InnerValue = Destination.XdrPublicKey };
            body.Destination = destination;
            body.Discriminant.InnerValue = OperationType.OperationTypeEnum.ACCOUNT_MERGE;
            return body;
        }

        /// <summary>
        /// Builds AccountMerge operation.
        /// <see cref="AccountMergeOperation"/>
        /// </summary>
        public class Builder
        {
            private readonly KeyPair _destination;
            private KeyPair _mSourceAccount;

            public Builder(xdr.Operation.OperationBody op)
            {
                _destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
            }

            /// <summary>
            /// Creates a new AccountMerge builder.
            /// </summary>
            /// <param name="destination">destination The account that receives the remaining XLM balance of the source account.</param>
            public Builder(KeyPair destination)
            {
                _destination = destination;
            }

            /// <summary>
            /// Set source account of this operation
            /// </summary>
            public KeyPair SourceAccount
            {
                get => _mSourceAccount;
                set => _mSourceAccount = value ?? throw new ArgumentNullException(nameof(value), "keypair cannot be null");
            }

            /// <summary>
            ///  Builds an operation
            /// </summary>
            /// <returns></returns>
            public AccountMergeOperation Build()
            {
                var operation = new AccountMergeOperation(_destination);
                if (_mSourceAccount != null)
                    operation.SourceAccount = _mSourceAccount;
                return operation;
            }
        }
    }
}