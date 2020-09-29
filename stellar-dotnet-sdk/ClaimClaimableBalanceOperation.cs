using System;

namespace stellar_dotnet_sdk
{
    public class ClaimClaimableBalanceOperation : Operation
    {
        public byte[] BalanceId { get; }

        public ClaimClaimableBalanceOperation(byte[] balanceId)
        {
            BalanceId = balanceId;
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.CLAIM_CLAIMABLE_BALANCE),
                ClaimClaimableBalanceOp = new xdr.ClaimClaimableBalanceOp
                {
                    BalanceID = new xdr.ClaimableBalanceID
                    {
                        Discriminant = new xdr.ClaimableBalanceIDType
                        {
                            InnerValue = xdr.ClaimableBalanceIDType.ClaimableBalanceIDTypeEnum.CLAIMABLE_BALANCE_ID_TYPE_V0,
                        },
                        V0 = new xdr.Hash(BalanceId)
                    }
                }
            };
        }

        public class Builder
        {
            private byte[] _balanceId;
            private KeyPair _sourceAccount;

            public Builder(xdr.ClaimClaimableBalanceOp op)
            {
                _balanceId = op.BalanceID.V0.InnerValue;
            }

            public Builder(byte[] balanceId)
            {
                if (balanceId.Length != 32)
                    throw new ArgumentException("Must be 32 bytes long", nameof(balanceId));
                _balanceId = balanceId;
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