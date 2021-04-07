using System.Linq;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="CreateClaimableBalanceOperation"/>.
    /// Use <see cref="Builder"/> to create a new ClawbackOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html">Clawback</see>
    /// </summary>
    public class ClawbackOperation : Operation
    {
        /// <summary>
        /// The asset to claw.
        /// </summary>
        public Asset Asset { get; }

        /// <summary>
        /// Amount to claw back.
        /// </summary>
        public string Amount { get; }

        /// <summary>
        /// The account to claw back the amount from.
        /// </summary>
        public IAccountId From { get; }


        private ClawbackOperation(Asset asset, string amount, IAccountId from)
        {
            Asset = asset;
            Amount = amount;
            From = from;
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.CLAWBACK),
                ClawbackOp = new xdr.ClawbackOp
                {
                    Amount = new xdr.Int64 { InnerValue = ToXdrAmount(Amount) },
                    Asset = Asset.ToXdr(),
                    From = From.MuxedAccount
                }
            };
        }

        /// <summary>
        /// Builds Clawback operation.
        /// </summary>
        /// <see cref="ClawbackOperation" />
        public class Builder
        {
            private Asset _asset;
            private string _amount;
            private IAccountId _from;

            private KeyPair _sourceAccount;

            public Builder(xdr.ClawbackOp op)
            {
                _asset = Asset.FromXdr(op.Asset);
                _amount = FromXdrAmount(op.Amount.InnerValue);
                _from = MuxedAccount.FromXdrMuxedAccount(op.From);
            }

            public Builder(Asset asset, string amount, IAccountId from)
            {
                _asset = asset;
                _amount = amount;
                _from = from;
            }

            /// <summary>
            ///  Sets the source account for this operation.
            /// </summary>
            /// <param name="account">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair account)
            {
                _sourceAccount = account;
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public ClawbackOperation Build()
            {
                var operation = new ClawbackOperation(_asset, _amount, _from);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}