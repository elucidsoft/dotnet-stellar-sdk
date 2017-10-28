using System;
using System.Collections.Generic;
using System.Text;
using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    public class AssetTypeCreditAlphaNum : Asset
    {
        protected String _Code;
        protected KeyPair _Issuer;

        public string Code { get { return _Code; } }
        public KeyPair Issuer { get { return KeyPair.FromAccountId(_Issuer.AccountId); } }

        public AssetTypeCreditAlphaNum(String code, KeyPair issuer)
        {
            _Code = code ?? throw new ArgumentNullException(nameof(code), "code cannot be null");

            if (issuer == null)
                throw new ArgumentNullException(nameof(issuer), "issuer cannot be null");

            _Issuer = KeyPair.FromAccountId(issuer.AccountId);
           
        }

        public override int GetHashCode()
        {
            //see: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, this.Code) ? this.Code.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, this.Issuer) ? this.Issuer.AccountId.GetHashCode() : 0);
                return hash;
            }
        }


        public override bool Equals(Object obj)
        {
            AssetTypeCreditAlphaNum o = (AssetTypeCreditAlphaNum)obj;

            return this.Code.Equals(o.Code) &&
                    this.Issuer.AccountId.Equals(o.Issuer.AccountId);
        }

        public override string GetType()
        {
            throw new NotImplementedException();
        }

        public override xdr.Asset ToXdr()
        {
            throw new NotImplementedException();
        }
    }
}
