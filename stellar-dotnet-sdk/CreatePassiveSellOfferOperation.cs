using System;
using stellar_dotnet_sdk.xdr;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="CreatePassiveOfferOp"/>.
    /// Use <see cref="Builder"/> to create a new CreatePassiveSellOfferOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#create-passive-offer">Manage Offer</see>
    /// </summary>
    public class CreatePassiveSellOfferOperation : Operation
    {
        private CreatePassiveSellOfferOperation(Asset selling, Asset buying, string amount, string price)
        {
            Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
            Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
        }

        public Asset Selling { get; }

        public Asset Buying { get; }

        public string Amount { get; }

        public string Price { get; }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.CreatePassiveSellOfferOp();
            op.Selling = Selling.ToXdr();
            op.Buying = Buying.ToXdr();
            var amount = new sdkxdr.Int64();
            amount.InnerValue = ToXdrAmount(Amount);
            op.Amount = amount;
            var price = stellar_dotnet_sdk.Price.FromString(Price);
            op.Price = price.ToXdr();

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.CREATE_PASSIVE_SELL_OFFER);
            body.CreatePassiveSellOfferOp = op;

            return body;
        }

        /// <summary>
        ///     Builds CreatePassiveOffer operation.
        /// </summary>
        /// <see cref="CreatePassiveSellOfferOperation" />
        public class Builder
        {
            private readonly string _Amount;
            private readonly Asset _Buying;
            private readonly string _Price;

            private readonly Asset _Selling;

            private KeyPair mSourceAccount;

            /// <summary>
            ///     Construct a new CreatePassiveOffer builder from a CreatePassiveOfferOp XDR.
            /// </summary>
            /// <param name="op"></param>
            public Builder(sdkxdr.CreatePassiveSellOfferOp op)
            {
                _Selling = Asset.FromXdr(op.Selling);
                _Buying = Asset.FromXdr(op.Buying);
                _Amount = FromXdrAmount(op.Amount.InnerValue);
                var n = new decimal(op.Price.N.InnerValue);
                var d = new decimal(op.Price.D.InnerValue);
                _Price = decimal.Divide(n, d).ToString();
            }

            /// <summary>
            ///     Creates a new CreatePassiveOffer builder.
            /// </summary>
            /// <param name="selling">The asset being sold in this operation</param>
            /// <param name="buying">The asset being bought in this operation</param>
            /// <param name="amount">Amount of selling being sold.</param>
            /// <param name="price">Price of 1 unit of selling in terms of buying.</param>
            /// <exception cref="ArithmeticException">when amount has more than 7 decimal places.</exception>
            public Builder(Asset selling, Asset buying, string amount, string price)
            {
                _Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
                _Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
                _Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                _Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            }

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public CreatePassiveSellOfferOperation Build()
            {
                var operation = new CreatePassiveSellOfferOperation(_Selling, _Buying, _Amount, _Price);
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}