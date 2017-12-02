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
        AUTH_REQUIRED_FLAG = AccountFlags.AccountFlagsEnum.AUTH_REQUIRED_FLAG,
        
        /// <summary>
        /// 
        /// </summary>
        AUTH_REVOCABLE_FLAG = AccountFlags.AccountFlagsEnum.AUTH_REVOCABLE_FLAG,
        
        /// <summary>
        /// 
        /// </summary>
        AUTH_IMMUTABLE_FLAG = AccountFlags.AccountFlagsEnum.AUTH_IMMUTABLE_FLAG
    }
}