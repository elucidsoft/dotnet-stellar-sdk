using System;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Use <see cref="Builder"/> to create a new InflationOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#inflation">Inflation</see>
    /// </summary>
    public class InflationOperation : Operation
    {
        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var body = new xdr.Operation.OperationBody
            {
                Discriminant = OperationType.Create(OperationType.OperationTypeEnum.INFLATION)
            };

            return body;
        }

        public class Builder
        {
            private IAccountId mSourceAccount;

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(IAccountId sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public InflationOperation Build()
            {
                var operation = new InflationOperation();
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}