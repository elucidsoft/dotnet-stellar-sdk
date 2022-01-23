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
        private PaymentOperation(IAccountId destination, Asset asset, string amount)
        {
            Destination = destination ??
                          throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            Asset = asset ?? throw new ArgumentNullException(nameof(asset), "asset cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
        }

        /// <summary>
        /// Account address that receives the payment.
        /// </summary>
        public IAccountId Destination { get; }

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
            op.Destination = Destination.MuxedAccount;
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
        ///<see cref="PathPaymentStrictReceiveOperation"/>
        /// ///<see cref="PathPaymentStrictSendOperation"/>
        public class Builder
        {
            private readonly string _amount;
            private readonly Asset _asset;
            private IAccountId _destination;

            private IAccountId _sourceAccount;

            ///<summary>
            /// Construct a new PaymentOperation builder from a PaymentOp XDR.
            ///</summary>
            ///<param name="op"><see cref="PaymentOp"/></param>
            public Builder(PaymentOp op)
            {
                _destination = MuxedAccount.FromXdrMuxedAccount(op.Destination);
                _asset = Asset.FromXdr(op.Asset);
                _amount = FromXdrAmount(op.Amount.InnerValue);
            }

            ///<summary>
            /// Creates a new PaymentOperation builder.
            ///</summary>
            ///<param name="destination">The Muxed destination</param>
            ///<param name="asset">The asset to send.</param>
            ///<param name="amount">The amount to send in lumens.</param>
            public Builder(string destination, Asset asset, string amount)
            {
                _destination = ConvertDestinationToAccountId(destination);
                _asset = asset;
                _amount = amount;
            }

            ///<summary>
            /// Creates a new PaymentOperation builder.
            ///</summary>
            ///<param name="destination">The destination keypair (uses only the public key).</param>
            ///<param name="asset">The asset to send.</param>
            ///<param name="amount">The amount to send in lumens.</param>
            public Builder(IAccountId destination, Asset asset, string amount)
            {
                _destination = destination;
                _asset = asset;
                _amount = amount;
            }

            ///<summary>
            /// Sets the source account for this operation.
            ///</summary>
            ///<param name="account">The operation's source account.</param>
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetSourceAccount(IAccountId account)
            {
                _sourceAccount = account;
                return this;
            }

            ///<summary>
            /// Sets the source account for this operation.
            ///</summary>
            ///<param name="destination">The operation's muxed destination.</param>
            ///<returns>Builder object so you can chain methods.</returns>
            ///
            public Builder SetSourceAccount(string destination)
            {
                _sourceAccount = ConvertDestinationToAccountId(destination);
                return this;
            }

            ///<summary>
            /// Builds an operation
            ///</summary>
            public PaymentOperation Build()
            {
                var operation = new PaymentOperation(_destination, _asset, _amount);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }

            private static IAccountId ConvertDestinationToAccountId(string destination)
            {
                var (id, key) = StrKey.DecodeStellarMuxedAccount(destination);
                return MuxedAccount.FromXdrMuxedAccount(new MuxedAccountMed25519(new KeyPair(key), id).MuxedAccount);
            }
        }
    }
}