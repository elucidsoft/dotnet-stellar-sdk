using System;

namespace stellar_dotnetcore_sdk
{
    /// <summary>
    /// Assets are uniquely identified by the asset code and the issuer. Ultimately, it’s up to the issuer to set the asset code. By convention, however, currencies should be represented by 
    /// the appropriate ISO 4217 code. For stocks and bonds, use the appropriate ISIN number.
    /// </summary>
    public class AssetTypeCreditAlphaNum : Asset
    {
        private readonly KeyPair _issuer;

        /// <summary>
        /// Creates an AssetTypeCreditAlphaNum based on the code and KeyPair
        /// </summary>
        /// <param name="code">The asset code.</param>
        /// <param name="issuer">The KeyPair of the issuer.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AssetTypeCreditAlphaNum(string code, KeyPair issuer)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code), "code cannot be null");

            if (issuer == null)
                throw new ArgumentNullException(nameof(issuer), "issuer cannot be null");

            _issuer = KeyPair.FromAccountId(issuer.AccountId);
        }

        /// <summary>
        /// Return the asset code
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Return the asset issuer
        /// </summary>
        public KeyPair Issuer => KeyPair.FromAccountId(_issuer.AccountId);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            //see: https://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = (int) 2166136261;
                const int hashingMultiplier = 16777619;

                var hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!ReferenceEquals(null, Code) ? Code.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!ReferenceEquals(null, Issuer) ? Issuer.AccountId.GetHashCode() : 0);
                return hash;
            }
        }


        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var o = (AssetTypeCreditAlphaNum) obj;

            return Code.Equals(o.Code) &&
                   Issuer.AccountId.Equals(o.Issuer.AccountId);
        }

        /// <inheritdoc />
        public override string GetType()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override xdr.Asset ToXdr()
        {
            throw new NotSupportedException();
        }
    }
}