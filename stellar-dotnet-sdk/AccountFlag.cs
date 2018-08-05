using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// 
    /// </summary>
    public enum AccountFlag
    {
        /// <summary>
        /// With this setting, an anchor must approve anyone who wants to hold its asset.
        /// </summary>
        AuthRequiredFlag = AccountFlags.AccountFlagsEnum.AUTH_REQUIRED_FLAG,

        /// <summary>
        /// With this setting, an anchor can set the authorize flag of an existing trustline to freeze the assets held by an asset holder.
        /// </summary>
        AuthRevocableFlag = AccountFlags.AccountFlagsEnum.AUTH_REVOCABLE_FLAG,

        /// <summary>
        /// With this setting, none of the following authorization flags can be changed.
        /// </summary>
        AuthImmutableFlag = AccountFlags.AccountFlagsEnum.AUTH_IMMUTABLE_FLAG
    }
}