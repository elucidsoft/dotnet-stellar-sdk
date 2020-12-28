using System;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="ClaimClaimableBalanceOperation"/>.
    /// Use <see cref="Builder"/> to create a new ClaimClaimableBalanceOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html">Claim Claimable Balance</see>
    /// </summary> 
    public class ClaimClaimableBalanceOperation : Operation
    {
        /// <summary>
        ///     The ID of the ClaimableBalanceEntry being claimed.
        /// </summary>
        public byte[] BalanceId { get; }

        public ClaimClaimableBalanceOperation(byte[] balanceId)
        {
            // Backwards compat - was previously expecting no type to be set, set CLAIMABLE_BALANCE_ID_TYPE_V0 to match previous behaviour.
            if (balanceId.Length == 32)
            {
                var expanded = new byte[36];
                Array.Copy(balanceId, 0, expanded, 4, 32);
                balanceId = expanded;
            } 

            if (balanceId.Length != 36)
            {
                throw new ArgumentException("Must be 36 bytes long", nameof(balanceId));
            }

            BalanceId = balanceId;
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE),
                ClaimClaimableBalanceOp = new xdr.ClaimClaimableBalanceOp
                {
                    BalanceID = xdr.ClaimableBalanceID.Decode(new xdr.XdrDataInputStream(BalanceId))
                }
            };
        }

        /// <summary>
        ///     Builds ClaimClaimableBalanceOperation operation.
        /// </summary>
        /// <see cref="ClaimClaimableBalanceOperation" />
        public class Builder
        {
            private byte[] _balanceId;
            private KeyPair _sourceAccount;

            public Builder(xdr.ClaimClaimableBalanceOp op)
            {
                _balanceId = op.BalanceID.V0.InnerValue;
            }

            /// <summary>
            ///     Creates a new ClaimClaimableBalanceOperation builder.
            /// </summary>
            /// <param name="balanceId">The ID of the ClaimableBalanceEntry being claimed.</param>
            /// <exception cref="ArgumentException">when balance id is not 36 bytes.</exception>
            public Builder(byte[] balanceId)
            {
                if (balanceId.Length != 36 && balanceId.Length != 32)
                    // Don't mention the 32 bytes, it's for backwards compat and shouldn't direct towards using it.
                    throw new ArgumentException("Must be 36 bytes long", nameof(balanceId));
                _balanceId = balanceId;
            }

            /// <summary>
            ///     Creates a new ClaimClaimableBalanceOperation builder.
            /// </summary>
            /// <param name="balanceId">The ID of the ClaimableBalanceEntry being claimed.</param>
            /// <exception cref="ArgumentException">when balance id is not a hex string representing 36 bytes.</exception>
            public Builder(String balanceId)
            {
                if (balanceId.Length != 72)
                    throw new ArgumentException("Must be a hex string of 72 characters", nameof(balanceId));
                _balanceId = Util.HexToBytes(balanceId);
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
            public ClaimClaimableBalanceOperation Build()
            {
                var operation = new ClaimClaimableBalanceOperation(_balanceId);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}