using System;
using stellar_dotnetcore_sdk.xdr;
using Int64 = stellar_dotnetcore_sdk.xdr.Int64;

namespace stellar_dotnetcore_sdk
{
    public class PaymentOperation : Operation
    {
        private PaymentOperation(KeyPair destination, Asset asset, string amount)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
        }

        public KeyPair Destination { get; }

        public Asset Asset { get; }

        public string Amount { get; }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var op = new PaymentOp();

            // destination
            var destination = new AccountID();
            destination.InnerValue = Destination.XdrPublicKey;
            op.Destination = destination;
            // asset
            op.Asset = Asset.ToXdr();
            // amount
            var amount = new Int64();
            amount.InnerValue = ToXdrAmount(Amount);
            op.Amount = amount;

            var body = new xdr.Operation.OperationBody();
            body.Discriminant = OperationType.Create(OperationType.OperationTypeEnum.PAYMENT);
            body.PaymentOp = op;
            return body;
        }

        /**
         * Builds Payment operation.
         * @see PathPaymentOperation
         */
        public class Builder
        {
            private readonly string amount;
            private readonly Asset asset;
            private readonly KeyPair destination;

            private KeyPair mSourceAccount;

            /**
             * Construct a new PaymentOperation builder from a PaymentOp XDR.
             * @param op {@link PaymentOp}
             */
            public Builder(PaymentOp op)
            {
                destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
                asset = Asset.FromXdr(op.Asset);
                amount = FromXdrAmount(op.Amount.InnerValue);
            }

            /**
             * Creates a new PaymentOperation builder.
             * @param destination The destination keypair (uses only the public key).
             * @param asset The asset to send.
             * @param amount The amount to send in lumens.
             * @throws ArithmeticException when amount has more than 7 decimal places.
             */
            public Builder(KeyPair destination, Asset asset, string amount)
            {
                this.destination = destination;
                this.asset = asset;
                this.amount = amount;
            }

            /**
             * Sets the source account for this operation.
             * @param account The operation's source account.
             * @return Builder object so you can chain methods.
             */
            public Builder SetSourceAccount(KeyPair account)
            {
                mSourceAccount = account;
                return this;
            }

            /**
             * Builds an operation
             */
            public PaymentOperation Build()
            {
                var operation = new PaymentOperation(destination, asset, amount);
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}