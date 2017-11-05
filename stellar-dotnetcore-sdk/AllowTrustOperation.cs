using System;
using System.Text;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AllowTrustOperation : Operation
    {
        private AllowTrustOperation(KeyPair trustor, string assetCode, bool authorize)
        {
            Trustor = trustor ?? throw new ArgumentNullException(nameof(trustor), "trustor cannot be null");
            AssetCode = assetCode ?? throw new ArgumentNullException(nameof(assetCode), "assetCode cannot be null");
            Authorize = authorize;
        }

        public string AssetCode { get; }

        public KeyPair Trustor { get; }

        public bool Authorize { get; }

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
            private readonly string _AssetCode;
            private readonly bool _Authorize;
            private readonly KeyPair _Trustor;

            private KeyPair mSourceAccount;

            public Builder(sdkxdr.AllowTrustOp op)
            {
                _Trustor = KeyPair.FromXdrPublicKey(op.Trustor.InnerValue);
                switch (op.Asset.Discriminant.InnerValue)
                {
                    case sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4:
                        _AssetCode = Encoding.UTF8.GetString(op.Asset.AssetCode4);
                        break;
                    case sdkxdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12:
                        _AssetCode = Encoding.UTF8.GetString(op.Asset.AssetCode12);
                        break;
                    default:
                        throw new Exception("Unknown asset code");
                }
                _Authorize = op.Authorize;
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
                _Trustor = trustor;
                _AssetCode = assetCode;
                _Authorize = authorize;
            }

            /// <summary>
            ///     Set source account of this operation
            /// </summary>
            /// <param name="sourceAccount">Source account</param>
            /// <returns>Builder object so you can chain methods.</param>
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount;
                return this;
            }

            /// <summary>
            ///     Builds an operation
            /// </summary>
            public AllowTrustOperation Build()
            {
                var operation = new AllowTrustOperation(_Trustor, _AssetCode, _Authorize);
                if (mSourceAccount != null)
                    operation.SourceAccount = mSourceAccount;
                return operation;
            }
        }
    }
}