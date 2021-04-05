using System.Linq;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="ClawbackClaimableBalanceOperation"/>.
    /// Use <see cref="Builder"/> to create a new ClawbackClaimableBalanceOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html">Clawback Claimable Balance</see>
    /// </summary>
    public class ClawbackClaimableBalanceOperation : Operation
    {
        public string BalanceID { get; }

        private ClawbackClaimableBalanceOperation(string balanceID)
        {
            BalanceID = balanceID;
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var claimableBalanceID = new xdr.ClaimableBalanceID();
            claimableBalanceID.V0 = new xdr.Hash(System.Convert.FromBase64String(BalanceID));

            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.CLAWBACK_CLAIMABLE_BALANCE),
                ClawbackClaimableBalanceOp = new xdr.ClawbackClaimableBalanceOp()
                {
                    BalanceID = claimableBalanceID
                }
            };
        }

        /// <summary>
        ///     Builds ClawbackClaimableBalanceOperation operation.
        /// </summary>
        /// <see cref="ClawbackClaimableBalanceOperation" />
        public class Builder
        {
            private string _balanceID;

            private KeyPair _sourceAccount;

            public Builder(xdr.ClawbackClaimableBalanceOp op)
            {
                _balanceID = System.Convert.ToString(op.BalanceID.V0.InnerValue);
            }

            public Builder(string balanceID)
            {
                _balanceID = balanceID;
            }

            /// <summary>
            ///     Sets the source account for this operation.
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
            public ClawbackClaimableBalanceOperation Build()
            {
                var operation = new ClawbackClaimableBalanceOperation(_balanceID);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}