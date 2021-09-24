using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnet_sdk
{
    public class LiquidityPoolDepositOperation : Operation
    {
        public LiquidityPoolID LiquidityPoolID { get; set; }
        public string MaxAmountA { get; set; }
        public string MaxAmountB { get; set; }
        public Price MinPrice { get; set; }
        public Price MaxPrice { get; set; }

        private LiquidityPoolDepositOperation(LiquidityPoolID liquidityPoolID, string maxAmountA, string maxAmountB, Price minPrice, Price maxPrice)
        {
            LiquidityPoolID = liquidityPoolID ?? throw new ArgumentNullException(nameof(liquidityPoolID), "liquidityPoolID cannot be null");
            MaxAmountA = maxAmountA ?? throw new ArgumentNullException(nameof(maxAmountA), "maxAmountA cannot be null");
            MaxAmountB = maxAmountB ?? throw new ArgumentNullException(nameof(maxAmountB), "maxAmountB cannot be null");
            MinPrice = minPrice ?? throw new ArgumentNullException(nameof(minPrice), "minPrice cannot be null");
            MaxPrice = maxPrice ?? throw new ArgumentNullException(nameof(maxPrice), "maxPrice cannot be null");
        }

        public override xdr.Operation.OperationBody ToOperationBody()
        {
            xdr.LiquidityPoolDepositOp operationXdr = new xdr.LiquidityPoolDepositOp();
            operationXdr.LiquidityPoolID = LiquidityPoolID.ToXdr();
            operationXdr.MaxAmountA = new xdr.Int64(ToXdrAmount(MaxAmountA));
            operationXdr.MaxAmountB = new xdr.Int64(ToXdrAmount(MaxAmountB));
            operationXdr.MinPrice = MinPrice.ToXdr();
            operationXdr.MaxPrice = MaxPrice.ToXdr();

            xdr.Operation.OperationBody body = new xdr.Operation.OperationBody();
            body.Discriminant.InnerValue = xdr.OperationType.OperationTypeEnum.LIQUIDITY_POOL_DEPOSIT;
            body.LiquidityPoolDepositOp = operationXdr;

            return body;
        }

        public class Builder
        {
            private LiquidityPoolID _liquidityPoolID;
            private Asset _assetA;
            private Asset _assetB;
            private string _maxAmountA;
            private string _maxAmountB;
            private Price _minPrice;
            private Price _maxPrice;

            private KeyPair _sourceAccount;

            public Builder(xdr.LiquidityPoolDepositOp operationXdr)
            {
                _liquidityPoolID = LiquidityPoolID.FromXdr(operationXdr.LiquidityPoolID);
                _maxAmountA = FromXdrAmount(operationXdr.MaxAmountA.InnerValue);
                _maxAmountB = FromXdrAmount(operationXdr.MaxAmountB.InnerValue);
                _minPrice = Price.FromXdr(operationXdr.MinPrice);
                _maxPrice = Price.FromXdr(operationXdr.MaxPrice);
            }

            public Builder(AssetAmount assetAmountA, AssetAmount assetAmountB, Price minPrice, Price maxPrice)
            {
                _assetA = assetAmountA.Asset;
                _assetB = assetAmountB.Asset;
                _maxAmountA = assetAmountA.Amount;
                _maxAmountB = assetAmountB.Amount;
                _minPrice = minPrice;
                _maxPrice = maxPrice;
            }

            public Builder(LiquidityPoolID liquidityPoolID, string maxAmountA, string maxAmountB, Price minPrice, Price maxPrice)
            {
                _liquidityPoolID = liquidityPoolID;
                _maxAmountA = maxAmountA;
                _maxAmountB = maxAmountB;
                _minPrice = minPrice;
                _maxPrice = maxPrice;
            }

            public Builder SetAssetA(Asset asset, string maxAmount)
            {
                _assetA = asset;
                _maxAmountA = maxAmount;
                return this;
            }

            public Builder SetAssetB(Asset asset, string maxAmount)
            {
                _assetB = asset;
                _maxAmountB = maxAmount;
                return this;
            }

            public Builder SetMinPrice(Price minPrice)
            {
                _minPrice = minPrice;
                return this;
            }

            public Builder SetMaxPrice(Price maxPrice)
            {
                _maxPrice = maxPrice;
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

            public LiquidityPoolDepositOperation Build()
            {
                if (_assetA != null && _assetB != null)
                {
                    if (_assetA.CompareTo(_assetB) >= 0)
                    {
                        throw new Exception("Asset A must be < Asset B (Lexicographic Order)");
                    }

                    if (_liquidityPoolID == null)
                    {
                        _liquidityPoolID = new LiquidityPoolID(xdr.LiquidityPoolType.LiquidityPoolTypeEnum.LIQUIDITY_POOL_CONSTANT_PRODUCT, _assetA, _assetB, LiquidityPoolParameters.Fee);
                    }
                }

                LiquidityPoolDepositOperation operation = new LiquidityPoolDepositOperation(_liquidityPoolID, _maxAmountA, _maxAmountB, _minPrice, _maxPrice);

                if (_sourceAccount != null)
                {
                    operation.SourceAccount = _sourceAccount;
                }

                return operation;
            }
        }
    }
}
