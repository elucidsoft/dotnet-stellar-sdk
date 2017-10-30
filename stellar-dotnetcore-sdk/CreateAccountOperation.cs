using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class CreateAccountOperation : Operation
    {
        private readonly KeyPair _Destination;
        private readonly string _StartingBalance;

        public KeyPair Destination { get { return _Destination; } }
        public string StartingBalance { get { return _StartingBalance; } }

        public CreateAccountOperation(KeyPair destination, String startingBalance)
        {
            this._Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            this._StartingBalance = startingBalance ?? throw new ArgumentNullException(nameof(startingBalance), "startingBalance cannot be null");
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            CreateAccountOp op = new CreateAccountOp();
            AccountID destination = new AccountID();
            destination.InnerValue = Destination.XdrPublicKey;
            op.Destination = destination;
            xdr.Int64 startingBalance = new xdr.Int64();
            startingBalance.InnerValue = Operation.ToXdrAmount(StartingBalance);
            op.StartingBalance = startingBalance;

            xdr.Operation.OperationBody body = new xdr.Operation.OperationBody();
            body.Discriminant = OperationType.Create(OperationType.OperationTypeEnum.CREATE_ACCOUNT);
            body.CreateAccountOp = op;
            return body;
        }

        public class Builder
        {
            private readonly KeyPair destination;
            private readonly String startingBalance;

            private KeyPair _SourceAccount;

            public Builder(CreateAccountOp createAccountOp)
            {
                destination = KeyPair.FromXdrPublicKey(createAccountOp.Destination.InnerValue);
                startingBalance = Operation.FromXdrAmount(createAccountOp.StartingBalance.InnerValue);
            }

            public Builder(KeyPair destination, String startingBalance)
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
                CreateAccountOperation operation = new CreateAccountOperation(destination, startingBalance);
                if (_SourceAccount != null)
                {
                    operation.SourceAccount = _SourceAccount;
                }
                return operation;
            }
        }
    }
}
