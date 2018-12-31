using System;
using System.Text;
using stellar_dotnet_sdk.xdr;
using sdkxdr = stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Represents a <see cref="AllowTrustOp"/>.
    /// Use <see cref="Builder"/> to create a new AllowTrustOperation.
    /// 
    /// See also: <see href="https://www.stellar.org/developers/guides/concepts/list-of-operations.html#allow-trust">Allow Trust</see>
    /// </summary>
    public class AllowTrustOperation : Operation
    {
        private AllowTrustOperation(KeyPair trustor, string assetCode, bool authorize)
        {
            Trustor = trustor ?? throw new ArgumentNullException(nameof(trustor), "trustor cannot be null");
            AssetCode = assetCode ?? throw new ArgumentNullException(nameof(assetCode), "assetCode cannot be null");
            Authorize = authorize;
        }

        /// <summary>
        /// The asset code being authorized.
        /// </summary>
        public string AssetCode { get; }

        /// <summary>
        /// The trusting account (the one being authorized)
        /// </summary>
        public KeyPair Trustor { get; }

        /// <summary>
        /// True to authorize the line, false to deauthorize.
        /// </summary>
        public bool Authorize { get; }

        public override OperationThreshold Threshold
        {
            get => OperationThreshold.Low;
        }

        /// <summary>
        /// Returns the Allow Trust XDR Operation Body
        /// </summary>
        /// <returns></returns>
        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            var op = new sdkxdr.AllowTrustOp();

            // trustor
            var trustor = new sdkxdr.AccountID();
            trustor.InnerValue = Trustor.XdrPublicKey;
            op.Trustor = trustor;
            // asset
            var asset = new sdkxdr.AllowTrustOp.AllowTrustOpAsset();
            if (AssetCode.Length <= 4)
            {
                asset.Discriminant = sdkxdr.AssetType.Create(sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4);
                asset.AssetCode4 = Util.PaddedByteArray(AssetCode, 4);
            }
            else
            {
                asset.Discriminant = sdkxdr.AssetType.Create(sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12);
                asset.AssetCode12 = Util.PaddedByteArray(AssetCode, 12);
            }

            op.Asset = asset;
            // authorize
            op.Authorize = Authorize;

            var body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.ALLOW_TRUST);
            body.AllowTrustOp = op;
            return body;
        }

        /// <summary>
        ///     Builds AllowTrust operation.
        /// </summary>
        /// <see cref="AllowTrustOperation" />
        public class Builder
        {
            private readonly string _assetCode;
            private readonly bool _authorize;
            private readonly KeyPair _trustor;

            private KeyPair _sourceAccount;

            /// <summary>
            /// Builder to build the AllowTrust Operation given an XDR AllowTrustOp
            /// </summary>
            /// <param name="op"></param>
            /// <exception cref="Exception"></exception>
            public Builder(sdkxdr.AllowTrustOp op)
            {
                _trustor = KeyPair.FromXdrPublicKey(op.Trustor.InnerValue);
                switch (op.Asset.Discriminant.InnerValue)
                {
                    case sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                        _assetCode = Encoding.UTF8.GetString(op.Asset.AssetCode4);
                        break;
                    case sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                        _assetCode = Encoding.UTF8.GetString(op.Asset.AssetCode12);
                        break;
                    case sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_NATIVE:
                        break;
                    default:
                        throw new Exception("Unknown asset code");
                }

                _authorize = op.Authorize;
            }

            /// <summary>
            ///     Creates a new AllowTrust builder.
            /// </summary>
            /// <param name="trustor">The account of the recipient of the trustline.</param>
            /// <param name="assetCode">
            ///     The asset of the trustline the source account is authorizing. For example, if a gateway wants
            ///     to allow another account to hold its USD credit, the type is USD.
            /// </param>
            /// <param name="authorize">Flag indicating whether the trustline is authorized.</param>
            public Builder(KeyPair trustor, string assetCode, bool authorize)
            {
                _trustor = trustor;
                _assetCode = assetCode;
                _authorize = authorize;
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

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public AllowTrustOperation Build()
            {
                var operation = new AllowTrustOperation(_trustor, _assetCode, _authorize);
                if (_sourceAccount != null)
                    operation.SourceAccount = _sourceAccount;
                return operation;
            }
        }
    }
}