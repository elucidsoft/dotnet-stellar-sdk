using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AssetTypeCreditAlphaNum12 : AssetTypeCreditAlphaNum
    {
        public AssetTypeCreditAlphaNum12(string code, KeyPair issuer) : base(code, issuer)
        {
            if (code.Length < 5 || code.Length > 12)
                throw new AssetCodeLengthInvalidException();
        }


        public override string GetType()
        {
            return "credit_alphanum12";
        }


        public override xdr.Asset ToXdr()
        {
            var thisXdr = new xdr.Asset();
            thisXdr.Discriminant = AssetType.Create(AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12);
            var credit = new xdr.Asset.AssetAlphaNum12();
            credit.AssetCode = Util.PaddedByteArray(Code, 12);
            var accountID = new AccountID();
            accountID.InnerValue = Issuer.XdrPublicKey;
            credit.Issuer = accountID;
            thisXdr.AlphaNum12 = credit;
            return thisXdr;
        }
    }
}