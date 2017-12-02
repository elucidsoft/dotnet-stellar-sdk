using stellar_dotnetcore_sdk.xdr;

namespace stellar_dotnetcore_sdk
{
    /// <summary>
    /// 
    /// </summary>
    public enum AccountFlag
    {
        /// <summary>
        /// 
        /// </summary>
        AuthRequiredFlag = AccountFlags.AccountFlagsEnum.AUTH_REQUIRED_FLAG,
        
        /// <summary>
        /// 
        /// </summary>
        AuthRevocableFlag = AccountFlags.AccountFlagsEnum.AUTH_REVOCABLE_FLAG,
        
        /// <summary>
        /// 
        /// </summary>
        AuthImmutableFlag = AccountFlags.AccountFlagsEnum.AUTH_IMMUTABLE_FLAG
    }
}