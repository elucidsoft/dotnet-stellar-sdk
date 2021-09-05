using System.Linq;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="SetTrustlineFlagsOperation"/>.
    /// Use <see cref="Builder"/> to create a new SetTrustlineFlagsOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html">Set Trustline Flags</see>
    /// </summary>
    public class SetTrustlineFlagsOperation : Operation
    {
        /// <summary>
        /// The asset to use in the operation.
        /// </summary>
        public Asset Asset { get; }

        /// <summary>
        /// Account whose trustline is affected by this operation.
        /// </summary>
        public KeyPair Trustor { get; }

        public uint? SetFlags { get; }

        public uint? ClearFlags { get; }


        private SetTrustlineFlagsOperation(Asset asset, KeyPair trustor, uint? setFlags, uint? clearFlags)
        {
            Asset = asset;
            Trustor = trustor;
            SetFlags = setFlags;
            ClearFlags = clearFlags;
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.SET_TRUST_LINE_FLAGS),
                SetTrustLineFlagsOp = new xdr.SetTrustLineFlagsOp
                {
                    Asset = Asset.ToXdr(),
                    Trustor = new xdr.AccountID(Trustor.XdrPublicKey),
                    SetFlags = new xdr.Uint32() { InnerValue = (int)SetFlags.Value },
                    ClearFlags = new xdr.Uint32() { InnerValue = (int)ClearFlags.Value }
                }
            };
        }

        /// <summary>
        /// Builds SetTrustlineFlagsOperation operation.
        /// </summary>
        /// <see cref="SetTrustlineFlagsOperation" />
        public class Builder
        {
            private Asset _asset;
            private KeyPair _trustor;
            private uint? _setFlags;
            private uint? _clearFlags;

            private KeyPair _sourceAccount;

            public Builder(xdr.SetTrustLineFlagsOp op)
            {
                _asset = Asset.FromXdr(op.Asset);
                _trustor = KeyPair.FromXdrPublicKey(op.Trustor.InnerValue);
                _setFlags = (uint?)op.SetFlags.InnerValue;
                _clearFlags = (uint?)op.ClearFlags.InnerValue;
            }

            public Builder(Asset asset, KeyPair trustor, uint? setFlags, uint? clearFlags)
            {
                _asset = asset;
                _trustor = trustor;
                _setFlags = setFlags;
                _clearFlags = clearFlags;
            }

            /// <summary>
            /// Sets the source account for this operation.
            /// </summary>
            /// <param name="account">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair account)
            {
                _sourceAccount = account;
                return this;
            }

            /// <summary>
            /// Builds an operation
            /// </summary>
            public SetTrustlineFlagsOperation Build()
            {
                var operation = new SetTrustlineFlagsOperation(_asset, _trustor, _setFlags, _clearFlags);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}