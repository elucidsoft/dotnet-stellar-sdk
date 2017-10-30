using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class PaymentOperation : Operation
    {

        private readonly KeyPair _Destination;
        private readonly Asset _Asset;
        private readonly string _Amount;

        public KeyPair Destination { get { return _Destination; } }
        public Asset Asset { get { return _Asset; } }
        public string Amount { get { return _Amount; } }

        private PaymentOperation(KeyPair destination, Asset asset, String amount)
        {
            this._Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            this._Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            this._Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            PaymentOp op = new PaymentOp();

            // destination
            AccountID destination = new AccountID();
            destination.InnerValue = this._Destination.XdrPublicKey;
            op.Destination = destination;
            // asset
            op.Asset = _Asset.ToXdr();
            // amount
            xdr.Int64 amount = new xdr.Int64();
            amount.InnerValue = Operation.ToXdrAmount(this._Amount);
            op.Amount = amount;

            xdr.Operation.OperationBody body = new xdr.Operation.OperationBody();
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
            private readonly KeyPair destination;
            private readonly Asset asset;
            private readonly String amount;

            private KeyPair mSourceAccount;

            /**
             * Construct a new PaymentOperation builder from a PaymentOp XDR.
             * @param op {@link PaymentOp}
             */
            public Builder(PaymentOp op)
            {
                destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
                asset = Asset.FromXdr(op.Asset);
                amount = Operation.FromXdrAmount(op.Amount.InnerValue);
            }

            /**
             * Creates a new PaymentOperation builder.
             * @param destination The destination keypair (uses only the public key).
             * @param asset The asset to send.
             * @param amount The amount to send in lumens.
             * @throws ArithmeticException when amount has more than 7 decimal places.
             */
            public Builder(KeyPair destination, Asset asset, String amount)
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
                PaymentOperation operation = new PaymentOperation(destination, asset, amount);
                if (mSourceAccount != null)
                {
                    operation.SourceAccount = mSourceAccount;
                }
                return operation;
            }
        }
    }
}
