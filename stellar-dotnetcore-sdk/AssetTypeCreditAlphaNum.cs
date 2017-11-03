using System;

namespace stellar_dotnetcore_sdk
{
    public class AssetTypeCreditAlphaNum : Asset
    {
        protected string _Code;
        protected KeyPair _Issuer;

        public AssetTypeCreditAlphaNum(string code, KeyPair issuer)
        {
            _Code = code ?? throw new ArgumentNullException(nameof(code), "code cannot be null");

            if (issuer == null)
                throw new ArgumentNullException(nameof(issuer), "issuer cannot be null");

            _Issuer = KeyPair.FromAccountId(issuer.AccountId);
        }

        public string Code => _Code;
        public KeyPair Issuer => KeyPair.FromAccountId(_Issuer.AccountId);

        public override int GetHashCode()
        {
            //see: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int) 2166136261;
                const int HashingMultiplier = 16777619;

                var hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Code) ? Code.GetHashCode() : 0);
                hash = (hash * HashingMultiplier) ^ (!ReferenceEquals(null, Issuer) ? Issuer.AccountId.GetHashCode() : 0);
                return hash;
            }
        }


        public override bool Equals(object obj)
        {
            var o = (AssetTypeCreditAlphaNum) obj;

            return Code.Equals(o.Code) &&
                   Issuer.AccountId.Equals(o.Issuer.AccountId);
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