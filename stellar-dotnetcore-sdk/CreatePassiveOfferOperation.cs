using System;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class CreatePassiveOfferOperation : Operation
    {
        private readonly Asset _Selling;
        private readonly Asset _Buying;
        private readonly String _Amount;
        private readonly String _Price;

        public Asset Selling => _Selling;
        public Asset Buying => _Buying;
        public string Amount => _Amount;
        public string Price => _Price;

        private CreatePassiveOfferOperation(Asset selling, Asset buying, String amount, String price)
        {
            this._Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
            this._Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
            this._Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            this._Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
        }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            sdkxdr.CreatePassiveOfferOp op = new sdkxdr.CreatePassiveOfferOp();
            op.Selling = Selling.ToXdr();
            op.Buying = Buying.ToXdr();
            sdkxdr.Int64 amount = new sdkxdr.Int64();
            amount.InnerValue = Operation.ToXdrAmount(this.Amount);
            op.Amount = amount;
            Price price = stellar_dotnetcore_sdk.Price.FromString(this.Price);
            op.Price = price.ToXdr();

            sdkxdr.Operation.OperationBody body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.CREATE_PASSIVE_OFFER);
            body.CreatePassiveOfferOp = op;

            return body;
        }

        ///<summary>
        /// Builds CreatePassiveOffer operation.
        /// </summary>
        /// <see cref="CreatePassiveOfferOperation"/>
        public class Builder
        {

            private readonly Asset _Selling;
            private readonly Asset _Buying;
            private readonly String _Amount;
            private readonly String _Price;

            private KeyPair mSourceAccount;

            ///<summary>
            /// Construct a new CreatePassiveOffer builder from a CreatePassiveOfferOp XDR.
            /// </summary>
            /// <param name="op"></param>
            public Builder(sdkxdr.CreatePassiveOfferOp op)
            {
                _Selling = Asset.FromXdr(op.Selling);
                _Buying = Asset.FromXdr(op.Buying);
                _Amount = Operation.FromXdrAmount(op.Amount.InnerValue);
                var n = new Decimal(op.Price.N.InnerValue);
                var d = new Decimal(op.Price.D.InnerValue);
                _Price = Decimal.Divide(n, d).ToString();
            }

            ///<summary>
            /// Creates a new CreatePassiveOffer builder.
            /// </summary>
            /// <param name="selling">The asset being sold in this operation</param> 
            /// <param name="buying">The asset being bought in this operation</param>
            /// <param name="amount">Amount of selling being sold.</param>
            /// <param name="price" Price of 1 unit of selling in terms of buying.</param>
            /// <exception cref="ArithmeticException">when amount has more than 7 decimal places.</param>
            public Builder(Asset selling, Asset buying, String amount, String price)
            {
                this._Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
                this._Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
                this._Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                this._Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            }

            ///<summary>
            /// Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount">The operation's source account.</param> 
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            ///<summary>
            /// Builds an operation
            /// </summary>
            public CreatePassiveOfferOperation Build()
            {
                CreatePassiveOfferOperation operation = new CreatePassiveOfferOperation(_Selling, _Buying, _Amount, _Price);
                if (mSourceAccount != null)
                {
                    operation.SourceAccount = mSourceAccount;
                }
                return operation;
            }
        }
    }
}