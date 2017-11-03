using System;
using stellar_dotnetcore_sdk.xdr;
using Int64 = stellar_dotnetcore_sdk.xdr.Int64;

namespace stellar_dotnetcore_sdk
{
    public class CreateAccountOperation : Operation
    {
        public CreateAccountOperation(KeyPair destination, string startingBalance)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            StartingBalance = startingBalance ?? throw new ArgumentNullException(nameof(startingBalance), "startingBalance cannot be null");
        }

        public KeyPair Destination { get; }

        public string StartingBalance { get; }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var op = new CreateAccountOp();
            var destination = new AccountID();
            destination.InnerValue = Destination.XdrPublicKey;
            op.Destination = destination;
            var startingBalance = new Int64();
            startingBalance.InnerValue = ToXdrAmount(StartingBalance);
            op.StartingBalance = startingBalance;

            var body = new xdr.Operation.OperationBody();
            body.Discriminant = OperationType.Create(OperationType.OperationTypeEnum.CREATE_ACCOUNT);
            body.CreateAccountOp = op;
            return body;
        }

        public class Builder
        {
            private readonly KeyPair destination;
            private readonly string startingBalance;

            private KeyPair _SourceAccount;

            public Builder(CreateAccountOp createAccountOp)
            {
                destination = KeyPair.FromXdrPublicKey(createAccountOp.Destination.InnerValue);
                startingBalance = FromXdrAmount(createAccountOp.StartingBalance.InnerValue);
            }

            public Builder(KeyPair destination, string startingBalance)
            {
                this.destination = destination;
                this.startingBalance = startingBalance;
            }

            public Builder SetSourceAccount(KeyPair account)
            {
                _SourceAccount = account;
                return this;
            }

            public CreateAccountOperation Build()
            {
                var operation = new CreateAccountOperation(destination, startingBalance);
                if (_SourceAccount != null)
                    operation.SourceAccount = _SourceAccount;
                return operation;
            }
        }
    }
}