using System;
using xdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="ChangeTrustOp"/>.
    /// Use <see cref="Builder"/> to create a new ChangeTrustOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#change-trust">Change Trust</see>
    /// </summary>
    public class ChangeTrustOperation : Operation
    {
        private ChangeTrustOperation(ChangeTrustAsset asset, string limit)
        {
            Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            Limit = limit ?? throw new ArgumentNullException(nameof(limit), "limit cannot be null");
        }

        public Asset Asset { get; }

        public string Limit { get; }

        public const string MaxLimit = "922337203685.4775807";

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var op = new xdr.ChangeTrustOp();
            op.Line = Asset.ToChangeTrustAssetXDR();
            var limit = new xdr.Int64();
            limit.InnerValue = ToXdrAmount(Limit);
            op.Limit = limit;

            var body = new xdr.Operation.OperationBody();
            body.Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.CHANGE_TRUST);
            body.ChangeTrustOp = op;
            return body;
        }

        /// <summary>
        ///     Builds ChangeTrust operation.
        /// </summary>
        /// <see cref="ChangeTrustOperation" />
        public class Builder
        {
            private readonly ChangeTrustAsset _Asset;
            private readonly string _Limit;

            private KeyPair _SourceAccount;

            public Builder(xdr.ChangeTrustOp op)
            {
                _Asset = ChangeTrustAsset.FromXDR(op.Line);
                _Limit = FromXdrAmount(op.Limit.InnerValue);
            }

            /// <summary>
            ///     Creates a new ChangeTrust builder.
            /// </summary>
            /// <param name="asset">
            ///     The asset of the trustline. For example, if a gateway extends a trustline of up to 200 USD to a
            ///     user, the line is USD.
            /// </param>
            /// <param name="limit">
            ///     The limit of the trustline. For example, if a gateway extends a trustline of up to 200 USD to a
            ///     user, the limit is 200.
            /// </param>
            /// <exception cref="ArithmeticException">When limit has more than 7 decimal places.</exception>
            public Builder(ChangeTrustAsset asset, string limit)
            {
                _Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
                _Limit = limit ?? throw new ArgumentNullException(nameof(limit), "limit cannot be null");
            }

            /// <summary>
            ///     Creates a new ChangeTrust builder.
            /// </summary>
            /// <param name="asset">
            ///     The asset of the trustline. For example, if a gateway extends a trustline of up to 200 USD to a
            ///     user, the line is USD.
            /// </param>
            public Builder(ChangeTrustAsset asset)
            {
                _Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
                _Limit = MaxLimit;
            }

            /// <summary>
            ///     Set source account of this operation
            /// </summary>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                _SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public ChangeTrustOperation Build()
            {
                var operation = new ChangeTrustOperation(_Asset, _Limit);
                if (_SourceAccount != null)
                    operation.SourceAccount = _SourceAccount;
                return operation;
            }
        }
    }
}