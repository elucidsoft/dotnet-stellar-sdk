using System;
using System.Text;
using sdkxdr = stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AllowTrustOperation : Operation
    {

        private readonly KeyPair _Trustor;
        private readonly String _AssetCode;
        private readonly bool authorize;

        public string AssetCode => _AssetCode;
        public KeyPair Trustor => _Trustor;
        public bool Authorize => authorize;

        private AllowTrustOperation(KeyPair trustor, String assetCode, bool authorize)
        {
            this._Trustor = trustor ?? throw new ArgumentNullException(nameof(trustor), "trustor cannot be null");
            this._AssetCode = assetCode ?? throw new ArgumentNullException(nameof(assetCode), "assetCode cannot be null");
            this.authorize = authorize;
        }

        public override sdkxdr.Operation.OperationBody ToOperationBody()
        {
            sdkxdr.AllowTrustOp op = new sdkxdr.AllowTrustOp();

            // trustor
            sdkxdr.AccountID trustor = new sdkxdr.AccountID();
            trustor.InnerValue = this.Trustor.XdrPublicKey;
            op.Trustor = trustor;
            // asset
            sdkxdr.AllowTrustOp.AllowTrustOpAsset asset = new sdkxdr.AllowTrustOp.AllowTrustOpAsset();
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

            sdkxdr.Operation.OperationBody body = new sdkxdr.Operation.OperationBody();
            body.Discriminant = sdkxdr.OperationType.Create(sdkxdr.OperationType.OperationTypeEnum.ALLOW_TRUST);
            body.AllowTrustOp = op;
            return body;
        }

        ///<summary>
        ///Builds AllowTrust operation.
        ///</summary>
        ///<see cref="AllowTrustOperation"/> 
        public class Builder
        {
            private readonly KeyPair _Trustor;
            private readonly String _AssetCode;
            private readonly bool _Authorize;

            private KeyPair mSourceAccount;

            Builder(sdkxdr.AllowTrustOp op)
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

            ///<summary>
            /// Creates a new AllowTrust builder.
            ///</summary>
            /// <param name="trustor">The account of the recipient of the trustline.</param>
            /// <param name="assetCode">The asset of the trustline the source account is authorizing. For example, if a gateway wants to allow another account to hold its USD credit, the type is USD.</param> 
            /// <param name="authorize">Flag indicating whether the trustline is authorized.</param>
            public Builder(KeyPair trustor, String assetCode, bool authorize)
            {
                this._Trustor = trustor;
                this._AssetCode = assetCode;
                this._Authorize = authorize;
            }

            ///<summary>
            /// Set source account of this operation
            /// </summary>
            /// <param name="sourceAccount">Source account</param> 
            /// <returns>Builder object so you can chain methods.</param> 
            public Builder SetSourceAccount(KeyPair sourceAccount)
            {
                mSourceAccount = sourceAccount;
                return this;
            }

            ///<summary>
            /// Builds an operation
            ///</summary>
            public AllowTrustOperation Build()
            {
                AllowTrustOperation operation = new AllowTrustOperation(_Trustor, _AssetCode, _Authorize);
                if (mSourceAccount != null)
                {
                    operation.SourceAccount = mSourceAccount;
                }
                return operation;
            }
        }
    }
}