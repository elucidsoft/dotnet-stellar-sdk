using System;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class ChangeTrustOperation : Operation
    {
        private readonly Asset _Asset;
        private readonly String _Limit;

        public Asset Asset => _Asset;
        public string Limit => _Limit;

        private ChangeTrustOperation(Asset asset, String limit)
        {
            this._Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            this._Limit = limit ?? throw new ArgumentNullException(nameof(limit), "limit cannot be null");
        }


        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            sdkxdr.ChangeTrustOp op = new sdkxdr.ChangeTrustOp();
            op.Line = Asset.ToXdr();
            sdkxdr.Int64 limit = new sdkxdr.Int64();
            limit.InnerValue = Operation.ToXdrAmount(this.Limit);
            op.Limit = limit;

            sdkxdr.Operation.OperationBody body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.CHANGE_TRUST);
            body.ChangeTrustOp = op;
            return body;
        }

        /// <summary>
        /// Builds ChangeTrust operation.
        /// </summary>
        /// <see cref="ChangeTrustOperation"/>
        public class Builder
        {
            private readonly Asset _Asset;
            private readonly String _Limit;

            private KeyPair _SourceAccount;

            public Builder(sdkxdr.ChangeTrustOp op)
            {
                _Asset = Asset.FromXdr(op.Line);
                _Limit = Operation.FromXdrAmount(op.Limit.InnerValue);
            }

            /// <summary>
            /// Creates a new ChangeTrust builder.
            /// </summary>
            /// <param name="asset">The asset of the trustline. For example, if a gateway extends a trustline of up to 200 USD to a user, the line is USD.</param> 
            /// <param name="limit">The limit of the trustline. For example, if a gateway extends a trustline of up to 200 USD to a user, the limit is 200.</param> 
            /// <exception cref="ArithmeticException">When limit has more than 7 decimal places.</exception>
            public Builder(Asset asset, String limit)
            {
                this._Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
                this._Limit = limit ?? throw new ArgumentNullException(nameof(limit), "limit cannot be null");
            }

            /// <summary>
            /// Set source account of this operation
            /// </summary>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                _SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            /// Builds an operation
            /// </summary>
            public ChangeTrustOperation Build()
            {
                ChangeTrustOperation operation = new ChangeTrustOperation(_Asset, _Limit);
                if (_SourceAccount != null)
                {
                    operation.SourceAccount = _SourceAccount;
                }
                return operation;
            }
        }
    }
}