using System;
using stellar_dotnet_sdk.xdr;
using Int64 = stellar_dotnet_sdk.xdr.Int64;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="CreateAccountOp"/>.
    /// Use <see cref="Builder"/> to create a new CreateAccountOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#create-account">Create Account</see>
    /// </summary>
    public class CreateAccountOperation : Operation
    {
        /// <summary>
        /// This operation creates and funds a new account with the specified starting balance.
        /// </summary>
        /// <remarks>
        /// Threshold: Medium
        /// Result: <see cref="CreateAccountResult"/>
        /// </remarks>
        /// <param name="destination">Destination account ID to create an account for.</param>
        /// <param name="startingBalance">Amount in XLM the account should be funded for. Must be greater than the <see href="https://www.stellar.org/developers/guides/concepts/fees.html">reserve balance amount.</see></param>
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

        /// <summary>
        ///     Builds a <see cref="CreateAccountOperation"/>.
        /// </summary>
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