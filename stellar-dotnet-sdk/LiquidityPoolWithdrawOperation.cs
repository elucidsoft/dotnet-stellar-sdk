using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolWithdrawOperation : Operation
    {
        public LiquidityPoolID LiquidityPoolID { get; set; }
        public string Amount { get; set; }
        public string MinAmountA { get; set; }
        public string MinAmountB { get; set; }

        private LiquidityPoolWithdrawOperation(LiquidityPoolID liquidityPoolID, string amount, string minAmountA, string minAmountB)
        {
            LiquidityPoolID = liquidityPoolID ?? throw new ArgumentNullException(nameof(liquidityPoolID), "liquidityPoolID cannot be null");
            Amount = amount ?? throw new ArgumentNullException(nameof(amount), "amount cannot be null");
            MinAmountA = minAmountA ?? throw new ArgumentNullException(nameof(minAmountA), "minAmountA cannot be null");
            MinAmountB = minAmountB ?? throw new ArgumentNullException(nameof(minAmountB), "minAmountB cannot be null");
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            xdr.LiquidityPoolWithdrawOp liquidityPoolWithdrawOperationXdr = new xdr.LiquidityPoolWithdrawOp();
            liquidityPoolWithdrawOperationXdr.LiquidityPoolID = LiquidityPoolID.ToXdr();
            liquidityPoolWithdrawOperationXdr.Amount = new xdr.Int64(ToXdrAmount(Amount));
            liquidityPoolWithdrawOperationXdr.MinAmountA = new xdr.Int64(ToXdrAmount(MinAmountA));
            liquidityPoolWithdrawOperationXdr.MinAmountB = new xdr.Int64(ToXdrAmount(MinAmountB));

            xdr.Operation.OperationBody body = new xdr.Operation.OperationBody();
            body.Discriminant.InnerValue = xdr.OperationType.OperationTypeEnum.LIQUIDITY_POOL_WITHDRAW;
            body.LiquidityPoolWithdrawOp = liquidityPoolWithdrawOperationXdr;
            return body;
        }

        public class Builder
        {
            private LiquidityPoolID _liquidityPoolID;
            private string _amount;
            private string _minAmountA;
            private string _minAmountB;

            private KeyPair _sourceAccount;

            public Builder(xdr.LiquidityPoolWithdrawOp operationXdr)
            {
                _liquidityPoolID = LiquidityPoolID.FromXdr(operationXdr.LiquidityPoolID);
                _amount = FromXdrAmount(operationXdr.Amount.InnerValue);
                _minAmountA = FromXdrAmount(operationXdr.MinAmountA.InnerValue);
                _minAmountB = FromXdrAmount(operationXdr.MinAmountB.InnerValue);
            }

            public Builder(LiquidityPoolID liquidityPoolID, string amount, string minAmountA, string minAmountB)
            {
                _liquidityPoolID = liquidityPoolID;
                _amount = amount;
                _minAmountA = minAmountA;
                _minAmountB = minAmountB;
            }

            public Builder(AssetAmount assetA, AssetAmount assetB, string amount)
            {
                _liquidityPoolID = new LiquidityPoolID(LiquiditypoolType.LIQUIDITY_POOL_CONSTANT_PRODUCT, assetA.Asset, assetB.Asset, LiquidityPoolParameters.Fee);
                _amount = amount;
                _minAmountA = assetA.Amount;
                _minAmountB = assetB.Amount;
            }

            public Builder SetLiquidityPoolID(LiquidityPoolID liquidityPoolID)
            {
                _liquidityPoolID = liquidityPoolID;
                return this;
            }

            public Builder SetAmount(string amount)
            {
                _amount = amount;
                return this;
            }

            public Builder SetMinAmountA(string minAmountA)
            {
                _minAmountA = minAmountA;
                return this;
            }

            public Builder SetMinAmountB(string minAmountB)
            {
                _minAmountB = minAmountB;
                return this;
            }

            /// <summary>
            ///     Set source account of this operation
            /// </summary>
            /// <param name="sourceAccount">Source account</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                _sourceAccount = sourceAccount;
                return this;
            }

            public LiquidityPoolWithdrawOperation Build()
            {
                LiquidityPoolWithdrawOperation operation = new LiquidityPoolWithdrawOperation(_liquidityPoolID, _amount, _minAmountA, _minAmountB);

                if (_sourceAccount != null)
                {
                    operation.SourceAccount = _sourceAccount;
                }

                return operation;
            }
        }
    }
}
