using System;
using stellar_dotnet_sdk.xdr;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="PathPaymentStrictSendOperation"/>.
    /// Use <see cref="Builder"/> to create a new PathPaymentStrictSendOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#path-payment">Path Payment</see>
    /// </summary>
    public class PathPaymentStrictSendOperation : Operation
    {
        private PathPaymentStrictSendOperation(Asset sendAsset, string sendAmount, KeyPair destination,
            Asset destAsset, string destMin, Asset[] path)
        {
            SendAsset = sendAsset ?? throw new ArgumentNullException(nameof(sendAsset), "sendAsset cannot be null");
            SendAmount = sendAmount ?? throw new ArgumentNullException(nameof(sendAmount), "sendAmount cannot be null");
            Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
            DestAsset = destAsset ?? throw new ArgumentNullException(nameof(destAsset), "destAsset cannot be null");
            DestMin = destMin ?? throw new ArgumentNullException(nameof(destMin), "destMin cannot be null");

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

        public string SendAmount { get; }

        public KeyPair Destination { get; }

        public Asset DestAsset { get; }

        public string DestMin { get; }

        public Asset[] Path { get; }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.PathPaymentStrictSendOp();

            // sendAsset
            op.SendAsset = SendAsset.ToXdr();
            // sendAmount
            var sendAmount = new sdkxdr.Int64();
            sendAmount.InnerValue = ToXdrAmount(SendAmount);
            op.SendAmount = sendAmount;
            // destination
            var destination = new sdkxdr.AccountID();
            destination.InnerValue = Destination.XdrPublicKey;
            op.Destination = destination;
            // destAsset
            op.DestAsset = DestAsset.ToXdr();
            // destMin
            var destMin = new sdkxdr.Int64();
            destMin.InnerValue = ToXdrAmount(DestMin);
            op.DestMin = destMin;
            // path
            var path = new sdkxdr.Asset[Path.Length];
            for (var i = 0; i < Path.Length; i++)
                path[i] = Path[i].ToXdr();
            op.Path = path;

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.PATH_PAYMENT_STRICT_RECEIVE);
            body.PathPaymentStrictSendOp = op;
            return body;
        }

        /// <summary>
        ///     Builds a <see cref="PathPaymentOperation"/>.
        /// </summary>
        public class Builder
        {
            private readonly string _DestMin;
            private readonly Asset _DestAsset;
            private readonly KeyPair _Destination;
            private readonly Asset _SendAsset;
            private readonly string _SendAmount;
            private Asset[] _Path;

            private KeyPair _SourceAccount;

            public Builder(sdkxdr.PathPaymentStrictSendOp op)
            {
                _SendAsset = Asset.FromXdr(op.SendAsset);
                _SendAmount = FromXdrAmount(op.SendAmount.InnerValue);
                _Destination = KeyPair.FromXdrPublicKey(op.Destination.InnerValue);
                _DestAsset = Asset.FromXdr(op.DestAsset);
                _DestMin = FromXdrAmount(op.DestMin.InnerValue);
                _Path = new Asset[op.Path.Length];
                for (var i = 0; i < op.Path.Length; i++)
                    _Path[i] = Asset.FromXdr(op.Path[i]);
            }

            /// <summary>
            ///     Creates a new PathPaymentOperation builder.
            /// </summary>
            /// <param name="sendAsset"> The asset deducted from the sender's account.</param>
            /// <param name="sendAmount"> The asset deducted from the sender's account.</param>
            /// <param name="destination"> Payment destination.</param>
            /// <param name="destAsset"> The asset the destination account receives.</param>
            /// <param name="destMin"> The amount of destination asset the destination account receives.</param>
            /// <exception cref="ArithmeticException"> When sendAmount or destMin has more than 7 decimal places.</exception>
            public Builder(Asset sendAsset, string sendAmount, KeyPair destination, Asset destAsset, string destMin)
            {
                _SendAsset = sendAsset ?? throw new ArgumentNullException(nameof(sendAsset), "sendAsset cannot be null");
                _SendAmount = sendAmount ?? throw new ArgumentNullException(nameof(sendAmount), "sendAmount cannot be null");
                _Destination = destination ?? throw new ArgumentNullException(nameof(destination), "destination cannot be null");
                _DestAsset = destAsset ?? throw new ArgumentNullException(nameof(destAsset), "destAsset cannot be null");
                _DestMin = destMin ?? throw new ArgumentNullException(nameof(destMin), "destMin cannot be null");
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
            public PathPaymentStrictSendOperation Build()
            {
                var operation = new PathPaymentStrictSendOperation(_SendAsset, _SendAmount, _Destination, _DestAsset, _DestMin, _Path);
                if (_SourceAccount != null)
                    operation.SourceAccount = _SourceAccount;
                return operation;
            }
        }
    }
}