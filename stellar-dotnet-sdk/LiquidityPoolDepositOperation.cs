using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolDepositOperation : Operation
    {
        public string LiquidityPoolID { get; set; }
        public string MaxAmountA { get; set; }
        public string MaxAmountB { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }

        public LiquidityPoolDepositOperation(string liquidityPoolID, string maxAmountA, string maxAmountB, string minPrice, string maxPrice)
        {
            LiquidityPoolID = liquidityPoolID ?? throw new ArgumentNullException(nameof(liquidityPoolID), "liquidityPoolID cannot be null");
            MaxAmountA = maxAmountA ?? throw new ArgumentNullException(nameof(maxAmountA), "maxAmountA cannot be null");
            MaxAmountB = maxAmountB ?? throw new ArgumentNullException(nameof(maxAmountB), "maxAmountB cannot be null");
            MinPrice = minPrice ?? throw new ArgumentNullException(nameof(minPrice), "minPrice argument cannot be null");
            MaxPrice = maxPrice ?? throw new ArgumentNullException(nameof(maxPrice), "maxPrice argument cannot be null");
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var op = new xdr.LiquidityPoolDepositOp();

            //LiquidityPoolID
            var idHash = Util.Hash(Encoding.UTF8.GetBytes(LiquidityPoolID));
            op.LiquidityPoolID = new xdr.PoolID(new xdr.Hash(idHash));

            //Max Amount A
            op.MaxAmountA = new xdr.Int64(ToXdrAmount(MaxAmountA));

            //Max Amount B
            op.MaxAmountB = new xdr.Int64(ToXdrAmount(MaxAmountB));

            //Min Price
            var minPrice = Price.FromString(MinPrice);
            op.MinPrice = new xdr.Price() { N = new xdr.Int32(minPrice.Numerator), D = new xdr.Int32(minPrice.Denominator) };

            //Max Price
            var maxPrice = Price.FromString(MaxPrice);
            op.MaxPrice = new xdr.Price() { N = new xdr.Int32(maxPrice.Numerator), D = new xdr.Int32(maxPrice.Denominator) };

            var body = new xdr.Operation.OperationBody();
            body.Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.LIQUIDITY_POOL_DEPOSIT);
            body.LiquidityPoolDepositOp = op;
            return body;
        }

        /// <summary>
        /// Builds a <see cref="LiquidityPoolDepositOperation"/>.
        /// </summary>
        public class Builder
        {
            private readonly string _LiquidityPoolID;
            private readonly string _MaxAmountA;
            private readonly string _MaxAmountB;
            private readonly string _MinPrice;
            private readonly string _MaxPrice;

            private IAccountId _SourceAccount;

            public Builder(xdr.LiquidityPoolDepositOp op)
            {
                _LiquidityPoolID = xdr.PoolID.op.LiquidityPoolID.InnerValue;
                _MaxAmountA = FromXdrAmount(op.MaxAmountA.InnerValue);
                _MaxAmountB = FromXdrAmount(op.MaxAmountB.InnerValue);
                _MinPrice = Price.FromXdr(op.MinPrice).ToString();
                _MaxPrice = Price.FromXdr(op.MaxPrice).ToString();
            }

            public Builder(string liquidityPoolID, string maxAmountA, string maxAmountB, string minPrice, string maxPrice)
            {
                _LiquidityPoolID = liquidityPoolID ?? throw new ArgumentNullException(nameof(liquidityPoolID), "liquidityPoolID cannot be null");
                _MaxAmountA = maxAmountA ?? throw new ArgumentNullException(nameof(maxAmountA), "maxAmountA cannot be null");
                _MaxAmountB = maxAmountB ?? throw new ArgumentNullException(nameof(maxAmountB), "maxAmountB cannot be null");
                _MinPrice = minPrice ?? throw new ArgumentNullException(nameof(minPrice), "minPrice cannot be null");
                _MaxPrice = maxPrice ?? throw new ArgumentNullException(nameof(maxPrice), "maxPrice cannot be null");
            }

            /// <summary>
            /// Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount"> The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(IAccountId sourceAccount)
            {
                _SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }

            /// <summary>
            /// Builds an operation
            /// </summary>
            /// <returns></returns>
            public LiquidityPoolDepositOperation Build()
            {
                var operation = new LiquidityPoolDepositOperation(_LiquidityPoolID, _MaxAmountA, _MaxAmountB, _MinPrice, _MinPrice);
                if (_SourceAccount != null)
                    operation.SourceAccount = _SourceAccount;
                return operation;
            }
        }
    }
}