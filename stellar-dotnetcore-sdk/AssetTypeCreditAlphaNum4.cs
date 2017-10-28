using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public class AssetTypeCreditAlphaNum4 : AssetTypeCreditAlphaNum
    {
        public AssetTypeCreditAlphaNum4(string code, KeyPair issuer) : base(code, issuer)
        {
            if (code.Length < 1 || code.Length > 4)
            {
                throw new AssetCodeLengthInvalidException();
            }
        }


        public override String GetType()
        {
            return "credit_alphanum4";
        }


        public override xdr.Asset ToXdr()
        {
            xdr.Asset thisXdr = new xdr.Asset();
            thisXdr.Discriminant = xdr.AssetType.Create(xdr.AssetType.AssetTypeEnum.ASSET_TYPE_CREDIT_ALPHANUM4);
            xdr.Asset.AssetAlphaNum4 credit = new xdr.Asset.AssetAlphaNum4();
            credit.AssetCode = (Util.PaddedByteArray(Code, 4));
            xdr.AccountID accountID = new xdr.AccountID();
            accountID.InnerValue = Issuer.XdrPublicKey;
            credit.Issuer = accountID;
            thisXdr.AlphaNum4 = credit;
            return thisXdr;
        }
    }
}
