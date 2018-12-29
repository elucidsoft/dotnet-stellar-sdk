using System;
using stellar_dotnet_sdk.xdr;
using Int64 = stellar_dotnet_sdk.xdr.Int64;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="PaymentOp"/> operation.
    /// Use <see cref="Builder"/> to create a new PaymentOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#payment">Payment</see>
    /// </summary>
    public class PaymentOperation : Operation
    {
        private PaymentOperation(KeyPair destination, Asset asset, string amount)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
        }

        /// <summary>
        /// Account address that receives the payment.
        /// </summary>
        public KeyPair Destination { get; }

        /// <summary>
        /// Asset to send to the destination account.
        /// </summary>
        public Asset Asset { get; }

        /// <summary>
        /// Amount of the aforementioned asset to send.
        /// </summary>
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

        ///<summary>
        /// Builds Payment operation.
        ///</summary>
        ///<see cref="PathPaymentOperation"/>
        public class Builder
        {
            private readonly string amount;
            private readonly Asset asset;
            private readonly KeyPair destination;

            private KeyPair mSourceAccount;

            ///<summary>
            /// Construct a new PaymentOperation builder from a PaymentOp XDR.
            ///</summary>
            ///<param name="op"><see cref="PaymentOp"/></param> 
            public Builder(PaymentOp op)
            {
                destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
                asset = Asset.FromXdr(op.Asset);
                amount = FromXdrAmount(op.Amount.InnerValue);
            }

            ///<summary>
            /// Creates a new PaymentOperation builder.
            ///</summary>
            ///<param name="destination">The destination keypair (uses only the public key).</param> 
            ///<param name="asset">The asset to send.</param> 
            ///<param name="amount">The amount to send in lumens.</param> 
            public Builder(KeyPair destination, Asset asset, string amount)
            {
                this.destination = destination;
                this.asset = asset;
                this.amount = amount;
            }

            ///<summary>
            /// Sets the source account for this operation.
            ///</summary>
            ///<param name="account">The operation's source account.</param> 
            ///<returns>Builder object so you can chain methods.</returns> 
            ///
            public Builder SetSourceAccount(KeyPair account)
            {
                mSourceAccount = account;
                return this;
            }

            ///<summary>
            /// Builds an operation
            ///</summary>
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