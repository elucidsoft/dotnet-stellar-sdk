using stellar_dotnetcore_sdk.xdr;
using System;
using System.Collections.Generic;
using System.Text;

namespace stellar_dotnetcore_sdk
{
    public enum AccountFlag
    {
        AUTH_REQUIRED_FLAG = AccountFlags.AccountFlagsEnum.AUTH_REQUIRED_FLAG,
        AUTH_REVOCABLE_FLAG = AccountFlags.AccountFlagsEnum.AUTH_REVOCABLE_FLAG,
        AUTH_IMMUTABLE_FLAG = AccountFlags.AccountFlagsEnum.AUTH_IMMUTABLE_FLAG
    }
}
