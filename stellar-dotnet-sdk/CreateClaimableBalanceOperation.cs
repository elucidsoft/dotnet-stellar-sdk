using System.Linq;

namespace stellar_dotnet_sdk
{
    public class CreateClaimableBalanceOperation : Operation
    {
        public Asset Asset { get; }
        public string Amount { get; }
        public Claimant[] Claimants { get; }
        
        private CreateClaimableBalanceOperation(Asset asset, string amount, Claimant[] claimants)
        {
            Asset = asset;
            Amount = amount;
            Claimants = claimants;
        }
        
        public override xdr.Operation.OperationBody ToOperationBody()
        {
            return new xdr.Operation.OperationBody
            {
                Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.CREATE_CLAIMABLE_BALANCE),
                CreateClaimableBalanceOp = new xdr.CreateClaimableBalanceOp
                {
                    Amount = new xdr.Int64 {InnerValue = ToXdrAmount(Amount)},
                    Asset = Asset.ToXdr(),
                    Claimants = Claimants.Select(claimant => claimant.ToXdr()).ToArray(),
                }
            };
        }

        public class Builder
        {
            private Asset _asset;
            private string _amount;
            private Claimant[] _claimants;
            private KeyPair _sourceAccount;

            public Builder(xdr.CreateClaimableBalanceOp op)
            {
                _asset = Asset.FromXdr(op.Asset);
                _amount = FromXdrAmount(op.Amount.InnerValue);
                _claimants = op.Claimants.Select(Claimant.FromXdr).ToArray();
            }

            public Builder(Asset asset, string amount, Claimant[] claimants)
            {
                _asset = asset;
                _amount = amount;
                _claimants = claimants;
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
            public CreateClaimableBalanceOperation Build()
            {
                var operation = new CreateClaimableBalanceOperation(_asset, _amount, _claimants);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}