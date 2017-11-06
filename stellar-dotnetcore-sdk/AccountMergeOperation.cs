using System;
using stellar_dotnetcore_sdk.responses.operations;
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
        ///     The account that receives the remaining XLM balance of the source account.
        /// </summary>
        public KeyPair Destination { get; }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var body = new xdr.Operation.OperationBody();
            var destination = new AccountID {InnerValue = Destination.XdrPublicKey};
            body.Destination = destination;
            body.Discriminant = OperationType.Create(OperationType.OperationTypeEnum.ACCOUNT_MERGE);
            return body;
        }

        /// <summary>
        ///     Builds AccountMerge operation.
        ///     <see cref="AccountMergeOperation" />
        /// </summary>
        public class Builder
        {
            private readonly KeyPair _destination;

            public Builder(xdr.Operation.OperationBody op)
            {
                _destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
            }

            /// <summary>
            ///     Creates a new AccountMerge builder.
            /// </summary>
            /// <param name="destination">destination The account that receives the remaining XLM balance of the source account.</param>
            public Builder(KeyPair destination)
            {
                _destination = destination;
            }

            /// <summary>
            ///     Set source account of this operation
            /// </summary>
            public KeyPair SourceAccount { get; private set; }

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            /// <returns></returns>
            public AccountMergeOperation Build()
            {
                var operation = new AccountMergeOperation(_destination);
                if (SourceAccount != null)
                    operation.SourceAccount = SourceAccount;
                return operation;
            }
        }
    }
}