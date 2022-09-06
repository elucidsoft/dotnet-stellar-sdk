using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    public class AssetTypeCreditAlphaNum12 : AssetTypeCreditAlphaNum
    {
        public const string RestApiType = "credit_alphanum12";

        public AssetTypeCreditAlphaNum12(string code, string issuer) : base(code, issuer)
        {
            if (code.Length < 5 || code.Length > 12)
                throw new AssetCodeLengthInvalidException();
        }

        public override string Type => RestApiType;

        public override xdr.Asset ToXdr()
        {
            var thisXdr = new xdr.Asset();
            thisXdr.Discriminant = AssetType.Create(AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12);
            var credit = new xdr.AlphaNum12();
            credit.AssetCode = new AssetCode12(Util.PaddedByteArray(Code, 12));
            var accountID = new AccountID();
            accountID.InnerValue = KeyPair.FromAccountId(Issuer).XdrPublicKey;
            credit.Issuer = accountID;
            thisXdr.AlphaNum12 = credit;
            return thisXdr;
        }

        public override int CompareTo(Asset asset)
        {
            if (asset.Type != RestApiType)
            {
                return 1;
            }

            AssetTypeCreditAlphaNum other = (AssetTypeCreditAlphaNum)asset;

            if (Code != other.Code)
            {
                return Code.CompareTo(other.Code);
            }

            return Issuer.CompareTo(other.Issuer);
        }
    }
}