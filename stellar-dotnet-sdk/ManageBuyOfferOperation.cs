using System;
using stellar_dotnet_sdk.xdr;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="ManageBuyOfferOp"/>.
    /// Use <see cref="Builder"/> to create a new ManageSellOfferOperation.
    ///
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#manage-offer">Manage Offer</see>
    /// </summary>
    public class ManageBuyOfferOperation : Operation
    {
        private ManageBuyOfferOperation(Asset selling, Asset buying, string buyAmount, string price, long offerId)
        {
            Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
            Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
            BuyAmount = buyAmount ?? throw new ArgumentNullException(nameof(buyAmount), "buyAmount cannot be null");
            Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            // offerId can be null
            OfferId = offerId;
        }

        public Asset Selling { get; }

        public Asset Buying { get; }

        public string BuyAmount { get; }

        public string Price { get; }

        public long OfferId { get; }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.ManageBuyOfferOp() {Selling = Selling.ToXdr(), Buying = Buying.ToXdr()};
            var amount = new sdkxdr.Int64 {InnerValue = ToXdrAmount(BuyAmount)};
            op.BuyAmount = amount;
            var price = stellar_dotnet_sdk.Price.FromString(Price);
            op.Price = price.ToXdr();
            var offerId = new sdkxdr.Int64 {InnerValue = OfferId};
            op.OfferID = offerId;

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.MANAGE_BUY_OFFER);
            body.ManageBuyOfferOp = op;

            return body;
        }

        public class Builder {
            private readonly string _BuyAmount;
            private readonly Asset _Buying;
            private readonly string _Price;

            private readonly Asset _Selling;

            private KeyPair mSourceAccount;
            private long offerId;

            /// <summary>
            ///     Construct a new ManageBuyOffer builder from a ManageBuyOfferOp XDR.
            /// </summary>
            /// <param name="op">
            ///     <see cref="sdkxdr.ManageBuyOfferOp" />
            /// </param>
            public Builder(sdkxdr.ManageBuyOfferOp op)
            {
                _Selling = Asset.FromXdr(op.Selling);
                _Buying = Asset.FromXdr(op.Buying);
                _BuyAmount = FromXdrAmount(op.BuyAmount.InnerValue);
                var n = new decimal(op.Price.N.InnerValue);
                var d = new decimal(op.Price.D.InnerValue);
                _Price = decimal.Divide(n, d).ToString();
                offerId = op.OfferID.InnerValue;
            }

            /// <summary>
            ///     Creates a new ManageBuyOffer builder. If you want to update existing offer use
            ///     <see cref="ManageBuyOfferOperation.Builder.SetOfferId(long)" />.
            /// </summary>
            /// <param name="selling">The asset being sold in this operation</param>
            /// <param name="buying"> The asset being bought in this operation</param>
            /// <param name="buyAmount"> Amount being bought.</param>
            /// <param name="price"> Price of 1 unit of buying in terms of selling.</param>
            /// <exception cref="ArithmeticException">when amount has more than 7 decimal places.</exception>
            public Builder(Asset selling, Asset buying, string amount, string price)
            {
                _Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
                _Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
                _BuyAmount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                _Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            }

            /// <summary>
            ///     Sets offer ID. <code>0</code> creates a new offer. Set to existing offer ID to change it.
            /// </summary>
            /// <param name="offerId
            public Builder SetOfferId(long offerId)
            {
                this.offerId = offerId;
                return this;
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
            public ManageBuyOfferOperation Build()
            {
                var operation = new ManageBuyOfferOperation(_Selling, _Buying, _BuyAmount, _Price, offerId);
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}