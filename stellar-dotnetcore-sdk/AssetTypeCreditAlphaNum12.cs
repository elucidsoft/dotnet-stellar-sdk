using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class AssetTypeCreditAlphaNum12 : AssetTypeCreditAlphaNum
    {
        public AssetTypeCreditAlphaNum12(string code, KeyPair issuer) : base(code, issuer)
        {
            if (code.Length < 5 || code.Length > 12)
            {
                throw new AssetCodeLengthInvalidException();
            }
        }


        public override String GetType()
        {
            return "credit_alphanum12";
        }


        public override xdr.Asset ToXdr()
        {
            xdr.Asset thisXdr = new xdr.Asset();
            thisXdr.Discriminant = xdr.AssetType.Create(xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM12);
            xdr.Asset.AssetAlphaNum12 credit = new xdr.Asset.AssetAlphaNum12();
            credit.AssetCode = (Util.PaddedByteArray(Code, 12));
            xdr.AccountID accountID = new xdr.AccountID();
            accountID.InnerValue = Issuer.XdrPublicKey;
            credit.Issuer = accountID;
            thisXdr.AlphaNum12 = credit;
            return thisXdr;
        }
    }
}
