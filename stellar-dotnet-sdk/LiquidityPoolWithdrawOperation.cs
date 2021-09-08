using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolWithdrawOperation : Operation
    {
        public string LiquidityPoolID { get; set; }
        public string Amount { get; set; }
        public string MinAmountA { get; set; }
        public string MinAmountB { get; set; }

        private LiquidityPoolWithdrawOperation(string liquidityPoolID, string amount, string minAmountA, string minAmountB)
        {
            LiquidityPoolID = liquidityPoolID ?? throw new ArgumentNullException(nameof(liquidityPoolID), "liquidityPoolID cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            MinAmountA = minAmountA ?? throw new ArgumentNullException(nameof(minAmountA), "minAmountA cannot be null");
            MinAmountB = minAmountB ?? throw new ArgumentNullException(nameof(minAmountB), "minAmountB argument cannot be null");
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            var op = new xdr.LiquidityPoolWithdrawOp();

            //LiquidityPoolID
            var idHash = Util.Hash(Encoding.UTF8.GetBytes(LiquidityPoolID));
            op.LiquidityPoolID = new xdr.PoolID(new xdr.Hash(idHash));

            //Amount
            op.Amount = new xdr.Int64(ToXdrAmount(Amount));

            //Min Amount A
            op.MinAmountA = new xdr.Int64(ToXdrAmount(MinAmountA));

            //Min Amount B
            op.MinAmountB = new xdr.Int64(ToXdrAmount(MinAmountB));

            var body = new xdr.Operation.OperationBody();
            body.Discriminant = xdr.OperationType.Create(xdr.OperationType.OperationTypeEnum.LIQUIDITY_POOL_WITHDRAW);
            body.LiquidityPoolWithdrawOp = op;
            return body;
        }

        /// <summary>
        /// Builds a <see cref="LiquidityPoolWithdrawOperation"/>.
        /// </summary>
        public class Builder
        {
            private readonly string _LiquidityPoolID;
            private readonly string _Amount;
            private readonly string _MinAmountA;
            private readonly string _MinAmountB;

            private IAccountId _SourceAccount;

            public Builder(xdr.LiquidityPoolWithdrawOp op)
            {
                _LiquidityPoolID = op.LiquidityPoolID.InnerValue.InnerValue;
                _Amount = FromXdrAmount(op.Amount.InnerValue);
                _MinAmountA = FromXdrAmount(op.MinAmountA.InnerValue);
                _MinAmountB = FromXdrAmount(op.MinAmountB.InnerValue);
            }

            public Builder(string liquidityPoolID, string amount, string minAmountA, string minAmountB)
            {
                _LiquidityPoolID = liquidityPoolID ?? throw new ArgumentNullException(nameof(liquidityPoolID), "liquidityPoolID cannot be null");
                _Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
                _MinAmountA = minAmountA ?? throw new ArgumentNullException(nameof(minAmountA), "minAmountA cannot be null");
                _MinAmountB = minAmountB ?? throw new ArgumentNullException(nameof(minAmountB), "minAmountB cannot be null");
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
            public LiquidityPoolWithdrawOperation Build()
            {
                var operation = new LiquidityPoolWithdrawOperation(_LiquidityPoolID, _Amount, _MinAmountA, _MinAmountB);
                if (_SourceAccount != null)
                    operation.SourceAccount = _SourceAccount;
                return operation;
            }
        }
    }
}