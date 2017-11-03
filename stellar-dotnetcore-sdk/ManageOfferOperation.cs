using System;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class ManageOfferOperation : Operation
    {
        private readonly Asset _Selling;
        private readonly Asset _Buying;
        private readonly String _Amount;
        private readonly String _Price;
        private readonly long _OfferId;

        public Asset Selling => _Selling;
        public Asset Buying => _Buying;
        public string Amount => _Amount;
        public string Price => _Price;
        public long OfferId => _OfferId;

        private ManageOfferOperation(Asset selling, Asset buying, String amount, String price, long offerId)
        {
            this._Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
            this._Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
            this._Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            this._Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            // offerId can be null
            this._OfferId = offerId;
        }


        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            sdkxdr.ManageOfferOp op = new sdkxdr.ManageOfferOp();
            op.Selling = Selling.ToXdr();
            op.Buying = Buying.ToXdr();
            sdkxdr.Int64 amount = new sdkxdr.Int64();
            amount.InnerValue = Operation.ToXdrAmount(this.Amount);
            op.Amount = amount;
            Price price = stellar_dotnetcore_sdk.Price.FromString(this.Price);
            op.Price = price.ToXdr();
            sdkxdr.Uint64 offerId = new sdkxdr.Uint64();
            offerId.InnerValue = this.OfferId;
            op.OfferID = offerId;

            sdkxdr.Operation.OperationBody body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.MANAGE_OFFER);
            body.ManageOfferOp = op;

            return body;
        }

        ///<summary>
        /// Builds ManageOffer operation. If you want to update existing offer use
        ///<see cref="stellar_dotnetcore_sdk.ManageOfferOperation.Builder.SetOfferId(long)"/>.
        ///<summary>
        ///<see cref="ManageOfferOperation"/>
        public class Builder
        {

            private readonly Asset _Selling;
            private readonly Asset _Buying;
            private readonly String _Amount;
            private readonly String _Price;
            private long offerId = 0;

            private KeyPair mSourceAccount;

            ///<summary>
            /// Construct a new CreateAccount builder from a CreateAccountOp XDR.
            ///</summary>
            ///<param name="op"><see cref="sdkxdr.ManageOfferOp"/></param>
            public Builder(sdkxdr.ManageOfferOp op)
            {
                _Selling = Asset.FromXdr(op.Selling);
                _Buying = Asset.FromXdr(op.Buying);
                _Amount = Operation.FromXdrAmount(op.Amount.InnerValue);
                var n = new Decimal(op.Price.N.InnerValue);
                var d = new Decimal(op.Price.D.InnerValue);
                _Price = Decimal.Divide(n, d).ToString();
                offerId = op.OfferID.InnerValue;
            }

            ///<summary>
            /// Creates a new ManageOffer builder. If you want to update existing offer use
            ///<see cref="stellar_dotnetcore_sdk.ManageOfferOperation.Builder.SetOfferId(long)"/>.
            ///</summary>
            ///<param name="selling">The asset being sold in this operation</param>
            ///<param name="buying"> The asset being bought in this operation</param>
            ///<param name="amount"> Amount of selling being sold.</param>
            ///<param name="price"> Price of 1 unit of selling in terms of buying.</param>
            ///<exception cref="ArithmeticException">when amount has more than 7 decimal places.</exception>
            public Builder(Asset selling, Asset buying, String amount, String price)
            {
                this._Selling = selling ?? throw new ArgumentNullException(nameof(selling), "selling cannot be null");
                this._Buying = buying ?? throw new ArgumentNullException(nameof(buying), "buying cannot be null");
                this._Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                this._Price = price ?? throw new ArgumentNullException(nameof(price), "price cannot be null");
            }

            ///<summary>
            /// Sets offer ID. <code>0</code> creates a new offer. Set to existing offer ID to change it.
            ///</summary>
            ///<param name="offerId
            public Builder SetOfferId(long offerId)
            {
                this.offerId = offerId;
                return this;
            }

            ///<summary>
            /// Sets the source account for this operation.
            ///</summary>
            ///<param name="sourceAccount">The operation's source account.</param>
            ///<returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            ///<summary>
            /// Builds an operation
            ///</summary>
            public ManageOfferOperation Build()
            {
                ManageOfferOperation operation = new ManageOfferOperation(_Selling, _Buying, _Amount, _Price, offerId);
                if (mSourceAccount != null)
                {
                    operation.SourceAccount = mSourceAccount;
                }
                return operation;
            }
        }
    }
}