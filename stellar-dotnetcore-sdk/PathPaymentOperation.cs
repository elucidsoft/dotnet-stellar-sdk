using System;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class PathPaymentOperation : Operation
    {
        private PathPaymentOperation(Asset sendAsset, string sendMax, KeyPair destination,
            Asset destAsset, string destAmount, Asset[] path)
        {
            SendAsset = sendAsset ?? throw new ArgumentNullException(nameof(sendAsset), "sendAsset cannot be null");
            SendMax = sendMax ?? throw new ArgumentNullException(nameof(sendMax), "sendMax cannot be null");
            Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            DestAsset = destAsset ?? throw new ArgumentNullException(nameof(destAsset), "destAsset cannot be null");
            DestAmount = destAmount ?? throw new ArgumentNullException(nameof(destAmount), "destAmount cannot be null");

            if (path == null)
            {
                Path = new Asset[0];
            }
            else
            {
                if (path.Length > 5)
                    throw new ArgumentException(nameof(path), "The maximum number of assets in the path is 5");
                Path = path;
            }
        }


        public Asset SendAsset { get; }

        public string SendMax { get; }

        public KeyPair Destination { get; }

        public Asset DestAsset { get; }

        public string DestAmount { get; }

        public Asset[] Path { get; }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.PathPaymentOp();

            // sendAsset
            op.SendAsset = SendAsset.ToXdr();
            // sendMax
            var sendMax = new sdkxdr.Int64();
            sendMax.InnerValue = ToXdrAmount(SendMax);
            op.SendMax = sendMax;
            // destination
            var destination = new sdkxdr.AccountID();
            destination.InnerValue = Destination.XdrPublicKey;
            op.Destination = destination;
            // destAsset
            op.DestAsset = DestAsset.ToXdr();
            // destAmount
            var destAmount = new sdkxdr.Int64();
            destAmount.InnerValue = ToXdrAmount(DestAmount);
            op.DestAmount = destAmount;
            // path
            var path = new sdkxdr.Asset[Path.Length];
            for (var i = 0; i < Path.Length; i++)
                path[i] = Path[i].ToXdr();
            op.Path = path;

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.PATH_PAYMENT);
            body.PathPaymentOp = op;
            return body;
        }

        /// <summary>
        ///     Builds PathPayment operation.
        /// </summary>
        /// <see cref="PathPaymentOperation" />
        public class Builder
        {
            private readonly string _DestAmount;
            private readonly Asset _DestAsset;
            private readonly KeyPair _Destination;
            private readonly Asset _SendAsset;
            private readonly string _SendMax;
            private Asset[] _Path;

            private KeyPair _SourceAccount;

            public Builder(sdkxdr.PathPaymentOp op)
            {
                _SendAsset = Asset.FromXdr(op.SendAsset);
                _SendMax = FromXdrAmount(op.SendMax.InnerValue);
                _Destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
                _DestAsset = Asset.FromXdr(op.DestAsset);
                _DestAmount = FromXdrAmount(op.DestAmount.InnerValue);
                _Path = new Asset[op.Path.Length];
                for (var i = 0; i < op.Path.Length; i++)
                    _Path[i] = Asset.FromXdr(op.Path[i]);
            }

            /// <summary>
            ///     Creates a new PathPaymentOperation builder.
            ///     <param name="sendAsset"> The asset deducted from the sender's account.</param>
            ///     <param name="sendMax"> The asset deducted from the sender's account.</param>
            ///     <param name="destination"> Payment destination.</param>
            ///     <param name="destAsset"> The asset the destination account receives.</param>
            ///     <param name="destAmount"> The amount of destination asset the destination account receives.</param>
            ///     <exception cref="ArithmeticException"> When sendMax or destAmount has more than 7 decimal places.</exception>
            public Builder(Asset sendAsset, string sendMax, KeyPair destination, Asset destAsset, string destAmount)
            {
                _SendAsset = sendAsset ?? throw new ArgumentNullException(nameof(sendAsset), "sendAsset cannot be null");
                _SendMax = sendMax ?? throw new ArgumentNullException(nameof(sendMax), "sendMax cannot be null");
                _Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
                _DestAsset = destAsset ?? throw new ArgumentNullException(nameof(destAsset), "destAsset cannot be null");
                _DestAmount = destAmount ?? throw new ArgumentNullException(nameof(destAmount), "destAmount cannot be null");
            }

            /// <summary>
            ///     Sets path for this operation
            ///     <param name="path">
            ///         The assets (other than send asset and destination asset) involved in the offers the path takes.
            ///         For example, if you can only find a path from USD to EUR through XLM and BTC, the path would be USD -&raquo;
            ///         XLM -&raquo; BTC -&raquo; EUR and the path field would contain XLM and BTC.
            ///     </param>
            ///     <returns>Builder object so you can chain methods</returns>
            ///     .
            public Builder SetPath(Asset[] path)
            {
                if (path == null)
                    throw new ArgumentNullException(nameof(path), "path cannot be null");

                if (path.Length > 5)
                    throw new ArgumentException(nameof(path), "The maximum number of assets in the path is 5");
                _Path = path;
                return this;
            }

            /// <summary>
            ///     Sets the source account for this operation.
            /// </summary>
            /// <param name="sourceAccount"> The operation's source account.</param>
            /// <returns>Builder object so you can chain methods.</returns>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                _SourceAccount = sourceAccount ?? throw new ArgumentNullException(nameof(sourceAccount), "sourceAccount cannot be null");
                return this;
            }


            /// <summary>
            ///     Builds an operation
            /// </summary>
            /// <returns></returns>
            public PathPaymentOperation Build()
            {
                var operation = new PathPaymentOperation(_SendAsset, _SendMax, _Destination, _DestAsset, _DestAmount, _Path);
                if (_SourceAccount != null)
                    operation.SourceAccount = _SourceAccount;
                return operation;
            }
        }
    }
}