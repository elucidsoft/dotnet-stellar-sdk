using System;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class ManageOfferOperation : Operation
    {
        private ManageOfferOperation(Asset selling, Asset buying, string amount, string price, long offerId)
        {
            Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
            Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            // offerId can be null
            OfferId = offerId;
        }

        public Asset Selling { get; }

        public Asset Buying { get; }

        public string Amount { get; }

        public string Price { get; }

        public long OfferId { get; }


        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.ManageOfferOp();
            op.Selling = Selling.ToXdr();
            op.Buying = Buying.ToXdr();
            var amount = new sdkxdr.Int64();
            amount.InnerValue = ToXdrAmount(Amount);
            op.Amount = amount;
            var price = stellar_dotnet_sdk.Price.FromString(Price);
            op.Price = price.ToXdr();
            var offerId = new sdkxdr.Uint64();
            offerId.InnerValue = OfferId;
            op.OfferID = offerId;

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.MANAGE_OFFER);
            body.ManageOfferOp = op;

            return body;
        }

        /// <summary>
        ///     Builds ManageOffer operation. If you want to update existing offer use
        ///     <see cref="stellar_dotnet_sdk.ManageOfferOperation.Builder.SetOfferId(long)" />.
        ///     <summary>
        ///         <see cref="ManageOfferOperation" />
        public class Builder
        {
            private readonly string _Amount;
            private readonly Asset _Buying;
            private readonly string _Price;

            private readonly Asset _Selling;

            private KeyPair mSourceAccount;
            private long offerId;

            /// <summary>
            ///     Construct a new CreateAccount builder from a CreateAccountOp XDR.
            /// </summary>
            /// <param name="op">
            ///     <see cref="sdkxdr.ManageOfferOp" />
            /// </param>
            public Builder(sdkxdr.ManageOfferOp op)
            {
                _Selling = Asset.FromXdr(op.Selling);
                _Buying = Asset.FromXdr(op.Buying);
                _Amount = FromXdrAmount(op.Amount.InnerValue);
                var n = new decimal(op.Price.N.InnerValue);
                var d = new decimal(op.Price.D.InnerValue);
                _Price = decimal.Divide(n, d).ToString();
                offerId = op.OfferID.InnerValue;
            }

            /// <summary>
            ///     Creates a new ManageOffer builder. If you want to update existing offer use
            ///     <see cref="stellar_dotnet_sdk.ManageOfferOperation.Builder.SetOfferId(long)" />.
            /// </summary>
            /// <param name="selling">The asset being sold in this operation</param>
            /// <param name="buying"> The asset being bought in this operation</param>
            /// <param name="amount"> Amount of selling being sold.</param>
            /// <param name="price"> Price of 1 unit of selling in terms of buying.</param>
            /// <exception cref="ArithmeticException">when amount has more than 7 decimal places.</exception>
            public Builder(Asset selling, Asset buying, string amount, string price)
            {
                _Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
                _Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
                _Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
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
            public ManageOfferOperation Build()
            {
                var operation = new ManageOfferOperation(_Selling, _Buying, _Amount, _Price, offerId);
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}